using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicStoneAmountModel : MonoBehaviour
{
    [SerializeField]
    private Text[] _eachMagicStoneAmountText;
    [SerializeField]
    private Text _sumMagicStoneAmountText;

    // Start is called before the first frame update
    void Start()
    {
        SetMagicStoneText();
    }

    //残りの魔石を表示
    public void RemainMagicStoneDisplay(UseMagicStoneStruct _useMagicStoneStruct)
    {
        for(int i = 0;i < ConstValue._stoneKind; i++)
        {
            //魔石を使用する場合はテキストを赤にして、消費後の数を表示
            DecreaseDisplay(_eachMagicStoneAmountText[i], GameManager.Instance._shareItem._magicStone[i].Amount, _useMagicStoneStruct._useMagicStone[i]);
        }

        DecreaseDisplay(_sumMagicStoneAmountText, GameManager.Instance._shareItem.SumMagicStone(), _useMagicStoneStruct._useSumMagicStone);
    }


    //テキストの色を変えて、テキストの値を変更
    private void DecreaseDisplay(Text _magicStoneAmountText, int _magicStoneAmount, int _useMagicSctoneAmount)
    {
        _magicStoneAmountText.text = (_magicStoneAmount - _useMagicSctoneAmount).ToString();

        if(_useMagicSctoneAmount > 0)
        {
            _magicStoneAmountText.color = new Color(ConstValue._decreaseColorR, ConstValue._decreaseColorG, ConstValue._decreaseColorB, ConstValue._decreaseColorA);
        }
        else
        {
            _magicStoneAmountText.color = new Color(ConstValue._normalColorR, ConstValue._normalColorG, ConstValue._normalColorB, ConstValue._normalColorA);
        }
        
    }

    
    //魔石を消費
    public void UseMagicStone(UseMagicStoneStruct _useMagicStoneStruct)
    {
        for(int i = 0; i < ConstValue._stoneKind; i++)
        {
            GameManager.Instance._shareItem._magicStone[i].Amount = GameManager.Instance._shareItem._magicStone[i].Amount - _useMagicStoneStruct._useMagicStone[i];
        }

        SetMagicStoneText();
    }

    //現在の魔石数量をテキストにセットし、テキストの色を元に戻す
    public void SetMagicStoneText()
    {
        for(int i= 0; i < ConstValue._stoneKind; i++)
        {
            _eachMagicStoneAmountText[i].text = GameManager.Instance._shareItem._magicStone[i].Amount.ToString();
            _eachMagicStoneAmountText[i].color = new Color(ConstValue._normalColorR, ConstValue._normalColorG, ConstValue._normalColorB, ConstValue._normalColorA);
        }

        _sumMagicStoneAmountText.text = GameManager.Instance._shareItem.SumMagicStone().ToString();
        _sumMagicStoneAmountText.color = new Color(ConstValue._normalColorR, ConstValue._normalColorG, ConstValue._normalColorB, ConstValue._normalColorA);
    }
}
