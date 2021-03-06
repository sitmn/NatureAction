using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonViewChangeSkill : ButtonViewBase
{
    //入れ替えるスキルの番号
    private int _skillNo;
    public int SkillNo
    {
        get { return _skillNo; }
        set { _skillNo = value; }
    }

    //スキルを入れ替えるキャラの番号
    private int _playerOperate;
    public int PlayerOperate
    {
        get { return _playerOperate; }
        set { _playerOperate = value; }
    }


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
        _buttonNo = 6;

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
