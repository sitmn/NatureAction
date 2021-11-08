using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UniRx;
using UnityEngine.UI;

public class StatusBarModel : MonoBehaviour
{
    //HPBarの緑ゲージとテキスト、1つで2キャラ分を共有
    [SerializeField] Transform _hpBarTr;
    [SerializeField] Text _hpText;
    [SerializeField] Text _hpMaxText;

    //MPBarの緑ゲージとテキスト、1つで2キャラ分を共有
    [SerializeField] Transform _mpBarTr;
    [SerializeField] Text _mpText;
    [SerializeField] Text _mpMaxText;

    private void Start()
    {
        //体力に応じてHPMPを減らす
        SetInstantiateStatus();

        //現在体力に応じてHPとMPが減る
        for (int i = 0; i < ConstValue._playerAmount; i++)
        {
            GameManager.Instance._playerStatus[i].Hp = (int)((float)GameManager.Instance._playerStatus[i].MaxHp
                * (float)GameManager.Instance._playerStatus[i].Health / GameManager.Instance._playerStatus[i].MaxHealth);

            GameManager.Instance._playerStatus[i].Mp = (int)((float)GameManager.Instance._playerStatus[i].MaxMp
                * (float)GameManager.Instance._playerStatus[i].Health / GameManager.Instance._playerStatus[i].MaxHealth);
        }

        for (int i = 0; i < ConstValue._playerAmount; i++)
        {
            //HpBarのストリームを作成
            GameManager.Instance._playerStatus[i].ReactiveHp
                .Skip(1)
                .Subscribe((x) => { SetHpUI(x, GameManager.Instance.PlayerOperate); })
                .AddTo(this);

            //MpBarのストリームを作成
            GameManager.Instance._playerStatus[i].ReactiveMp
                .Skip(1)
                .Subscribe((x) => { SetMpUI(x, GameManager.Instance.PlayerOperate); })
                .AddTo(this);
        }

        //キャラ1のみStatusゲージをセット
        InitializeSetUI(GameManager.Instance._playerStatus[0].Hp, GameManager.Instance._playerStatus[0].Mp, 0);
    }

    private void SetInstantiateStatus()
    {
        //現在体力に応じてHPとMPが減る
        for (int i = 0; i < ConstValue._playerAmount; i++)
        {
            GameManager.Instance._playerStatus[i].Hp = (int)((float)GameManager.Instance._playerStatus[i].MaxHp
                * (float)GameManager.Instance._playerStatus[i].Health / GameManager.Instance._playerStatus[i].MaxHealth);

            GameManager.Instance._playerStatus[i].Mp = (int)((float)GameManager.Instance._playerStatus[i].MaxMp
                * (float)GameManager.Instance._playerStatus[i].Health / GameManager.Instance._playerStatus[i].MaxHealth);
        }
    }


    private void InitializeSetUI(int _hpMax, int _mpMax, int _playerOperate)
    {
        SetHpUI(_hpMax, _playerOperate);
        SetMpUI(_mpMax, _playerOperate);
    }


    public void SetHpUI(int _hp, int _playerOperate)
    {
        SetHPBar(_hp, _playerOperate);
        SetHpText(_hp, _playerOperate);
    }

    public void SetMpUI(int _mp, int _playerOperate)
    {
        SetMPBar(_mp, _playerOperate);
        SetMpText(_mp, _playerOperate);
    }

    //HPBarの変動
    private void SetHPBar(int _hp, int _playerOperate)
    {
        Debug.Log(_hp +"/" +GameManager.Instance._playerStatus[_playerOperate].MaxHp);
        float _hpRaito = (float)_hp / (float)GameManager.Instance._playerStatus[_playerOperate].MaxHp;
        if(_hpRaito > 0) _hpBarTr.localScale = new Vector3(_hpRaito, 1, 1);
        else _hpBarTr.localScale = new Vector3(0, 1, 1);
    }

    //HPTextの変動
    private void SetHpText(int _hp, int _playerOperate)
    {
        _hpText.text = GameManager.Instance._playerStatus[_playerOperate].Hp.ToString();
        _hpMaxText.text = GameManager.Instance._playerStatus[_playerOperate].MaxHp.ToString();
    }

    //MPBarの変動
    private void SetMPBar(int _mp, int _playerOperate)
    {
        float _mpRaito = (float)_mp / (float)GameManager.Instance._playerStatus[_playerOperate].MaxMp;
        if (_mpRaito > 0) _mpBarTr.localScale = new Vector3(_mpRaito, 1, 1);
        else _mpBarTr.localScale = new Vector3(0, 1, 1);
    }

    //MPTextの変動
    private void SetMpText(int _mp, int _playerOperate)
    {
        _mpText.text = GameManager.Instance._playerStatus[_playerOperate].Mp.ToString();
        _mpMaxText.text = GameManager.Instance._playerStatus[_playerOperate].MaxMp.ToString();
    }
}
