using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//日付テキストを管理
public class DayValueModel : MonoBehaviour
{
    [SerializeField]
    private Text _dayText;

    // Start is called before the first frame update
    void Start()
    {
        SetText();
    }


    public void SetText()
    {
        _dayText.text = GameManager._dayManage.Day.ToString();
    }
}
