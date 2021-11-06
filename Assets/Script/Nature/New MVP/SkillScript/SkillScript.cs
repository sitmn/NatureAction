using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillScript : MonoBehaviour
{
    private SkillData _skillData;

    //スキル一覧
    [SerializeField]
    private Transform[] _contentTr;
    //生成用ボタン
    [SerializeField]
    private GameObject _instantiateButtonObj;

    //キャラにセットされているスキルボタンの選択状況
    private int[] _setSkill = new int[ConstValue._playerAmount];

    //キャラにセットされているスキル用のボタンイメージ
    [SerializeField]
    private Image[] _setSkillButtonImage1;
    [SerializeField]
    private Image[] _setSkillButtonImage2;

    private void Awake()
    {
        _skillData = Resources.Load("SkillData") as SkillData;
    }

    private void Start()
    {
        SkillButtonSet();
        gameObject.SetActive(false);
    }

    private void SkillButtonSet()
    {
        for(int i = 0;i < ConstValue._playerAmount; i++)
        {
            int i2 = i;

            for (int j =0; j < _skillData.sheets[0].list.Count; j++)
            {
                
                int j2 = j;

                GameObject _buttonObj = Instantiate(_instantiateButtonObj, _contentTr[i2]);
                //スキル画像と詳細文をセット
                SetSkillDetail(_buttonObj, j2);
                //ボタンの中身をセット
                _buttonObj.GetComponent<Button>().onClick.AddListener(() => SetSkillButtonInfomation(i2, j2));
            }

            for (int j = 0; j < ConstValue._skillSetMax; j++)
            {
                InstantiateSetSkillImage(i2, GameManager.Instance._playerStatus[i2]._playerSkill[j].SkillImage, j);
            }
        }
    }

    //装備しているスキルの画像セット、初期値
    private void InstantiateSetSkillImage(int _playerOperate, Sprite _skillImage, int _removeSkillNo)
    {
        if (_playerOperate == 0)
        {
            _setSkillButtonImage1[_removeSkillNo].sprite = _skillImage;
        }
        else if (_playerOperate == 1)
        {
            _setSkillButtonImage2[_removeSkillNo].sprite = _skillImage;
        }
    }

    //装備しているスキルの画像をセット
    private void SetSkillImage(int _playerOperate, int _skillNo, int _removeSkillNo)
    {
        if(_playerOperate == 0)
        {
            _setSkillButtonImage1[_removeSkillNo].sprite = _skillData.sheets[0].list[_skillNo]._skillImage;
        }else if(_playerOperate == 1)
        {
            _setSkillButtonImage2[_removeSkillNo].sprite = _skillData.sheets[0].list[_skillNo]._skillImage;
        }
    }

    //ボタンにスキルの画像と詳細をセット
    private void SetSkillDetail(GameObject _buttonObj, int _skillNo)
    {
        //スキル画像をセット
        _buttonObj.GetComponent<Transform>().GetChild(0).GetComponent<Image>().sprite = _skillData.sheets[0].list[_skillNo]._skillImage;
        //スキル詳細をセット
        _buttonObj.GetComponent<Transform>().GetChild(1).GetComponent<Text>().text = "攻撃倍率：" + _skillData.sheets[0].list[_skillNo]._skillAttackMagnification.ToString()
                                                                                + "　消費MP：" + _skillData.sheets[0].list[_skillNo]._skillMP.ToString()
                                                                                + "\n" + _skillData.sheets[0].list[_skillNo]._skillDetail;
    }

    //ボタンにスキルの内容をセット
    private void SetSkillButtonInfomation(int _playerOperate, int _skillNo)
    {
        //習得しているスキルボタン（押すとスキルをセット）
        if(GameManager.Instance._playerStatus[_playerOperate]._playerSkillList[_skillNo] == true)
        {
            SkillSet(_playerOperate, _skillNo, _setSkill[_playerOperate]);
            SetSkillImage(_playerOperate, _skillNo, _setSkill[_playerOperate]);
        }
        //習得していないスキルボタン（押すとスキル習得画面が表示）
        else
        {

        }
    }

    //スキルの習得
    public void SkillObtain(int _playerNo, int _skillNo)
    {
        GameManager.Instance._playerStatus[_playerNo]._playerSkillList[_skillNo] = true;
    }
    //使用スキルにセット
    //_setSkillNo: マスタデータで付番しているスキルNo
    //_removeSkillNo: プレイヤーが装備しているスキルリストの番号
    public void SkillSet(int _playerNo, int _setSkillNo, int _removeSkillNo)
    {
        Skill _skill = new Skill(_skillData.sheets[0].list[_setSkillNo]._skillName,
                                 _skillData.sheets[0].list[_setSkillNo]._skillMP,
                                 _skillData.sheets[0].list[_setSkillNo]._skillAttackMagnification,
                                 _skillData.sheets[0].list[_setSkillNo]._skillAnimationLength,
                                 _skillData.sheets[0].list[_setSkillNo]._skillEndTime,
                                 _skillData.sheets[0].list[_setSkillNo]._skillDurationTime,
                                 _skillData.sheets[0].list[_setSkillNo]._skillAttackRange,
                                 _skillData.sheets[0].list[_setSkillNo]._skillSpeed,
                                 _skillData.sheets[0].list[_setSkillNo]._skillTypeName,
                                 _skillData.sheets[0].list[_setSkillNo]._skillAnimation,
                                 _skillData.sheets[0].list[_setSkillNo]._skillColliderObj,
                                 _skillData.sheets[0].list[_setSkillNo]._skillImage,
                                 _skillData.sheets[0].list[_setSkillNo]._skillAudioClip);

        GameManager.Instance._playerStatus[_playerNo]._playerSkill[_removeSkillNo] = _skill;
    }

    private void OnEnable()
    {
        //スクリーン表示時、カーソルをスキル1に合わせる
        for(int i= 0; i < ConstValue._playerAmount; i++)
        {
            _setSkill[i] = 0;
        }
    }
}
