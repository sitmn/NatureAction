using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BlinkHPBar : MonoBehaviour
{
    private Image blinkHpImage;

    // Start is called before the first frame update
    void Start()
    {
        blinkHpImage = this.gameObject.GetComponent<Image>();

        BarBlink();
    }

    // Update is called once per frame
    private void BarBlink()
    {
        DOTween.ToAlpha(() => blinkHpImage.color,
                         x => blinkHpImage.color = x, 0, ConstValue._hPBarBlinkTime)
                        .SetEase(Ease.InCirc)
                        .SetLoops(-1, LoopType.Yoyo);
    }
}
