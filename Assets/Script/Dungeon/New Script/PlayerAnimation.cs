using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField]
    private Animator[] _playerAnimation;


    public void MoveAnimation(int _playerOperate, bool _animationBool)
    {
        _playerAnimation[_playerOperate].SetBool("move", _animationBool);
        
    }

    public void AttackAnimation(int _playerOperate)
    {
        //_playerAnimation[_playerOperate].SetBool("Attack", _animationBool);
        _playerAnimation[_playerOperate].SetTrigger("AttackTri");
    }


    public void SkillAnimation(int _playerOperate)
    {
        _playerAnimation[_playerOperate].SetTrigger("SkillTri");
    }
}
