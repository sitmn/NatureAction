using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillIcon : MonoBehaviour
{
    [SerializeField]
    private Image[] _skillIcon;
    [SerializeField]
    private Transform _selectIconTr;

    // Start is called before the first frame update
    void Start()
    {
        //シーン開始時のキャラスキルアイコンをセット
        SkillIconSet(0);
    }

    
    //プレイヤーのスキルアイコンセット
    public void SkillIconSet(int _playerOperate)
    {
        for(int i =0; i < _skillIcon.Length; i++)
        {
            _skillIcon[i].sprite = GameManager.Instance._playerStatus[_playerOperate]._playerSkill[i].SkillImage;
        }
        _selectIconTr.localPosition = new Vector3(-ConstValue._skillIconDistance, 0);
    }

    //スキル選択時のアイコン移動
    public void SelectIconMove()
    {
        _selectIconTr.localPosition = new Vector3(ConstValue._skillIconDistance * (GameManager.Instance._playerStatus[GameManager.Instance.PlayerOperate].PlayerUseSkillNo - 1), 0, 0);
    }
}
