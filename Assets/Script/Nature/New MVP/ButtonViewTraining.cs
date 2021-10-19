using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonViewTraining : ButtonViewBase
{
    //修行タイプ用のNo
    /*0:体力修行
      1:攻撃修行
      2:防御修行
      3:素早さ修行
      4:索敵修行
      5:
      6:
      7:
      8:
     */
    [SerializeField]
    private int _trainingNo;
    public int TrainingNo { get { return _trainingNo; } }

    //プレイヤー1の修行ボタンか（true:プレイヤー1, false:プレイヤー2）
    [SerializeField]
    private bool _ownerIsPlayer2TrainingButtonFlag;
    public bool OwnerIsPlayer2TrainingButtonFlag { get { return _ownerIsPlayer2TrainingButtonFlag; } }

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
        _buttonNo = 1;

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
