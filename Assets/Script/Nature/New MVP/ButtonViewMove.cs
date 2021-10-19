using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonViewMove : ButtonViewBase
{
    //行先の番号
    /*0:町A
     *1:町B 
     *2:町C
     * 
     * 
     * 
     * 
     * 
     * 
     * 
     *101:ダンジョンA
     *102:ダンジョンB
     *103:ダンジョンC
     */
    [SerializeField]
    private int _destinationNo;

    public int DestinationNo
    {
        get{ return _destinationNo; }
    }

    //ボタン判定用
    void Start()
    {
        _buttonNo = 3;
    }
}
