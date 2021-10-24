using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

//宝石の攻撃、ダメージや攻撃Objのスピード
public class JuelAttack : MonoBehaviour
{
    private JuelController _juelController;


    // Start is called before the first frame update
    void Start()
    {
        _juelController = transform.parent.gameObject.GetComponent<JuelController>();
        DeleteAttack();
    }

    //攻撃オブジェクトを一定時間で削除
    private async void DeleteAttack()
    {
        await UniTask.Delay(EnemyStatus._juelStatusData.sheets[0].list[_juelController._juelNo]._juelAttackDurationTime);
        Destroy(gameObject);
    }



    // Update is called once per frame
    void Update()
    {
        if(transform.rotation.y == 0)
        {
            transform.position += new Vector3(EnemyStatus._juelStatusData.sheets[0].list[_juelController._juelNo]._juelAttackSpeed, 0, 0);
        }else if (transform.rotation.y == 90f)
        {
            transform.position += new Vector3(0, 0, -EnemyStatus._juelStatusData.sheets[0].list[_juelController._juelNo]._juelAttackSpeed);
        }
        else if (transform.rotation.y == 180f)
        {
            transform.position += new Vector3(-EnemyStatus._juelStatusData.sheets[0].list[_juelController._juelNo]._juelAttackSpeed, 0, 0);
        }
        else if (transform.rotation.y == 270f)
        {
            transform.position += new Vector3(0, 0, EnemyStatus._juelStatusData.sheets[0].list[_juelController._juelNo]._juelAttackSpeed);
        }
    }


    private void JuelPlayerAttackDamage()
    {
        Debug.Log("プレイヤーダメージ");
        int damage = EnemyStatus._juelStatusData.sheets[0].list[_juelController._juelNo]._juelAttack - GameManager.Instance._playerStatus[GameManager.Instance.PlayerOperate].Defense;
        if (damage <= 0) damage = 1;
        GameManager.Instance._playerStatus[GameManager.Instance.PlayerOperate].Hp.Value -= damage;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            JuelPlayerAttackDamage();
            Destroy(gameObject);
        }
    }
}
