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

        _navSpeed = _enemyNavmesh._navmeshAgent.speed;

        //敵の近づく距離
        _enemyNavmesh._navmeshAgent.stoppingDistance = EnemyStatus._enemyStatusData.sheets[0].list[_enemyNo]._enemyDistance;

        _enemyHp = new ReactiveProperty<int>(EnemyStatus._enemyStatusData.sheets[0].list[_enemyNo]._enemyMaxHp);


        //攻撃用ストリーム作成
        IDisposable _attackStream = this.UpdateAsObservable()
            .Where((_) => _searchEnemyFlag && !_actionFlag)
            .ThrottleFirst(TimeSpan.FromMilliseconds(EnemyStatus._enemyStatusData.sheets[0].list[_enemyNo]._enemyAttackDurationTime))
            .Subscribe((_) => EnemyAction())
            .AddTo(this);

        //体力管理用ストリーム作成
        _enemyHp
            .Subscribe((x) =>
            {
                if(x > 0)
                {
                    _animator.SetTrigger("Damage");
                }
                else
                {
                    _attackStream.Dispose();
                    _animator.SetTrigger("Die");
                }
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
    private void NavmeshStop()
    {
        _enemyNavmesh._navmeshAgent.speed = 0f;
        _enemyNavmesh._navmeshAgent.updateRotation = false;
    }
    //ナビメッシュ移動を再開
    private void NavmeshResume()
    {
        Debug.Log("EEE");
        _enemyNavmesh._navmeshAgent.speed = _navSpeed;
        _enemyNavmesh._navmeshAgent.updateRotation = true;
    }

    //攻撃アニメーション時、攻撃を生成
    private void EnemyAttack()
    {
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
        //プレイヤーのリアクティブプロパティのお金、魔石の値を上昇
        //破壊アニメーション
        Destroy(gameObject);
    }

}
