using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameClear : MonoBehaviour
{
    [SerializeField]
    private EventManager _eventManager;

    [SerializeField]
    private Text _clearDayText;

    // Start is called before the first frame update
    void Start()
    {
        _eventManager.ActiveScreen = "GameClearScreen";
        _eventManager.ActiveScene = "GameClear";

        //クリア日数をセット
        _clearDayTextSet();

        GameObject _gameManager = GameObject.Find("GameManager");

        //ゲームマネージャ削除
        SceneManager.MoveGameObjectToScene(_gameManager, SceneManager.GetActiveScene());
        //クリアメッセージを飛ばす
        _eventManager.EventSendMessage();
    }

    //クリア日を表示
    private void _clearDayTextSet()
    {
        _clearDayText.text = "Game Clear!!!\n" 
                            +  "クリア日数：" + GameManager.Instance._dayManage.Day.ToString();
    }
}
