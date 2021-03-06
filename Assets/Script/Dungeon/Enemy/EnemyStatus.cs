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
        _enemyStatusData = StaticAssets.SetAssetValue4(_enemyStatusData);
        _juelStatusData = Resources.Load("JuelStatusData") as JuelStatusData;
        _juelStatusData = StaticAssets.SetAssetValue2(_juelStatusData);
    }
}
