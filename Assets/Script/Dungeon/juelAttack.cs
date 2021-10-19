using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//攻撃にアタッチするスクリプト
public class juelAttack : MonoBehaviour
{
    private int deleteCount;
    private int count;

    private float rotation_y;

    private JuelController juelController;


    // Start is called before the first frame update
    void Start()
    {
        deleteCount = 1000;
        count = 0;

        rotation_y = transform.rotation.eulerAngles.y;

        juelController = transform.parent.gameObject.GetComponent<JuelController>();
            
    }

    // Update is called once per frame
    void Update()
    {
        

        count++;
        Debug.Log(rotation_y + "YY");
        if(rotation_y == 0)
        {
            Debug.Log("A");
            transform.position += new Vector3(juelController.juelStatus.juel_AttackSpeed, 0, 0);
        }else if (rotation_y == 90f)
        {
            Debug.Log("B");
            transform.position += new Vector3(0, 0, -juelController.juelStatus.juel_AttackSpeed);
        }
        else if (rotation_y == 180f)
        {
            Debug.Log("C");
            transform.position += new Vector3(-juelController.juelStatus.juel_AttackSpeed, 0, 0);
        }
        else if (rotation_y == 270f)
        {
            Debug.Log("D");
            transform.position += new Vector3(0, 0, juelController.juelStatus.juel_AttackSpeed);
        }

        if(count > deleteCount)
        {
            Destroy(gameObject);
        }
    }


    //攻撃ヒット時
    private void juelEnemyAttackDamage(EnemyController enemyController)
    {
        int damage = juelController.juelStatus.juel_Attack - enemyController.thisEnemyState.Get_defense();

        enemyController.damageEnemy(damage);
    }

    private void juelPlayerAttackDamage(PlayerController2 playerController)
    {
        int damage = transform.parent.gameObject.GetComponent<JuelController>().juelStatus.juel_Attack - playerStatus.player_Defense;

        //playerController.damagePlayer(damage);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("aaaaa");
            juelPlayerAttackDamage(other.gameObject.GetComponent<PlayerController2>());
            Destroy(gameObject);
        }
        else if(other.gameObject.tag == "Enemy")
        {
            juelEnemyAttackDamage(other.gameObject.GetComponent<EnemyController>());
            Destroy(gameObject);
        }

        
        
    }
}
