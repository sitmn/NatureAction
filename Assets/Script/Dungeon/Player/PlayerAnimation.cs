
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

public class PlayerAnimation : MonoBehaviour
{
    private Animator _playerAnimation;

    //スキルに応じたアニメーションクリップの差し替え
    private AnimatorOverrideController _animatorOverrideController;


    //攻撃判定
    [SerializeField]
    private GameObject _attackColliderObj;

    //SE
    private SE _playerSE;

    private void Awake()
    {
        _playerAnimation = GetComponent<Animator>();
        _animatorOverrideController = new AnimatorOverrideController();
        OverrideAnimationClipSet();

        _playerSE = GetComponent<SE>();
    }

    private void OverrideAnimationClipSet()
    {
        _animatorOverrideController = new AnimatorOverrideController();
        _animatorOverrideController.runtimeAnimatorController = _playerAnimation.runtimeAnimatorController;
        _playerAnimation.runtimeAnimatorController = _animatorOverrideController;
    }

    public void MoveAnimation(int PlayerOperate, bool _animationBool)
    {
        _playerAnimation.SetBool("move", _animationBool);
    }

    public void AttackAnimation(int _playerOperate)
    {
        _playerAnimation.SetTrigger("AttackTri");
    }


    public void SkillAnimation(int _playerOperate)
    {
        _playerAnimation.SetTrigger("SkillTri");
    }

    public void SkillAnimationChange(PlayerStatus _playerStatus)
    {
        ChangeClip(_playerStatus);
    }

    //使用スキルに応じたアニメーションクリップを差し替え
    private void ChangeClip(PlayerStatus _playerStatus)
    {
        // ステートをキャッシュ
        AnimatorStateInfo[] layerInfo = new AnimatorStateInfo[_playerAnimation.layerCount];
        for (int i = 0; i < _playerAnimation.layerCount; i++)
        {
            layerInfo[i] = _playerAnimation.GetCurrentAnimatorStateInfo(i);
        }

        // AnimationClipを差し替えて、強制的にアップデート
        // ステートがリセットされる
        _animatorOverrideController["Skill"]
            = _playerStatus._playerSkill[_playerStatus.PlayerUseSkillNo].SkillAnimation;
        _playerAnimation.SetFloat("AnimationSpeed", _playerStatus._playerSkill[_playerStatus.PlayerUseSkillNo].SkillAnimationLength);
        _playerAnimation.Update(0.0f);

        // ステートを戻す
        for (int i = 0; i < _playerAnimation.layerCount; i++)
        {
            _playerAnimation.Play(layerInfo[i].fullPathHash, i, layerInfo[i].normalizedTime);
        }
    }

    /***********アニメーションが呼び出すメソッド*****************/


    //攻撃実行
    public void AttackStart()
    {
        //攻撃効果音
        if(GameManager.Instance._playerStatus[GameManager.Instance.PlayerOperate]._attackAudio != null)
        {
            _playerSE.PlaySE(GameManager.Instance._playerStatus[GameManager.Instance.PlayerOperate]._attackAudio);
        }

        GameObject _attackObj = Instantiate(GameManager.Instance._playerStatus[GameManager.Instance.PlayerOperate]._attackColliderObj, transform.forward, Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0));
        //弾の大きさに応じて敵から離れた位置に弾を生成
        _attackObj.transform.position = transform.position + transform.forward + new Vector3(0, 0.5f, 0);
    }

    //プレイヤーのスキル
    public void SkillStart()
    {
        //スキル効果音
        if (GameManager.Instance._playerStatus[GameManager.Instance.PlayerOperate]._playerSkill[GameManager.Instance._playerStatus[GameManager.Instance.PlayerOperate].PlayerUseSkillNo].SkillAudioClip != null)
        {
            _playerSE.PlaySE(GameManager.Instance._playerStatus[GameManager.Instance.PlayerOperate]._playerSkill[GameManager.Instance._playerStatus[GameManager.Instance.PlayerOperate].PlayerUseSkillNo].SkillAudioClip);
        }

        GameObject _skillColliderObj = Instantiate(GameManager.Instance._playerStatus[GameManager.Instance.PlayerOperate]._playerSkill[GameManager.Instance._playerStatus[GameManager.Instance.PlayerOperate].PlayerUseSkillNo].SkillColliderObj, this.transform.position + new Vector3(0, 0.5f, 0), transform.rotation);

        //SkillCのみプレイヤー中心に
        if (_skillColliderObj.name == "SkillTypeC(Clone)")
        {
            _skillColliderObj.transform.position = this.gameObject.transform.position;
            _skillColliderObj.transform.rotation = _skillColliderObj.transform.rotation * Quaternion.AngleAxis(90f, Vector3.right);
        }else if(_skillColliderObj.tag == "PlayerHeal")
        {
            _skillColliderObj.transform.position = new Vector3(_skillColliderObj.transform.position.x, 0 , _skillColliderObj.transform.position.z) + this.transform.forward;
        }
        else _skillColliderObj.transform.position = this.gameObject.transform.position + transform.forward + new Vector3(0, 0.5f, 0);
    }

    /*
    //ダメージ時の仰け反り
    public void DamageStart()
    {
        _coolTimeFlag = false;
    }*/
}
