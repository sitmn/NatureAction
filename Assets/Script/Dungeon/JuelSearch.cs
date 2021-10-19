using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*索敵用コライダーのスクリプト、フラグONOFFのみ*/
public class juelSearch : MonoBehaviour
{
    private JuelController juelController;

    // Start is called before the first frame update
    void Start()
    {
        juelController = transform.parent.gameObject.GetComponent<JuelController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Player")
        {
            juelController.searchEnemyBool = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Player")
        {
            Debug.Log("bbbb");
            juelController.searchEnemyBool = true;
        }
    }
}
