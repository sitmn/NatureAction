using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class Presenter : MonoBehaviour
{
    private SystemData systemData;

    /******************** 画面情報 ********************/
    //現在表示されている画面
    [SerializeField]
    private CanvasGroup ActiveCanvasGroup;
    [SerializeField]
    private DayValueModel DayText;
    /**************************************************/


    /******************* ボタン情報 ********************/
    //ボタンが有効か
    static bool _buttonActiveFlag = true;

    //ボタン検知用リスト
    [SerializeField]
    private ButtonViewScreenChange[] ButtonViewScreenChangeArray;
    [SerializeField]
    private ButtonViewTraining[] ButtonViewTrainingPlayer1Array;
    [SerializeField]
    private ButtonViewTraining[] ButtonViewTrainingPlayer2Array;
    [SerializeField]
    private ButtonViewMove[] ButtonViewMoveArray;
    [SerializeField]
    private ButtonViewTrainingConfirm ButtonViewTrainingConfirm;
    [SerializeField]
    private ButtonViewMoveConfirm ButtonViewMoveConfirm;
    [SerializeField]
    private ButtonViewSetSkill[] ButtonViewSetSkillPlayer1;
    [SerializeField]
    private ButtonViewSetSkill[] ButtonViewSetSkillPlayer2;
    [SerializeField]
    private List<ButtonViewChangeSkill> ButtonViewChangeSkill;
    [SerializeField]
    private ButtonViewChangeSkillConfirm ButtonViewChangeSkillConfirm;
    [SerializeField]
    private ButtonViewSkillUnrockConfirm ButtonViewSkillUnrockConfirm;
    /**************************************************/


    /******************** 修行情報 ********************/
    //選択中の修行ボタン 0がプレイヤー1,1がプレイヤー2
    private ButtonViewTraining[] _selectTrainingButtonViewArray = new ButtonViewTraining[ConstValue._playerAmount];
    //修行で変動する値の構造体
    private TrainingStruct _trainingStruct;
    [SerializeField]
    private StatusValueModel _statusValueModel;
    [SerializeField]
    private HealthBarModel _hpBarModel;
    [SerializeField]
    private MagicStoneAmountModel _magicStoneAmountModel;

    /**************************************************/

    /******************** 修行確定 ********************/
    //確定ボタン押下時のTL
    [SerializeField]
    private PlayableDirector _trainingConfirmDirector;
    //修行アニメーション暗転画面（ボタン検知用リストには入れない（ボタン非アクティブのタイミングが違い、同時押しの心配もないため））
    [SerializeField]
    private Button _skipButton;
    //修行計算用クラス
    private TrainingCalculateScript _trainingCalculateScr;
    /**************************************************/

    /******************** 移動情報 ********************/
    //移動先選択用
    private ButtonViewMove _selectMoveButtonView;
    /**************************************************/

    /******************** 移動情報 ********************/
    //確定ボタン押下時のTL
    [SerializeField]
    private PlayableDirector _moveConfirmDirector;
    /**************************************************/

    /******************** スキル情報 ********************/
    //スキル画面用スクリプト
    [SerializeField]
    private SkillScript _skillScr;
    [SerializeField]
    private ConsumeStoneAmountModel _consumeStoneAmountModel;
    private SkillData _skillData;
    //選択中の装備スキルボタン 0がプレイヤー1,1がプレイヤー2
    private ButtonViewSetSkill[] _selectSetSkillButtonViewArray = new ButtonViewSetSkill[ConstValue._playerAmount];
    //選択中のスキルボタン
    private ButtonViewChangeSkill _selectChangeSkillButton;
    UseMagicStoneStruct _useMagicStone;
    /**************************************************/

    private void Awake()
    {
        GameManager.Instance.InitializePlayerStatus();

        //初期スキルセット
        GameManager.Instance.InstantiateSkill();
        GameManager.Instance.PlayerOperate = 0;
    }


    // Start is called before the first frame update
    void Start()
    {
        systemData = Resources.Load("SystemData") as SystemData;
        _skillData = Resources.Load("SkillData") as SkillData;
        
        ButtonViewChangeSkill = _skillScr.SkillButtonSet();

        ButtonStream _buttonStream = new ButtonStream
            (ButtonViewScreenChangeArray 
            , ButtonViewTrainingPlayer1Array 
            , ButtonViewTrainingPlayer2Array 
            , ButtonViewTrainingConfirm
            , ButtonViewMoveArray
            , ButtonViewMoveConfirm
            , ButtonViewSetSkillPlayer1
            , ButtonViewSetSkillPlayer2
            , ButtonViewChangeSkill
            , ButtonViewChangeSkillConfirm
            , ButtonViewSkillUnrockConfirm);


        //ボタンのストリームを登録
        for (int i = 0; i < _buttonStream._allButtonViewList.Count; i++)
        {
            int n = i;

            _buttonStream._allButtonObservableArray[n] = _buttonStream._allButtonArray[n].OnClickAsObservable()
                .TakeUntilDestroy(_buttonStream._allButtonArray[n].gameObject)
                .Select(_ => _buttonStream._allButtonViewList[n]);
                
        }
        
        //ボタン同時押し防止のためにストリームをマージ
        Observable.Merge(_buttonStream._allButtonObservableArray)
                .Where((_) => _buttonActiveFlag)
                .Subscribe((x) => ButtonAction(x));


        //スキップボタンのストリームを登録 TLを再開
        SkipButtonView _skipButtonView = _skipButton.GetComponent<SkipButtonView>();
        _skipButton.OnClickAsObservable().ThrottleFirst(TimeSpan.FromMilliseconds(10))
            .Subscribe(_ => {
                TimeLineStart(_trainingConfirmDirector);});


        _trainingCalculateScr = new TrainingCalculateScript();
    }

    


    //ボタン毎に処理割り振り:buttonNoで判定
    private void ButtonAction(ButtonViewBase _buttonViewBase)
    {
        if (_buttonViewBase.ButtonNo == 0)
        {
            ScreenChange(_buttonViewBase);
        }
        else if(_buttonViewBase.ButtonNo == 1)
        {
            TrainingSelect(_buttonViewBase);
        }else if(_buttonViewBase.ButtonNo == 2)
        {
            TrainingConfirm();
        }
        else if(_buttonViewBase.ButtonNo == 3)
        {
            MoveSelect(_buttonViewBase);
        }else if(_buttonViewBase.ButtonNo == 4)
        {
            MoveButtonConfirm();
        }else if(_buttonViewBase.ButtonNo == 5)
        {
            SetSkillSelect(_buttonViewBase);
        }
        else if (_buttonViewBase.ButtonNo == 6)
        {
            SetSkillChangeSelect(_buttonViewBase);
        }
        else if (_buttonViewBase.ButtonNo == 7)
        {
            SetSkillChange();
        }
        else if (_buttonViewBase.ButtonNo == 8)
        {
            SkillUnrock();
        }
    }
    
    //選択中のボタンをクリア
    public void SelectButtonClear()
    {
        for(int i = 0; i < _selectTrainingButtonViewArray.Length; i++)
        {
            _selectTrainingButtonViewArray[i] = null;
        }

        _statusValueModel.NullValueSet();
        _magicStoneAmountModel.SetMagicStoneText();
        _hpBarModel.BlinkHPBarHide();
        _trainingStruct = new TrainingStruct();

        _selectMoveButtonView = null;

        for(int i = 0; i < _selectSetSkillButtonViewArray.Length; i++){
            _selectSetSkillButtonViewArray[i] = null;
        }

        _selectChangeSkillButton = null;

        _useMagicStone = new UseMagicStoneStruct();
    }

    
    //ボタンNo0:画面遷移の処理
    private void ScreenChange(ButtonViewBase _buttonViewBase)
    {
        ButtonViewScreenChange _buttonViewScreenChange = _buttonViewBase as ButtonViewScreenChange;
        if (_buttonViewScreenChange.NextCanvasGroup == ActiveCanvasGroup) return;

        _buttonActiveFlag = false;

        //次に表示する画面をセット
        _buttonViewScreenChange.NextCanvasGroup.gameObject.SetActive(true);
        Vector2 _setPos = new Vector2(Screen.width, 0);
        _buttonViewScreenChange.NextCanvasGroup.transform.localPosition = _setPos;

        //今表示されている画面と次に表示する画面をスライド移動、画面外の画面は非表示
        _buttonViewScreenChange.NextCanvasGroup.transform.DOLocalMove(Vector2.zero, systemData.sheets[0].list[0].slideTime);
        ActiveCanvasGroup.transform.DOLocalMove(new Vector2(-Screen.width, 0), systemData.sheets[0].list[0].slideTime)
            .OnComplete(() => {
                //修行ボタンの選択状態をクリア
                SelectButtonClear();

                ActiveCanvasGroup.gameObject.SetActive(false);
                ActiveCanvasGroup = _buttonViewScreenChange.NextCanvasGroup;
                _buttonActiveFlag = true;
            });
        
    }


    //ボタンNo1:修行ボタンの選択処理  各パラメータを取得,プレイヤー1と2の修行ボタン選択時１つのみ点滅
    private void TrainingSelect(ButtonViewBase _buttonViewBase)
    {
        _trainingCalculateScr = new TrainingCalculateScript();
        //修行ボタンクラスとしてキャスト
        ButtonViewTraining _buttonViewTraining = _buttonViewBase as ButtonViewTraining;
        
        //選択ボタンをグローバル変数に格納するためのメソッド
        Action<ButtonViewBase> _buttonSelectInMethod = TrainingButtonViewSelect;
        
        //押したボタンを１つ選択状態に
        if (_buttonViewTraining.OwnerIsPlayer2TrainingButtonFlag)
        {
            ButtonSelect(_buttonViewTraining, ButtonViewTrainingPlayer2Array, _buttonSelectInMethod);
        }
        else { 
            ButtonSelect(_buttonViewTraining, ButtonViewTrainingPlayer1Array, _buttonSelectInMethod); 
        }

        //体力消費・上昇ステータス・魔石消費量を計算
        _trainingStruct = _trainingCalculateScr.TrainingCalculate(_selectTrainingButtonViewArray);

        //ステータス上昇値を表示
        _statusValueModel.ChangeValueText(_trainingStruct._trainingStatusStruct);

        //体力消費を表示
        _hpBarModel.SetBlinkHPBar(_trainingStruct._changeHealth);

        //魔石消費量を表示
        _magicStoneAmountModel.RemainMagicStoneDisplay(_trainingStruct._useMagicStoneStruct);
    }



    //配列内のボタンを１つのみ選択状態に
    private void ButtonSelect(ButtonViewBase _buttonViewBase, ButtonViewBase[] _buttonViewBaseArray, Action<ButtonViewBase> _buttonSelectInMethod)
    {
        foreach (ButtonViewBase _buttonView in _buttonViewBaseArray)
        {
            _buttonView.ButtonSelectFlag = false;
        }

        _buttonViewBase.ButtonSelectFlag = true;

        _buttonSelectInMethod(_buttonViewBase);
    }

    //現在格納している選択ボタンを上書き
    private void TrainingButtonViewSelect(ButtonViewBase _selectButtonView)
    {
        ButtonViewTraining _buttonViewTraining = _selectButtonView as ButtonViewTraining;
        _selectTrainingButtonViewArray[Convert.ToInt32(_buttonViewTraining.OwnerIsPlayer2TrainingButtonFlag)] = _buttonViewTraining;
    }
    private void MoveButtonViewSelect(ButtonViewBase _selectButtonView)
    {
        ButtonViewMove _buttonViewMove = _selectButtonView as ButtonViewMove;
        _selectMoveButtonView = _buttonViewMove;
    }
    private void SetSkillButtonViewSelect(ButtonViewBase _selectButtonView)
    {
        ButtonViewSetSkill _buttonViewSetSkill = _selectButtonView as ButtonViewSetSkill;
        _selectSetSkillButtonViewArray[_buttonViewSetSkill.PlayerOperate] = _buttonViewSetSkill;
    }

    private void ChangeSkillButtonViewSelect(ButtonViewBase _selectButtonView)
    {
        ButtonViewChangeSkill _buttonViewChangeSkill = _selectButtonView as ButtonViewChangeSkill;
        _selectChangeSkillButton = _buttonViewChangeSkill;
    }

    //ボタンNo2：修行確定ボタン
    private void TrainingConfirm()
    {
        if (_selectTrainingButtonViewArray[0] == null || _selectTrainingButtonViewArray[1] == null) return;
        _buttonActiveFlag = false;

        _trainingConfirmDirector.Play();
        /*
確定ボタンを押下
→　体力ゲージが減る・ステータステキストが上昇
→　全選択状態リセット・修行スライドの下にアニメーションスライド表示
→　修行画面をスライドで画面外へ
→　アニメーション（2d OR 3d）が見える
→　画像タップ待ち
→　画面暗転
→　ホーム画面表示
→　画面暗転解除
→　日付を更新
→　あればイベント
→　ユーザ操作へ */
        //修行ボタンが2つ選択条件になっているとき以外は半透明
        //処理内容：タイムライン実行（画面上のボタンがはける、ボタンの選択状態クリア（点滅と配列）、修行アニメーション、テキスト上昇、データ反映、日付変更、画面暗転させて、再度修行画面へ）
    }

    //ステータスと魔石、体力ゲージを反映
    public void ParameterConfirm()
    {
        _hpBarModel.BlinkHPBarHide();

        _statusValueModel.StatusChange(_trainingStruct._trainingStatusStruct);

        _hpBarModel.SetHPBar(_trainingStruct._changeHealth);

        _magicStoneAmountModel.UseMagicStone(_trainingStruct._useMagicStoneStruct);
    }

    public void TimeLineStop(PlayableDirector _playableDirector)
    {
        _playableDirector.Pause();
    }

    public void TimeLineStart(PlayableDirector _playableDirector)
    {
        _playableDirector.Resume();
    }

    //スクリーンのフェードアウト フェードインは、黒一色の画面をフェードアウトさせる
    //ヒエラルキー上、上にあるCanvasGroupを_nextCanvasGroupとすること
    public void FadeOutScreen(CanvasGroup _nextCanvasGroup)
    {
        _nextCanvasGroup.gameObject.SetActive(true);
        _nextCanvasGroup.transform.localPosition = new Vector2(0, 0);

        ActiveCanvasGroup.DOFade(0, ConstValue._fadeOutTime).OnComplete(() => {
            ActiveScreenChange(_nextCanvasGroup);
        }).Play();
    }

    //表示
    private void ActiveScreenChange(CanvasGroup _nextCanvasGroup)
    {
        ActiveCanvasGroup.transform.localPosition = new Vector2(Screen.width + ActiveCanvasGroup.transform.position.x, ActiveCanvasGroup.transform.position.y);
        ActiveCanvasGroup.alpha = 1f;
        ActiveCanvasGroup.gameObject.SetActive(false);
        ActiveCanvasGroup = _nextCanvasGroup;
    }
    
    //ホームスクリーンを表示して値を更新
    public void NextDayStart()
    {
        //日付を次の日に更新
        GameManager.Instance._dayManage.Day = 1;
        DayText.SetText();

        _buttonActiveFlag = true;
    }

    //ボタンNo3：修行選択ボタン
    private void MoveSelect(ButtonViewBase _buttonViewBase)
    {
        Action<ButtonViewBase> _buttonSelectInMethod = MoveButtonViewSelect;

        ButtonSelect(_buttonViewBase, ButtonViewMoveArray, _buttonSelectInMethod);
    }

    //ボタンNo4：修行確定ボタン
    private void MoveButtonConfirm()
    {
        if (_selectMoveButtonView == null) return;

        _buttonActiveFlag = false;

        _moveConfirmDirector.Play();
    }

    public void DestinationMove()
    {
        _buttonActiveFlag = true;

        if (_selectMoveButtonView.DestinationNo == 0)
        {
            SceneManager.LoadScene("Dungeon");
        }
    }


    //ボタンNo5：装備スキル選択ボタン
    private void SetSkillSelect(ButtonViewBase _buttonViewBase)
    {
        ButtonViewSetSkill _buttonViewSetSkill = _buttonViewBase as ButtonViewSetSkill;
        Action<ButtonViewBase> _buttonSelectInMethod = SetSkillButtonViewSelect;

        if (_buttonViewSetSkill.PlayerOperate == 0)
        {
            ButtonSelect(_buttonViewBase, ButtonViewSetSkillPlayer1, _buttonSelectInMethod);
        }else if(_buttonViewSetSkill.PlayerOperate == 1)
        {
            ButtonSelect(_buttonViewBase, ButtonViewSetSkillPlayer2, _buttonSelectInMethod);
        }
    }

    //ボタンNo6：装備スキル入れ替え選択ボタン
    private void SetSkillChangeSelect(ButtonViewBase _buttonViewBase)
    {
        ButtonViewChangeSkill _buttonViewChangeSkill = _buttonViewBase as ButtonViewChangeSkill;
        Action<ButtonViewBase> _buttonSelectInMethod = ChangeSkillButtonViewSelect;

        //Listを配列に
        ButtonViewChangeSkill[] _buttonViewChangeSkillArray = new ButtonViewChangeSkill[ButtonViewChangeSkill.Count];
        for(int i = 0; i < ButtonViewChangeSkill.Count; i++)
        {
            _buttonViewChangeSkillArray[i] = ButtonViewChangeSkill[i];
        }
        //ボタンを選択状態に
        ButtonSelect(_buttonViewBase, _buttonViewChangeSkillArray, _buttonSelectInMethod);

        _useMagicStone = _skillScr.UseMagicStoneCalculate(_buttonViewChangeSkill.SkillNo);
        //魔石消費量表示,ヘッダーとスキルスクリーン
        _magicStoneAmountModel.RemainMagicStoneDisplay(_useMagicStone);
        _consumeStoneAmountModel.DisplayConsumeStone(_selectChangeSkillButton.SkillNo, _skillData);
    }

    //ボタンNo7：装備スキル入れ替え確定ボタン
    private void SetSkillChange()
    {
        //スキルが選択されていて、習得しているスキルの場合、スキル入替
        if(_selectChangeSkillButton != null && _selectSetSkillButtonViewArray[_selectChangeSkillButton.PlayerOperate] && GameManager.Instance._playerStatus[_selectChangeSkillButton.PlayerOperate]._playerSkillList[_selectChangeSkillButton.SkillNo] == true)
        {
            _skillScr.SetSkillButtonInfomation(_selectChangeSkillButton.PlayerOperate, _selectChangeSkillButton.SkillNo, _selectSetSkillButtonViewArray[_selectChangeSkillButton.PlayerOperate].SetSkillNo);
        }
    }

    //ボタンNo8：スキルアンロック確定ボタン
    private void SkillUnrock()
    {
        //スキルが選択されていて、習得していないスキルの場合、魔石が足りていればスキル習得
        if (_selectChangeSkillButton != null && GameManager.Instance._playerStatus[_selectChangeSkillButton.PlayerOperate]._playerSkillList[_selectChangeSkillButton.SkillNo] == false)
        {
            if(GameManager.Instance._shareItem._magicStone[0].Amount >= _useMagicStone._useMagicStone[0]
            && GameManager.Instance._shareItem._magicStone[1].Amount >= _useMagicStone._useMagicStone[1]
            && GameManager.Instance._shareItem._magicStone[2].Amount >= _useMagicStone._useMagicStone[2]
            && GameManager.Instance._shareItem._magicStone[3].Amount >= _useMagicStone._useMagicStone[3]
            && GameManager.Instance._shareItem._magicStone[4].Amount >= _useMagicStone._useMagicStone[4])
            {
                _skillScr.SkillObtain(_selectChangeSkillButton.PlayerOperate, _selectChangeSkillButton.SkillNo);
                _selectChangeSkillButton.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = _skillData.sheets[0].list[_selectChangeSkillButton.SkillNo]._skillImage;
                _magicStoneAmountModel.UseMagicStone(_useMagicStone);
            }
        }
    }
}


//ボタンストリームクラス 全てのボタンを一括保持
public class ButtonStream{
    public List<ButtonViewBase> _allButtonViewList;
    public Button[] _allButtonArray;
    public IObservable<ButtonViewBase>[] _allButtonObservableArray;


    //クラス内リスト・配列の初期化
    public ButtonStream
        (ButtonViewScreenChange[] _buttonViewScreenChangeArray
        , ButtonViewTraining[] _buttonViewTrainingPlayer1Array
        , ButtonViewTraining[] _buttonViewTrainingPlayer2Array
        , ButtonViewTrainingConfirm _buttonViewTrainingConfirm
        , ButtonViewMove[] _buttonViewMove
        , ButtonViewMoveConfirm _buttonViewMoveConfirm
        , ButtonViewSetSkill[] _buttonViewSetSkillPlayer1
        , ButtonViewSetSkill[] _buttonViewSetSkillPlayer2
        , List<ButtonViewChangeSkill> _buttonViewChangeSkill
        , ButtonViewChangeSkillConfirm _buttonViewChangeSkillConfirm
        , ButtonViewSkillUnrockConfirm _buttonViewSkillUnrockConfirm)
    {
        //全てのボタン数
        int _allButtonCount
            = _buttonViewScreenChangeArray.Length
            + _buttonViewTrainingPlayer1Array.Length
            + _buttonViewTrainingPlayer2Array.Length
            + 1 //_buttonViewTrainingConfirm
            + _buttonViewMove.Length
            + 1 //_buttonViewMoveConfirm
            + _buttonViewSetSkillPlayer1.Length
            + _buttonViewSetSkillPlayer2.Length
            + _buttonViewChangeSkill.Count
            + 1 //_buttonViewChangeSkillConfirm
            + 1 //_buttonViewSkillUnrockConfirm
            ;

        _allButtonViewList = new List<ButtonViewBase>();
        _allButtonArray = new Button[_allButtonCount];
        _allButtonObservableArray = new IObservable<ButtonViewBase>[_allButtonCount];

        foreach(ButtonViewScreenChange _buttonView in _buttonViewScreenChangeArray)
        {
            _allButtonViewList.Add(_buttonView);
        }
        foreach (ButtonViewTraining _buttonView in _buttonViewTrainingPlayer1Array)
        {
            _allButtonViewList.Add(_buttonView);
        }
        foreach (ButtonViewTraining _buttonView in _buttonViewTrainingPlayer2Array)
        {
            _allButtonViewList.Add(_buttonView);
        }

            _allButtonViewList.Add(_buttonViewTrainingConfirm);

        foreach (ButtonViewMove _buttonView in _buttonViewMove)
        {
            _allButtonViewList.Add(_buttonView);
        }

        _allButtonViewList.Add(_buttonViewMoveConfirm);

        foreach (ButtonViewSetSkill _buttonView in _buttonViewSetSkillPlayer1)
        {
            _allButtonViewList.Add(_buttonView);
        }

        foreach (ButtonViewSetSkill _buttonView in _buttonViewSetSkillPlayer2)
        {
            _allButtonViewList.Add(_buttonView);
        }

        foreach (ButtonViewChangeSkill _buttonView in _buttonViewChangeSkill)
        {
            _allButtonViewList.Add(_buttonView);
        }

        _allButtonViewList.Add(_buttonViewChangeSkillConfirm);

        _allButtonViewList.Add(_buttonViewSkillUnrockConfirm);

        for (int i = 0; i < _allButtonViewList.Count; i++)
        {
            _allButtonArray[i] = _allButtonViewList[i].gameObject.GetComponent<Button>();
        }
        for (int i = 0; i < _allButtonViewList.Count; i++)
        {
            _allButtonArray[i] = _allButtonViewList[i].gameObject.GetComponent<Button>();
        }
    }
}