using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//スキル画面用スクリプト
public class SkillScript
{
    //スキルの習得
    public void SkillObtain(SkillData _skillData, int _playerNo, int _skillNo)
    {
        GameManager.Instance._playerStatus[_playerNo]._playerSkillList.Add(new Skill(_skillData.sheets[0].list[_skillNo]._skillName,
                                                                            _skillData.sheets[0].list[_skillNo]._skillMP,
                                                                            _skillData.sheets[0].list[_skillNo]._skillAttackMagnification,
                                                                            _skillData.sheets[0].list[_skillNo]._skillAnimationLength,
                                                                            _skillData.sheets[0].list[_skillNo]._skillEndTime,
                                                                            _skillData.sheets[0].list[_skillNo]._skillDurationTime,
                                                                            _skillData.sheets[0].list[_skillNo]._skillAttackRange,
                                                                            _skillData.sheets[0].list[_skillNo]._skillSpeed,
                                                                            _skillData.sheets[0].list[_skillNo]._skillTypeName,
                                                                            _skillData.sheets[0].list[_skillNo]._skillAnimation,
                                                                            _skillData.sheets[0].list[_skillNo]._skillColliderObj,
                                                                            _skillData.sheets[0].list[_skillNo]._skillImage,
                                                                            _skillData.sheets[0].list[_skillNo]._skillAudioClip));
    }
    //使用スキルにセット
    public void SkillSet(int _playerNo, int _setSkillNo, int _removeSkillNo)
    {
        GameManager.Instance._playerStatus[_playerNo]._playerSkill[_removeSkillNo] = GameManager.Instance._playerStatus[_playerNo]._playerSkillList[_setSkillNo];
    }
}
