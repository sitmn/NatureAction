using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HealthBarModel : MonoBehaviour
{
    //HPBarの緑ゲージ部
    [SerializeField] Transform[] _hpBarTr;

    //HPBarの点滅部
    [SerializeField] Transform[] _hpBarBlinkTr;

    //HPBarの変動アニメーション
    public void SetHPBar(ChangeHealth _tired)
    {
        
        int[] _healthPoint = new int[ConstValue._playerAmount];
        for(int i = 0;i < _healthPoint.Length; i++)
        {
            Debug.Log(GameManager.Instance._playerStatus[i].Health + "プレイヤー" + i);
            _healthPoint[i] = GameManager.Instance._playerStatus[i].Health - _tired._tiredPoint[i];
            _hpBarTr[i].DOScaleX((float)_healthPoint[i] / GameManager.Instance._playerStatus[i].MaxHealth, ConstValue._textChangeTime);

            GameManager.Instance._playerStatus[i].Health = _healthPoint[i];
        }
    }


    //修行前の消費HP表示
    public void SetBlinkHPBar(ChangeHealth _tired)
    {
        //点滅ゲージの長さ
        float[] _tiredBar = new float[ConstValue._playerAmount];
        //緑ゲージの右端へ点滅用ゲージ移動用
        float[] _blinkHPBarPosX = new float[ConstValue._playerAmount];

        for (int i = 0; i < _tiredBar.Length; i++)
        {
            //点滅ゲージの長さを導出
            _tiredBar[i] = (float)_tired._tiredPoint[i] / 100f;
            _hpBarBlinkTr[i].localScale = new Vector3(_tiredBar[i], _hpBarBlinkTr[i].localScale.y, _hpBarBlinkTr[i].localScale.z);

            //緑ゲージの右端へ移動
            _blinkHPBarPosX[i] = ConstValue._hpBarWidth * _hpBarTr[i].localScale.x;
            _hpBarBlinkTr[i].localPosition = new Vector3(_blinkHPBarPosX[i], 0, 0);
        }
    }

    //点滅ゲージを非表示
    public void BlinkHPBarHide()
    {
        for(int i = 0;i < _hpBarBlinkTr.Length; i++)
        {
            _hpBarBlinkTr[i].localScale = new Vector3(0, _hpBarBlinkTr[i].localScale.y, _hpBarBlinkTr[i].localScale.z);
        }
    }

}
