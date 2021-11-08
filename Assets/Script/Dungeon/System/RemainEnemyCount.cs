using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RemainEnemyCount : MonoBehaviour
{
    [SerializeField]
    private Text _remainEnemyCountText;

    // Start is called before the first frame update
    void Start()
    {
        RemainEnemyCountSet();
    }

    public void RemainEnemyCountSet()
    {
        _remainEnemyCountText.text = "敵残数：" + MapManager._mapEnemyCount;
    }
}
