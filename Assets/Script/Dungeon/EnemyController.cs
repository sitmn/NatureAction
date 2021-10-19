using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public EnemyState thisEnemyState;


    // Start is called before the first frame update
    void Start()
    {
        //この敵のステータスを取得
        thisEnemyState = new EnemyState(this.gameObject.tag);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void damageEnemy(int damage)
    {
        thisEnemyState.Set_hp(thisEnemyState.Get_hp() - damage);
        if(thisEnemyState.Get_hp() <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
