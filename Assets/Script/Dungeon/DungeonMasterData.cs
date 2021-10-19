using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonMasterData : MonoBehaviour
{
    public EnemyStatusData enemyStatusData;
    // Start is called before the first frame update

    private void Awake()
    {
        enemyStatusData = Resources.Load("EnemyStatusData") as EnemyStatusData;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
