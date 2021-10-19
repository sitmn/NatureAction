using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StatusValueModel : MonoBehaviour
{
    [SerializeField] private Text[] _changeStatusTextPlayer1;
    [SerializeField] private Text[] _changeStatusTextPlayer2;

    [SerializeField] private Text[] _statusTextPlayer1;
    [SerializeField] private Text[] _statusTextPlayer2;


    private void Start()
    {
        _statusTextPlayer1[0].text = GameManager._playerStatus[0].MaxHp.ToString();
        _statusTextPlayer1[1].text = GameManager._playerStatus[0].MaxMp.ToString();
        _statusTextPlayer1[2].text = GameManager._playerStatus[0].Attack.ToString();
        _statusTextPlayer1[3].text = GameManager._playerStatus[0].Defense.ToString();
        _statusTextPlayer1[4].text = GameManager._playerStatus[0].Speed.ToString();
        _statusTextPlayer2[0].text = GameManager._playerStatus[1].MaxHp.ToString();
        _statusTextPlayer2[1].text = GameManager._playerStatus[1].MaxMp.ToString();
        _statusTextPlayer2[2].text = GameManager._playerStatus[1].Attack.ToString();
        _statusTextPlayer2[3].text = GameManager._playerStatus[1].Defense.ToString();
        _statusTextPlayer2[4].text = GameManager._playerStatus[1].Speed.ToString();
    }

    //増減値の表示
    public void ChangeValueText(TrainingStatusStruct _trainingStatusStruct)
    {
        for(int i= 0; i < ConstValue._playerStatuKind;i++)
        {
            _changeStatusTextPlayer1[i].text = InputNotNull(_trainingStatusStruct._trainingPoint[i, 0]);
            _changeStatusTextPlayer2[i].text = InputNotNull(_trainingStatusStruct._trainingPoint[i, 1]);
        }
    }

    //上昇値が0の場合はテキストを表示しない
    private string InputNotNull(int _statusPoint)
    {
        string InputText;

        if (_statusPoint != 0)
        {
            InputText = _statusPoint.ToString();
        }
        else
        {
            InputText = "";
        }

        return InputText;
    }

    //ステータスの増減アニメーションさせてデータに反映
    public void StatusChange(TrainingStatusStruct _trainingStatusStruct)
    {
        int[] _beforeStatus = new int[10];

        //修行前ステータス
        _beforeStatus[0] = GameManager._playerStatus[0].MaxHp;
        _beforeStatus[1] = GameManager._playerStatus[0].MaxMp;
        _beforeStatus[2] = GameManager._playerStatus[0].Attack;
        _beforeStatus[3] = GameManager._playerStatus[0].Defense;
        _beforeStatus[4] = GameManager._playerStatus[0].Speed;
        _beforeStatus[5] = GameManager._playerStatus[1].MaxHp;
        _beforeStatus[6] = GameManager._playerStatus[1].MaxMp;
        _beforeStatus[7] = GameManager._playerStatus[1].Attack;
        _beforeStatus[8] = GameManager._playerStatus[1].Defense;
        _beforeStatus[9] = GameManager._playerStatus[1].Speed;

        //修行後ステータス
        GameManager._playerStatus[0].MaxHp += _trainingStatusStruct._trainingPoint[0, 0];
        GameManager._playerStatus[0].MaxMp += _trainingStatusStruct._trainingPoint[1, 0];
        GameManager._playerStatus[0].Attack += _trainingStatusStruct._trainingPoint[2, 0];
        GameManager._playerStatus[0].Defense += _trainingStatusStruct._trainingPoint[3, 0];
        GameManager._playerStatus[0].Speed += _trainingStatusStruct._trainingPoint[4, 0];
        GameManager._playerStatus[1].MaxHp += _trainingStatusStruct._trainingPoint[0, 1];
        GameManager._playerStatus[1].MaxMp += _trainingStatusStruct._trainingPoint[1, 1];
        GameManager._playerStatus[1].Attack += _trainingStatusStruct._trainingPoint[2, 1];
        GameManager._playerStatus[1].Defense += _trainingStatusStruct._trainingPoint[3, 1];
        GameManager._playerStatus[1].Speed += _trainingStatusStruct._trainingPoint[4, 1];

        //テキスト動作
        _statusTextPlayer1[0].DOCounter(_beforeStatus[0],GameManager._playerStatus[0].MaxHp, ConstValue._textChangeTime).Play();
        _statusTextPlayer1[1].DOCounter(_beforeStatus[1],GameManager._playerStatus[0].MaxMp, ConstValue._textChangeTime).Play();
        _statusTextPlayer1[2].DOCounter(_beforeStatus[2],GameManager._playerStatus[0].Attack, ConstValue._textChangeTime).Play();
        _statusTextPlayer1[3].DOCounter(_beforeStatus[3],GameManager._playerStatus[0].Defense, ConstValue._textChangeTime).Play();
        _statusTextPlayer1[4].DOCounter(_beforeStatus[4],GameManager._playerStatus[0].Speed, ConstValue._textChangeTime).Play();
        _statusTextPlayer2[0].DOCounter(_beforeStatus[5],GameManager._playerStatus[1].MaxHp, ConstValue._textChangeTime).Play();
        _statusTextPlayer2[1].DOCounter(_beforeStatus[6],GameManager._playerStatus[1].MaxMp, ConstValue._textChangeTime).Play();
        _statusTextPlayer2[2].DOCounter(_beforeStatus[7],GameManager._playerStatus[1].Attack, ConstValue._textChangeTime).Play();
        _statusTextPlayer2[3].DOCounter(_beforeStatus[8],GameManager._playerStatus[1].Defense, ConstValue._textChangeTime).Play();
        _statusTextPlayer2[4].DOCounter(_beforeStatus[9],GameManager._playerStatus[1].Speed, ConstValue._textChangeTime).Play();
    }

    //修行スクリーン非表示時、増減値テキストをNull
    public void NullValueSet()
    {
        for (int i = 0; i < _changeStatusTextPlayer1.Length; i++){
            _changeStatusTextPlayer1[i].text = "";
        }
        for (int i = 0; i < _changeStatusTextPlayer2.Length; i++)
        {
            _changeStatusTextPlayer2[i].text = "";
        }
    }
}
