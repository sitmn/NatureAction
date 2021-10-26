﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCollider : MonoBehaviour
{

    //敵へのダメージ
    private void DamageEnemy(EnemyController _enemyController)
    {
        int _power;

        //スキルの時、スキル倍率を攻撃力にプラス
        if (gameObject.tag == "Attack") _power = GameManager.Instance._playerStatus[GameManager.Instance.PlayerOperate].Attack;
        else _power = (int)((float)(GameManager.Instance._playerStatus[GameManager.Instance.PlayerOperate].Attack)
                * GameManager.Instance._playerStatus[GameManager.Instance.PlayerOperate]._playerSkill[GameManager.Instance._playerStatus[GameManager.Instance.PlayerOperate].PlayerUseSkillNo].SkillAttackMagnification);

        int _damage = _power - EnemyStatus._enemyStatusData.sheets[0].list[_enemyController.EnemyNo]._enemyDefense;
        if (_damage <= 0) _damage = 1;

        _enemyController.EnemyHp -= _damage;
    }

    //敵へのダメージ
    private void DamageJuel(JuelController _juelController)
    {
        int _power;

        //スキルの時、スキル倍率を攻撃力にプラス
        if (gameObject.tag == "Attack") _power = GameManager.Instance._playerStatus[GameManager.Instance.PlayerOperate].Attack;
        else _power = (int)((float)(GameManager.Instance._playerStatus[GameManager.Instance.PlayerOperate].Attack) 
                * GameManager.Instance._playerStatus[GameManager.Instance.PlayerOperate]._playerSkill[GameManager.Instance._playerStatus[GameManager.Instance.PlayerOperate].PlayerUseSkillNo].SkillAttackMagnification);

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
            Debug.Log("SSSS");
            if (other.gameObject.GetComponent<JuelController>() != null)
            {
                DamageJuel(other.gameObject.GetComponent<JuelController>());
            }
        }
    }
}
