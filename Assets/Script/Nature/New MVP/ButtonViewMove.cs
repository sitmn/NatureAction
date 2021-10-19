using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    //ボタンImage
    private Image buttonImage;
    //ボタンの色
    private Color thisColor;
    //ボタン元の色
    private Color originColor;
    //時間
    private float time;

    //ボタン点滅スピード
    private float buttonColorSpeed;

    //ボタン判定用
    void Start()
    {
        _buttonNo = 3;

        buttonColorSpeed = ConstValue._buttonBlinkTime;

        buttonImage = this.GetComponent<Image>();
        thisColor = this.GetComponent<Image>().color;
        //キャンセル時、元の色に戻すため、色を格納しておく
        originColor = thisColor;
    }


    void Update()
    {
        ButtonBlink();
    }

    private void ButtonBlink()
    {
        if (_buttonSelectFlag == true)
        {
            //時間でボタンの色のアルファ値のみ切り替え
            time += Time.deltaTime * 5.0f * buttonColorSpeed;
            thisColor.a = Mathf.Sin(time) * 0.5f + 0.5f;
            buttonImage.color = thisColor;
        }
        else
        {
            buttonImage.color = originColor;
        }
    }

    private void OnDisable()
    {
        _buttonSelectFlag = false;

    }
}
