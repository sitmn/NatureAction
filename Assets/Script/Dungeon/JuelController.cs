using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuelController : MonoBehaviour
{
    public GameObject actionObj;  //攻撃用のオブジェクト

    public juelStatus juelStatus;   //宝石のステータス

    public int juelNo;   //宝石番号（宝石名の整理）

    public bool searchEnemyBool;   //索敵範囲にプレイヤーまたは敵がいるか
    private int actionSpan;         //攻撃間隔
    private int actionCount;        //攻撃間隔カウント

    //コンストラクタ、作成宝石Noの設定
    public JuelController(int No){
        juelNo = No;
        }

    // Start is called before the first frame update
    void Start()
    {
        if(juelNo == 1)
        {
            juelStatus = new rubyStatus();
        }else if(juelNo == 2)
        {
            juelStatus = new aquamarineStatus();
        }
        else
        {
            Debug.Log("エラー：宝石Noが存在しない");
        }
        searchEnemyBool = false;

        actionSpan = juelStatus.juel_AttackSpan;

        actionCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (searchEnemyBool)
        {
            juelAction();
        }


    }

    //一定時間（actionCount）毎に攻撃を実行
    private void juelAction()
    {
        actionCount++;

        if (actionCount >= actionSpan)
        {
            Vector3 attackPos = transform.position;
            if (transform.rotation.y == 0)
            {
                attackPos.x += 1;
            }
            else if(transform.rotation.y == 90f)
            {
                attackPos.z += -1;
            }
            else if (transform.rotation.y == 180f)
            {
                attackPos.x += -1;
            }
            else if (transform.rotation.y == 270f)
            {
                attackPos.z += 1;
            }
            GameObject childObj = Instantiate(actionObj, attackPos, Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0));
            childObj.transform.parent = transform;

            actionCount = 0;
        }
    }
}
