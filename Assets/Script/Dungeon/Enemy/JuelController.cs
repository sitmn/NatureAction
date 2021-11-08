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
    public ReactiveProperty<int> ReactiveHp
    {
        get { return _juelHp; }
        set { _juelHp = value; }
    }
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

    private SE _juelSE;
    private JuelHpBarModel _juelHpBarModel;


    // Start is called before the first frame update
    void Start()
    {
        this._juelHp = new ReactiveProperty<int>(EnemyStatus._juelStatusData.sheets[0].list[_juelNo]._juelMaxHp);
        _juelSE = GetComponent<SE>();
        _juelHpBarModel = GetComponent<JuelHpBarModel>();
        
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
            .Skip(1)
            .Subscribe((x) =>
            {
                if (x > 0)
                {
                    if (EnemyStatus._juelStatusData.sheets[0].list[_juelNo]._juelDamageAudio != null) _juelSE.PlaySE(EnemyStatus._juelStatusData.sheets[0].list[_juelNo]._juelDamageAudio);
                }
                else
                {
                    if (EnemyStatus._juelStatusData.sheets[0].list[_juelNo]._juelDamageAudio != null) _juelSE.PlaySE(EnemyStatus._juelStatusData.sheets[0].list[_juelNo]._juelDamageAudio);
                    _attackStream.Dispose();
                    JuelDestroy();
                }

                _juelHpBarModel.SetHpUI(x, _juelNo);
            })
            .AddTo(this);
    }

    //一定時間毎に攻撃を実行
    private void JuelAction()
    {
        if (EnemyStatus._juelStatusData.sheets[0].list[_juelNo]._juelAttackAudio != null) _juelSE.PlaySE(EnemyStatus._juelStatusData.sheets[0].list[_juelNo]._juelAttackAudio);

        GameObject childObj = Instantiate(_actionObj, transform.forward, Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0));
        childObj.transform.parent = transform;
        //弾の大きさに応じて宝石から離れた位置に弾を生成
        childObj.transform.localPosition = Vector3.right * EnemyStatus._juelStatusData.sheets[0].list[_juelNo]._juelAttackRangeMagnification;
        
    }

    //HPが0になると撃破
    private void JuelDestroy()
    {
        //マップから敵を削除
        MapManager._mapEnemyCount.Value -= 1;
        //魔石をドロップ
        StoneDrop();

        //プレイヤーのリアクティブプロパティのお金、魔石の値を上昇
        //破壊アニメーション

        Destroy(gameObject);
    }

    private void StoneDrop()
    {
        MagicStoneDisplayModel._magicStone[0].Value += EnemyStatus._juelStatusData.sheets[0].list[_juelNo]._dropPurpleStone;
        MagicStoneDisplayModel._magicStone[1].Value += EnemyStatus._juelStatusData.sheets[0].list[_juelNo]._dropRedStone;
        MagicStoneDisplayModel._magicStone[2].Value += EnemyStatus._juelStatusData.sheets[0].list[_juelNo]._dropBlueStone;
        MagicStoneDisplayModel._magicStone[3].Value += EnemyStatus._juelStatusData.sheets[0].list[_juelNo]._dropGreenStone;
        MagicStoneDisplayModel._magicStone[4].Value += EnemyStatus._juelStatusData.sheets[0].list[_juelNo]._dropYellowStone;
    }
}
