using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using Cysharp.Threading.Tasks;
using System;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private int _enemyNo;
    public int EnemyNo => _enemyNo;

    private ReactiveProperty<int> _enemyHp;
    public ReactiveProperty<int> ReactiveHp
    {
        get { return _enemyHp; }
        set { _enemyHp = value; }
    }
    public int EnemyHp {
        get { return _enemyHp.Value; }
        set {
            if (value > 0) _enemyHp.Value = value;
            else _enemyHp.Value = 0;
        }
    }
    private EnemyNavmesh _enemyNavmesh;

    [SerializeField]
    private GameObject _actionObj;

    private Animator _animator;

    private SE _enemySE;

    private EnemyHpBarModel _enemyHpBarModel;

    //攻撃開始範囲にプレイヤーがいるか
    public bool _searchEnemyFlag;

    //アクション中は動作を停止
    private bool _actionFlag;
    //ナビメッシュのスピード
    float _navSpeed;


    void Start()
    {
        _enemyNavmesh = GetComponent<EnemyNavmesh>();
        _animator = GetComponent<Animator>();
        _enemySE = GetComponent<SE>();
        _enemyHpBarModel = GetComponent<EnemyHpBarModel>();

        _navSpeed = _enemyNavmesh._navmeshAgent.speed;

        //敵の近づく距離
        _enemyNavmesh._navmeshAgent.stoppingDistance = EnemyStatus._enemyStatusData.sheets[0].list[_enemyNo]._enemyDistance;

        _enemyHp = new ReactiveProperty<int>(EnemyStatus._enemyStatusData.sheets[0].list[_enemyNo]._enemyMaxHp);


        //攻撃用ストリーム作成
        IDisposable _attackStream = this.UpdateAsObservable()
            .Where((_) => _searchEnemyFlag && !_actionFlag && EventManager._eventFlag.Value == false)
            .ThrottleFirst(TimeSpan.FromMilliseconds(EnemyStatus._enemyStatusData.sheets[0].list[_enemyNo]._enemyAttackDurationTime))
            .Subscribe((_) => EnemyAction())
            .AddTo(this);

        //体力管理用ストリーム作成
        _enemyHp
            .Skip(1)
            .Subscribe((x) =>
            {
                if (EnemyStatus._enemyStatusData.sheets[0].list[_enemyNo]._enemyDamageAudio != null) _enemySE.PlaySE(EnemyStatus._enemyStatusData.sheets[0].list[_enemyNo]._enemyDamageAudio);
                if (x > 0)
                {

                    _animator.SetTrigger("Damage");
                }
                else
                {
                    _attackStream.Dispose();
                    _animator.SetTrigger("Die");
                }

                _enemyHpBarModel.SetHpUI(x, _enemyNo);
            })
            .AddTo(this);
    }

    //一定時間毎に攻撃を実行
    private async void EnemyAction()
    {
        try
        {
            _actionFlag = true;
            NavmeshStop();

            _animator.SetTrigger("Attack");

            var token = this.GetCancellationTokenOnDestroy();
            await UniTask.Delay(EnemyStatus._enemyStatusData.sheets[0].list[_enemyNo]._enemyAttackEndTime, cancellationToken: token);

            NavmeshResume();
            _actionFlag = false;
        }
        catch (OperationCanceledException e)
        {

        }
    }

    //ナビメッシュ移動を停止
    public void NavmeshStop()
    {
        _enemyNavmesh._navmeshAgent.speed = 0f;
        _enemyNavmesh._navmeshAgent.updateRotation = false;
    }
    //ナビメッシュ移動を再開
    public void NavmeshResume()
    {
        _enemyNavmesh._navmeshAgent.speed = _navSpeed;
        _enemyNavmesh._navmeshAgent.updateRotation = true;
    }

    //攻撃アニメーション時、攻撃を生成
    private void EnemyAttack()
    {
        if (EnemyStatus._enemyStatusData.sheets[0].list[_enemyNo]._enemyAttackAudio != null) _enemySE.PlaySE(EnemyStatus._enemyStatusData.sheets[0].list[_enemyNo]._enemyAttackAudio);

        GameObject _attackObj = Instantiate(_actionObj, transform.forward, Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0));
        //弾の大きさに応じて敵から離れた位置に弾を生成
        _attackObj.transform.position = transform.position + transform.forward * EnemyStatus._enemyStatusData.sheets[0].list[_enemyNo]._enemyAttackRange + new Vector3(0, 0.5f, 0);
    }

    //ダメージアニメーション時、中断されたものを復元
    private void EnemyDamage()
    {
        NavmeshResume();
        _actionFlag = false;
    }

    private void EnemyDestroy()
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
        MagicStoneDisplayModel._magicStone[0].Value += EnemyStatus._enemyStatusData.sheets[0].list[_enemyNo]._dropPurpleStone;
        MagicStoneDisplayModel._magicStone[1].Value += EnemyStatus._enemyStatusData.sheets[0].list[_enemyNo]._dropRedStone;
        MagicStoneDisplayModel._magicStone[2].Value += EnemyStatus._enemyStatusData.sheets[0].list[_enemyNo]._dropBlueStone;
        MagicStoneDisplayModel._magicStone[3].Value += EnemyStatus._enemyStatusData.sheets[0].list[_enemyNo]._dropGreenStone;
        MagicStoneDisplayModel._magicStone[4].Value += EnemyStatus._enemyStatusData.sheets[0].list[_enemyNo]._dropYellowStone;
    }

}
