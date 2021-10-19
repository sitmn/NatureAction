using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerController : MonoBehaviour
{
    private IInputPlayerProvider _inputPlayerProvider;
    private PlayerMoveScript _playerMoveScr;

    
    //プレイヤーアニメーション
    [SerializeField]
    private PlayerAnimation _playerAnimation;

    // Start is called before the first frame update
    void Start()
    {
        _inputPlayerProvider = GetComponent<IInputPlayerProvider>();

        _playerMoveScr = GetComponent<PlayerMoveScript>();

        //プレイヤー関係のストリームを発行
        InputPlayerStream();
    }


    //プレイヤーストリームの発行
    private void InputPlayerStream()
    {
        _inputPlayerProvider.
            MoveVector.
            Subscribe((x) =>
            {
                _playerMoveScr.PlayerMove(x);
                if (x != Vector3.zero)
                {
                    _playerMoveScr.PlayerRotate(x);
                }

                if (x != Vector3.zero)
                {
                    _playerAnimation.MoveAnimation(GameManager._playerOperate, true);
                }
                else
                {
                    _playerAnimation.MoveAnimation(GameManager._playerOperate, false);
                }
            }).AddTo(this);

        _inputPlayerProvider
            .Attack
            .Subscribe((_) => 
            {
                _playerAnimation.AttackAnimation(GameManager._playerOperate);
            }).AddTo(this);

        _inputPlayerProvider
            .Skill
            .Subscribe((_) =>
            {
                _playerAnimation.SkillAnimation(GameManager._playerOperate);
            }).AddTo(this);

        _inputPlayerProvider
            .SkillChange
            .Subscribe((_) =>
            {
                SkillChange();
            }).AddTo(this);

        _inputPlayerProvider
            .CharChange
            .Subscribe((_) =>
            {
                PlayerCharChange();
            }).AddTo(this);
    }


    //プレイヤーのスキル切り替え
    public void SkillChange()
    {
        Debug.Log("SkillChange");
    }

    //プレイヤーのキャラ切り替え
    private void PlayerCharChange()
    {
        Debug.Log("CharChange");
    }

}
