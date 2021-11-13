using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    //会話イベントマネージャ
    private EventManager _eventManager;
    [SerializeField]
    private Button _eventSkipButton;

    public static ReactiveProperty<int> _mapEnemyCount;
    [SerializeField]
    private GameObject _parentEnemyObj;

    //敵の残数変動用スクリプト
    [SerializeField]
    private RemainEnemyCount _remainEnemyCount;

    private void Awake()
    {
        _eventManager = new EventManager();
        _eventManager = GameObject.Find("EventManager").GetComponent<EventManager>();

        _eventManager.ActiveScene = SceneManager.GetActiveScene().name;
        _eventManager.ActiveScreen = "";
    }

    private void Start()
    {
        //敵をカウント
        _mapEnemyCount = new ReactiveProperty<int>(_parentEnemyObj.GetComponent<Transform>().childCount);

        //敵数カウントダウン、敵が0になったらステージクリア
        _mapEnemyCount
            .Subscribe((x) => {
                if (x > 0) _remainEnemyCount.RemainEnemyCountSet();
                else StageClear();
                 })
            .AddTo(this);

        //イベントスキップボタンのストリームを登録
        _eventSkipButton.OnClickAsObservable()
            .Subscribe(_ => _eventManager.EventSkip());

        //操作キャラを1に
        GameManager.Instance.PlayerOperate = 0;

        //会話イベント
        _eventManager.EventSendMessage();
    }

    //敵を全て倒すとステージクリアと表示され、Natureシーンへ

    private void StageClear()
    {
        //日付を更新
        NextDayUpdate(ConstValue._clearProgressDay);
        //体力をMAXまで回復
        PlayerHealthReset();

        //会話イベント用のクリアフラグ更新
        DungeonClearFlag();

        ChangeSceen();
    }

    public void StageGameOver()
    {
        //日付を更新
        NextDayUpdate(ConstValue._gameoverProgressDay);
        //体力をMAXまで回復
        PlayerHealthReset();

        SceneManager.LoadScene("Nature");
    }

    private void NextDayUpdate(int _days)
    {
        GameManager.Instance._dayManage.Day = _days;
    }

    private void PlayerHealthReset()
    {
        for(int i = 0; i < ConstValue._playerAmount; i++)
        {
            GameManager.Instance._playerStatus[i].Health = GameManager.Instance._playerStatus[i].MaxHealth;
        }
    }

    private void DungeonClearFlag()
    {
        if(_eventManager.ActiveScreen == "Dungeon1")
        {
            GameManager.Instance._eventData.DungeonClearFlag[7] = true;
        }else if (_eventManager.ActiveScreen == "Dungeon2")
        {
            GameManager.Instance._eventData.DungeonClearFlag[8] = true;
        }else if (_eventManager.ActiveScreen == "Dungeon3")
        {
            GameManager.Instance._eventData.DungeonClearFlag[9] = true;
        }
    }


    private void ChangeSceen()
    {
        //ステージ3クリア時はゲームクリア画面へ、それ以外はメニューへ戻る
        if (_eventManager.ActiveScreen == "Dungeon3")
        {
            SceneManager.LoadScene("GameClear");
        }
        else
        {
            SceneManager.LoadScene("Nature");
        }
    }
}
