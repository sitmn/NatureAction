using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonViewScreenChange : ButtonViewBase
{
    //遷移先の画面
    public CanvasGroup NextCanvasGroup;

    //ボタン判定用
    void Start()
    {
        _buttonNo = 0;
    }
}
