using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ボタン用基盤クラス
public class ButtonViewBase : MonoBehaviour
{
    /****ボタンNo*****/
    /*0:画面遷移ボタン
     *1:修行選択ボタン（休息含む）
     *2:修行実行ボタン
     *3:移動選択ボタン（町・ダンジョン）
     *4:移動決定ボタン
     *5:スキル選択ボタン
     *6:セーブボタン
     */
    protected int _buttonNo;
    
    public int ButtonNo { 
        get { return _buttonNo; } 
        set { _buttonNo = value; } 
    }
    
    protected bool _buttonSelectFlag;
    public bool ButtonSelectFlag { 
        get { return _buttonSelectFlag; } 
        set { _buttonSelectFlag = value; } }
}
