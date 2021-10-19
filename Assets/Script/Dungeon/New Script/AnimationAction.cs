
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

public class AnimationAction : MonoBehaviour
{
    //攻撃判定
    [SerializeField]
    private GameObject _attackObj;
    //スキルの攻撃判定
    [SerializeField]
    private GameObject _skillObj;

    //攻撃実行
    public async UniTask AttackStart()
    {
        _attackObj.SetActive(true);
        await UniTask.Delay(TimeSpan.FromMilliseconds(
            GameManager._playerStatus[GameManager._playerOperate].AttackDurationTime));
        _attackObj.SetActive(false);
    }

    //プレイヤーのスキル
    public async UniTask SkillStart()
    {
        _skillObj.SetActive(true);
        await UniTask.Delay(TimeSpan.FromMilliseconds(
            GameManager._playerStatus[GameManager._playerOperate]._playerSkill[GameManager._playerUseSkillNo].SkillDurationTime));
        _skillObj.SetActive(false);
    }

    //敵へのダメージを計算
    private void playerAttackDamage(EnemyController enemyController)
    {
        int damage = playerStatus.player_Attack - enemyController.thisEnemyState.Get_defense();

        enemyController.damageEnemy(damage);
    }

    //攻撃をくらったとき
    public void damagePlayer(int damage)
    {
        playerStatus.player_HP = playerStatus.player_HP - damage;
        if (playerStatus.player_HP <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag + "タグ");
        //敵に攻撃が当たった場合
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("B");
            if (other.gameObject.GetComponent<EnemyController>() != null)
            {
                Debug.Log("C");
                playerAttackDamage(other.gameObject.GetComponent<EnemyController>());
            }
        }
    }
}
