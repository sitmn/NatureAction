using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx.Triggers;
using UniRx;
using System;

public class JuelController : MonoBehaviour
{
    [SerializeField]
    private GameObject _actionObj;  //攻撃用のオブジェクト

    public int _juelNo;   //宝石番号（宝石名の整理）
    public bool _searchEnemyFlag;   //索敵範囲にプレイヤーまたは敵がいるか


    // Start is called before the first frame update
    void Start()
    {
        _searchEnemyFlag = false;

        //攻撃用ストリーム作成
        this.UpdateAsObservable()
            .Where((_) => _searchEnemyFlag)
            .ThrottleFirst(TimeSpan.FromMilliseconds(5000))
            .Subscribe((_) => JuelAction())
            .AddTo(this);
    }

    //一定時間毎に攻撃を実行
    private void JuelAction()
    {
        Debug.Log(transform.rotation * Vector3.right);
        Vector3 attackPos = transform.rotation * Vector3.right;
        Debug.Log(attackPos);
        /*if (transform.rotation.y == 0)
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
        }*/
        GameObject childObj = Instantiate(_actionObj, attackPos, Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0));
        childObj.transform.parent = transform;
    }
}
