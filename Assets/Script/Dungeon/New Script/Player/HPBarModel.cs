using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UniRx;
using UnityEngine.UI;

public class HPBarModel : MonoBehaviour
{
    //HPBarの緑ゲージとテキスト、1つで2キャラ分を共有
    [SerializeField] Transform _hpBarTr;
    [SerializeField] Text _hpText;
    [SerializeField] Text _hpMaxText;

    private void Start()
    {
        for(int i = 0; i < ConstValue._playerAmount; i++)
        {
            //HpBarのストリームを作成
            GameManager.Instance._playerStatus[i].ReactiveHp
                .Subscribe((x) => { SetHpUI(x); })
                .AddTo(this);
        }
    }

    public void SetHpUI(int _hp)
    {
        SetHPBar(_hp);
        SetHpText(_hp);
    }

    //HPBarの変動
    private void SetHPBar(int _hp)
    {
        float _hpRaito = (float)_hp / (float)GameManager.Instance._playerStatus[GameManager.Instance.PlayerOperate].MaxHp;
        if(_hpRaito > 0) _hpBarTr.localScale = new Vector3(_hpRaito, 1, 1);
        else _hpBarTr.localScale = new Vector3(0, 1, 1);
    }

    //HPTextの変動
    private void SetHpText(int _hp)
    {
        _hpText.text = GameManager.Instance._playerStatus[GameManager.Instance.PlayerOperate].Hp.ToString();
        _hpMaxText.text = GameManager.Instance._playerStatus[GameManager.Instance.PlayerOperate].MaxHp.ToString();
    }
}
