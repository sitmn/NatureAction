using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

public class PlayerSkillCollider : MonoBehaviour
{
    private int _skillUseNo;
    private int _playerOperate;

    private void Start()
    {
        //攻撃発生時に、各値を固定
        _skillUseNo = GameManager.Instance._playerStatus[GameManager.Instance.PlayerOperate].PlayerUseSkillNo;
        _playerOperate = GameManager.Instance.PlayerOperate;

        DeleteSkill();
    }

    void Update()
    {
        Vector3 _skillVector = Quaternion.AngleAxis(transform.rotation.eulerAngles.y - 90, Vector3.up) * Vector3.right;
        transform.position += _skillVector * GameManager.Instance._playerStatus[_playerOperate]._playerSkill[_skillUseNo].SkillSpeed;
    }

    private async void DeleteSkill()
    {
        try
        {
            var token = this.GetCancellationTokenOnDestroy();
            Debug.Log(GameManager.Instance._playerStatus[_playerOperate]._playerSkill[_skillUseNo].SkillDurationTime);
            await UniTask.Delay(GameManager.Instance._playerStatus[_playerOperate]._playerSkill[_skillUseNo].SkillDurationTime, cancellationToken: token);
            Destroy(gameObject);
        }
        catch (OperationCanceledException e)
        {

        }
    }



    //敵へのダメージ
    private void DamageEnemy(EnemyController _enemyController)
    {
        int _power;

        //スキルの時、スキル倍率を攻撃力にプラス
        if (gameObject.tag == "Attack") _power = GameManager.Instance._playerStatus[_playerOperate].Attack;
        else _power = (int)((float)(GameManager.Instance._playerStatus[_playerOperate].Attack)
                * GameManager.Instance._playerStatus[_playerOperate]._playerSkill[_skillUseNo].SkillAttackMagnification);

        int _damage = _power - EnemyStatus._enemyStatusData.sheets[0].list[_enemyController.EnemyNo]._enemyDefense;
        if (_damage <= 0) _damage = 1;

        _enemyController.EnemyHp -= _damage;
    }

    //敵へのダメージ
    private void DamageJuel(JuelController _juelController)
    {
        int _power;

        //スキルの時、スキル倍率を攻撃力にプラス
        if (gameObject.name == "AttackColliderObj(Clone)") _power = GameManager.Instance._playerStatus[_playerOperate].Attack;
        else _power = (int)((float)(GameManager.Instance._playerStatus[GameManager.Instance.PlayerOperate].Attack)
                * GameManager.Instance._playerStatus[_playerOperate]._playerSkill[_skillUseNo].SkillAttackMagnification);

        int _damage = _power - EnemyStatus._juelStatusData.sheets[0].list[_juelController.JuelNo]._juelDefense;
        if (_damage <= 0) _damage = 1;

        _juelController.JuelHp -= _damage;
    }

    void OnTriggerEnter(Collider other)
    {
        //敵に攻撃が当たった場合
        if (other.gameObject.tag == "Enemy")
        {
            if (other.gameObject.GetComponent<EnemyController>() != null)
            {
                DamageEnemy(other.gameObject.GetComponent<EnemyController>());
            }
        }
        else if (other.gameObject.tag == "Juel")
        {
            if (other.gameObject.GetComponent<JuelController>() != null)
            {
                DamageJuel(other.gameObject.GetComponent<JuelController>());
            }
        }
    }
}
