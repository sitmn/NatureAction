using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Triggers;
using UniRx;
using System;

public class JuelController : MonoBehaviour
{
    [SerializeField]
    private GameObject _actionObj;  //攻撃用のオブジェクト

    public bool _searchEnemyFlag;   //索敵範囲にプレイヤーまたは敵がいるか

    [SerializeField]
    private int _juelNo;   //宝石番号（宝石名の整理）
    public int JuelNo => _juelNo;

    private ReactiveProperty<int> _juelHp;
    public ReactiveProperty<int> ReactiveJuelHp => _juelHp;
    public int JuelHp
    {
        get { return _juelHp.Value; }
        set {   if(value > 0)
                {
                    _juelHp.Value = value;
                }
                else
                {
                _juelHp.Value = 0;
                }
            }
    }


    // Start is called before the first frame update
    void Start()
    {
        this._juelHp = new ReactiveProperty<int>(EnemyStatus._juelStatusData.sheets[0].list[_juelNo]._juelMaxHp);

        _searchEnemyFlag = false;

        _juelHp.Value = EnemyStatus._juelStatusData.sheets[0].list[_juelNo]._juelMaxHp;

        //攻撃用ストリーム作成
        IDisposable _attackStream = this.UpdateAsObservable()
            .Where((_) => _searchEnemyFlag)
            .ThrottleFirst(TimeSpan.FromMilliseconds(EnemyStatus._juelStatusData.sheets[0].list[_juelNo]._juelAttackDurationTime))
            .Subscribe((_) => JuelAction())
            .AddTo(this);

        //体力管理用ストリーム作成
        _juelHp
            .Where((x) => x <= 0)
            .Subscribe((_) =>
            {
                _attackStream.Dispose();
                JuelDestroy();
            })
            .AddTo(this);
    }

    //一定時間毎に攻撃を実行
    private void JuelAction()
    {
        GameObject childObj = Instantiate(_actionObj, transform.forward, Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0));
        childObj.transform.parent = transform;
        //弾の大きさに応じて宝石から離れた位置に弾を生成
        childObj.transform.localPosition = Vector3.right * EnemyStatus._juelStatusData.sheets[0].list[_juelNo]._juelAttackRangeMagnification;
        
    }

    //HPが0になると撃破
    private void JuelDestroy()
    {
        //プレイヤーのリアクティブプロパティのお金、魔石の値を上昇
        //破壊アニメーション

        Destroy(gameObject);
    }
}
