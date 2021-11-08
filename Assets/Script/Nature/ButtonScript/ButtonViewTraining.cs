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
    private Image _buttonImage;
    //ボタンの色
    private Color _thisColor;
    //ボタン元の色
    private Color _originColor;
    //時間
    private float _time;

    //ボタン点滅スピード
    private float _buttonColorSpeed;

    //ボタン判定用
    void Start()
    {
        _buttonNo = 1;

        _buttonColorSpeed = ConstValue._buttonBlinkTime;

        _buttonImage = this.GetComponent<Image>();
        _thisColor = this.GetComponent<Image>().color;
        //キャンセル時、元の色に戻すため、色を格納しておく
        _originColor = _thisColor;
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
            _time += Time.deltaTime * 5.0f * _buttonColorSpeed;
            _thisColor.a = Mathf.Sin(_time) * 0.5f + 0.5f;
            _buttonImage.color = _thisColor;
        }
        else
        {
            _buttonImage.color = _originColor;
        }
    }

    private void OnDisable()
    {
        _buttonSelectFlag = false;
        
    }
}
