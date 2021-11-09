using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    //会話イベントクラス
    private EventSendMessageNameData _eventSendMessageData;

    [SerializeField]
    private Fungus.Flowchart _flowchart;

    [SerializeField]
    private Button _eventSkipButton;

    //会話イベント中か
    public static ReactiveProperty<bool> _eventFlag;

    //現在のシーン名
    private string _activeScene;
    public string ActiveScene
    {
        get { return _activeScene; }
        set { _activeScene = value; }
    }

    //現在のスクリーン名
    private string _activeScreen;
    public string ActiveScreen
    {
        get { return _activeScene; }
        set { _activeScreen = value; }
    }


    private void Awake()
    {
        _eventSendMessageData = Resources.Load("EventSendMessageName") as EventSendMessageNameData;
        _eventFlag = new ReactiveProperty<bool>(false);
    }


    //FlowChartへMessageを送る
    public void EventSendMessage()
    {
        for(int i = 0; i < GameManager.Instance._eventData.EventAlreadyRead.Length; i++)
        {
            if (_activeScene == _eventSendMessageData.sheets[0].list[i]._sceneName
             && _activeScreen == _eventSendMessageData.sheets[0].list[i]._screenName
             && GameManager.Instance._eventData.DungeonClearFlag[i] == true
             && GameManager.Instance._eventData.EventAlreadyRead[i] == false)
            {
                //会話イベント開始
                _eventFlag.Value = true;
                //会話イベントを既読に
                GameManager.Instance._eventData.EventAlreadyRead[i] = true;
                //スキップボタン表示
                _eventSkipButton.gameObject.SetActive(true);

                //Messageを送る
                _flowchart.SendFungusMessage(_eventSendMessageData.sheets[0].list[i]._sendMessageName);

                break;
            }
        }
    }

    //会話イベント終了
    public void EventFinish()
    {
        _eventSkipButton.gameObject.SetActive(false);
        _eventFlag.Value = false;
    }

    public void EventSkip()
    {
        // スキップボタン無効化
        _eventSkipButton.gameObject.SetActive(false);
        // 現在のブロックを停止する
        _flowchart.StopAllBlocks();

        EventFinish();
    }
}


//会話イベントの管理クラス、FlowChartへのメッセージ名と既読情報管理
public class EventData
{
    //イベントの既読状況
    private bool[] _eventAlreadyRead;
    public bool[] EventAlreadyRead
    {
        get { return _eventAlreadyRead; }
        set { _eventAlreadyRead = value; }
    }

    //ダンジョンクリアフラグ（関係ないものは常にTrue）
    private bool[] _dungeonClearFlag;
    public bool[] DungeonClearFlag
    {
        get { return _dungeonClearFlag; }
        set { _dungeonClearFlag = value; }
    }

    public EventData(bool[] _dungeonClearFlag)
    {
        this._eventAlreadyRead = new bool[_dungeonClearFlag.Length];
        this._dungeonClearFlag = new bool[_dungeonClearFlag.Length];

        for (int i = 0;i < _dungeonClearFlag.Length; i++)
        {
            this._eventAlreadyRead[i] = false;
            this._dungeonClearFlag[i] = _dungeonClearFlag[i];
        }
    }
}
