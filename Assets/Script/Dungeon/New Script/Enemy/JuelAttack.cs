using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

//宝石の攻撃、ダメージや攻撃Objのスピード
public class JuelAttack : MonoBehaviour
{
    private JuelController _juelController;


    // Start is called before the first frame update
    void Start()
    {
        _juelController = transform.parent.gameObject.GetComponent<JuelController>();
        AttackScale();
        DeleteAttack();
    }

    //攻撃の大きさをキャラ毎に設定
    private void AttackScale()
    {
        transform.localScale *= EnemyStatus._juelStatusData.sheets[0].list[_juelController.JuelNo]._juelAttackRangeMagnification;
    }


    //攻撃オブジェクトを一定時間で削除
    private async void DeleteAttack()
    {
        try{
            var token = this.GetCancellationTokenOnDestroy();
            await UniTask.Delay(EnemyStatus._juelStatusData.sheets[0].list[_juelController.JuelNo]._juelAttackDurationTime, cancellationToken: token);
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
        Vector3 _attackVector = Quaternion.AngleAxis(transform.rotation.eulerAngles.y, Vector3.up) * Vector3.right;
        transform.position += _attackVector * EnemyStatus._juelStatusData.sheets[0].list[_juelController.JuelNo]._juelAttackSpeed;
    }


    //プレイヤーへダメージ
    private void JuelPlayerAttackDamage()
    {
        int damage = EnemyStatus._juelStatusData.sheets[0].list[_juelController.JuelNo]._juelAttack - GameManager.Instance._playerStatus[GameManager.Instance.PlayerOperate].Defense;
        if (damage <= 0) damage = 1;
        GameManager.Instance._playerStatus[GameManager.Instance.PlayerOperate].Hp -= damage;
    }

    //プレイヤーヒット時、ダメージ。プレイヤーの攻撃で消せる
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            JuelPlayerAttackDamage();
            Destroy(gameObject);
        }else if(other.gameObject.tag == "PlayerAttack")
        {
            //プレイヤーの攻撃を破壊
            Destroy(other.gameObject);
        }
    }
}
