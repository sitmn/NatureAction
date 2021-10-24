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
    }

    //プレイヤーが離れるのを検知
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _juelController._searchEnemyFlag = false;
        }
    }

    //プレイヤーの接近を検知
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _juelController._searchEnemyFlag = true;
        }
    }
}
