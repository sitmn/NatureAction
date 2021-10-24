using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus:MonoBehaviour
{
    public static EnemyStatusData _enemyStatusData;
    public static JuelStatusData _juelStatusData;


    private void Awake()
    {
        _enemyStatusData = Resources.Load("EnemyStatusData") as EnemyStatusData;
        _juelStatusData = Resources.Load("JuelStatusData") as JuelStatusData;
        Debug.Log(_juelStatusData);
    }
}
