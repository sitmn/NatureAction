using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class MagicStoneDisplayModel : MonoBehaviour
{
    //魔石量テキスト
    [SerializeField]
    private Text[] _magicStoneText;

    //各魔石のストリーム用、GameManagerの魔石数ではなくここを反映させる
    public static ReactiveProperty<int>[] _magicStone = new ReactiveProperty<int>[ConstValue._stoneKind];

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < ConstValue._stoneKind; i++)
        {
            _magicStone[i] = new ReactiveProperty<int>(GameManager.Instance._shareItem._magicStone[i].Amount);

            int i2 = i;
            _magicStone[i]
            .Subscribe((x) => {
                SetGameManagerStone(x, i2);
                SetMagicStoneText(i2); })
            .AddTo(this);
        }
    }

    //GameManagerの魔石数を反映
    private void SetGameManagerStone(int _magicStoneAmount,int _magicStoneNo)
    {
        GameManager.Instance._shareItem._magicStone[_magicStoneNo].Amount = _magicStoneAmount;
    }

    //現在の魔石数量をテキストにセットし、テキストの色を元に戻す
    public void SetMagicStoneText(int _magicStoneNo)
    {
        _magicStoneText[_magicStoneNo].text = GameManager.Instance._shareItem._magicStone[_magicStoneNo].Amount.ToString();
    }
}
