using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsumeStoneAmountModel : MonoBehaviour
{
    [SerializeField]
    private Text[] _consumeMagicStoneText;


    //スキル習得にかかる魔石消費量を表示
    public void DisplayConsumeStone(int _skillNo, SkillData _skillData)
    {
        _consumeMagicStoneText[0].text = _skillData.sheets[0].list[_skillNo]._consumePurpleStone.ToString();
        _consumeMagicStoneText[1].text = _skillData.sheets[0].list[_skillNo]._consumeRedStone.ToString();
        _consumeMagicStoneText[2].text = _skillData.sheets[0].list[_skillNo]._consumeBlueStone.ToString();
        _consumeMagicStoneText[3].text = _skillData.sheets[0].list[_skillNo]._consumeGreenStone.ToString();
        _consumeMagicStoneText[4].text = _skillData.sheets[0].list[_skillNo]._consumeYellowStone.ToString();
    }
}
