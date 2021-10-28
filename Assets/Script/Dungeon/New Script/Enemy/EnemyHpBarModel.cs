using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class EnemyHpBarModel : MonoBehaviour
{
    private GameObject _canvas;
    private Transform _hpBarTr;

    // Start is called before the first frame update
    void Awake()
    {
        _canvas = transform.GetChild(0).gameObject;
        //HPBarの取得
        _hpBarTr = _canvas.transform.GetChild(1).GetComponent<Transform>();
    }

    private void Update()
    {
        _canvas.transform.LookAt(transform.position + Vector3.forward);
    }

    public void SetHpUI(int _hp, int _enemyNo)
    {
        SetHpBar(_hp, _enemyNo);
    }

    //HPBarの変動
    private void SetHpBar(int _hp, int _enemyNo)
    {
        float _hpRaito = (float)_hp / (float)EnemyStatus._enemyStatusData.sheets[0].list[_enemyNo]._enemyMaxHp;
        if (_hpRaito > 0) _hpBarTr.localScale = new Vector3(_hpRaito, 1, 1);
        else _hpBarTr.localScale = new Vector3(0, 1, 1);
    }

}
