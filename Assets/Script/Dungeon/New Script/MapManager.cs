using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;

public class MapManager : MonoBehaviour
{
    public static ReactiveProperty<int> _mapEnemyCount;
    [SerializeField]
    private GameObject _parentEnemyObj;

    private void Start()
    {
        //敵をカウント
        _mapEnemyCount = new ReactiveProperty<int>(_parentEnemyObj.GetComponent<Transform>().GetChildCount());

        //敵が0になったらステージクリア
        _mapEnemyCount
            .Where((x) => x <= 0)
            .Subscribe((_) => StageClear())
            .AddTo(this);
    }

    //敵を全て倒すとステージクリアと表示され、Natureシーンへ

    private void StageClear()
    {
        //日付を更新
        NextDayUpdate(ConstValue._clearProgressDay);
        //体力をMAXまで回復
        PlayerHealthReset();

        SceneManager.LoadScene("Nature");
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
}
