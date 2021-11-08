using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*索敵用コライダーのスクリプト、フラグONOFFのみ*/
public class JuelSearch : MonoBehaviour
{
    private JuelController _juelController;

    // Start is called before the first frame update
    void Start()
    {
        _juelController = transform.parent.gameObject.GetComponent<JuelController>();

        //宝石毎の索敵範囲を設定
        gameObject.GetComponent<SphereCollider>().radius = EnemyStatus._juelStatusData.sheets[0].list[_juelController.JuelNo]._juelSearchRange;
    }

    //プレイヤーが離れるのを検知
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("FFF");
        if (other.gameObject.tag == "Player")
        {
            _juelController._searchEnemyFlag = false;
        }
    }

    //プレイヤーの接近を検知
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("SSS");
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("AAA");
            _juelController._searchEnemyFlag = true;
        }
    }
}
