using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UniRx;

public class HPBarModel : MonoBehaviour
{
    //HPBarの緑ゲージ部、Barは1本で2キャラ分を共有
    [SerializeField] Transform _hpBarTr;

    private void Start()
    {
        for(int i = 0; i < ConstValue._playerAmount; i++)
        {
            //HpBarのストリームを作成
            GameManager.Instance._playerStatus[i].Hp
                .Subscribe((x) => SetHPBar(x))
                .AddTo(this);
        }
    }

    //HPBarの変動
    public void SetHPBar(int _hp)
    {
        float _hpRaito = (float)_hp / (float)GameManager.Instance._playerStatus[GameManager.Instance.PlayerOperate].MaxHp;
        _hpBarTr.localScale = new Vector3(_hpRaito, 1, 1);
    }
}
