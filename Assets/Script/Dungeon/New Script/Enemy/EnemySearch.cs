using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySearch : MonoBehaviour
{
    private EnemyController _enemyController;

    // Start is called before the first frame update
    void Start()
    {
        _enemyController = transform.parent.gameObject.GetComponent<EnemyController>();

        //敵毎の索敵範囲を設定
        gameObject.GetComponent<SphereCollider>().radius = EnemyStatus._enemyStatusData.sheets[0].list[_enemyController.EnemyNo]._enemySearchRange;
    }

    //プレイヤーが離れるのを検知
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _enemyController._searchEnemyFlag = false;
        }
    }

    //プレイヤーの接近を検知
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _enemyController._searchEnemyFlag = true;
        }
    }
}
