using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField]
    private int _enemyNo;


    // Start is called before the first frame update
    void Start()
    {
        AttackScale();
        DeleteAttack();
    }

    //攻撃の大きさをキャラ毎に設定
    private void AttackScale()
    {
        transform.localScale *= EnemyStatus._enemyStatusData.sheets[0].list[_enemyNo]._enemyAttackRange;
    }

    //攻撃オブジェクトを一定時間で削除
    private async void DeleteAttack()
    {
        try
        {
            var token = this.GetCancellationTokenOnDestroy();
            await UniTask.Delay(EnemyStatus._enemyStatusData.sheets[0].list[_enemyNo]._enemyAttackDurationTime, cancellationToken: token);
            Destroy(gameObject);
        }
        catch (OperationCanceledException e)
        {

        }
    }


    //弾が進行方向へ移動
    // Update is called once per frame
    void Update()
    {
        Vector3 _attackVector = Quaternion.AngleAxis(transform.rotation.eulerAngles.y - 90, Vector3.up) * Vector3.right;
        transform.position += _attackVector * EnemyStatus._enemyStatusData.sheets[0].list[_enemyNo]._enemyAttackSpeed;
    }


    //プレイヤーへダメージ
    private void EnemyPlayerAttackDamage()
    {
        int damage = EnemyStatus._enemyStatusData.sheets[0].list[_enemyNo]._enemyAttack - GameManager.Instance._playerStatus[GameManager.Instance.PlayerOperate].Defense;
        if (damage <= 0) damage = 1;
        GameManager.Instance._playerStatus[GameManager.Instance.PlayerOperate].Hp -= damage;
    }

    //プレイヤーヒット時、ダメージ。プレイヤーの攻撃で消せる
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            EnemyPlayerAttackDamage();
            Destroy(gameObject);
        }
        else if (other.gameObject.tag == "PlayerAttack")
        {
            Destroy(gameObject);
        }
    }
}
