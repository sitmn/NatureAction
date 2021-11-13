using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    [SerializeField]
    private SE _seScr;
    [SerializeField]
    private AudioClip _gameStartButtonSE;

    // Start is called before the first frame update
    void Start()
    {

    }

    //ゲームシーンへ
    public void GameStartButton()
    {
        _seScr.PlaySE(_gameStartButtonSE);
        SceneManager.LoadScene("Nature");
    }
}
