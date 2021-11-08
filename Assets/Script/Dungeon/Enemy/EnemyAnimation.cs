using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

public class EnemyAnimation : MonoBehaviour
{/*
    private Animator _enemyAnimation;

    //スキルに応じたアニメーションクリップの差し替え
    private AnimatorOverrideController _animatorOverrideController;


    //攻撃判定
    [SerializeField]
    private GameObject _attackColliderObj;

    private void Awake()
    {
        _enemyAnimation = GetComponent<Animator>();
    }

    public void MoveAnimation(int PlayerOperate, bool _animationBool)
    {
        _enemyAnimation.SetBool("move", _animationBool);
    }

    public void AttackAnimation(int PlayerOperate)
    {
        _playerAnimation.SetTrigger("AttackTri");
    }


    public void SkillAnimation(int PlayerOperate)
    {
        Debug.Log(GameManager.Instance._playerStatus[GameManager.Instance.PlayerOperate].PlayerUseSkillNo + "No");
        _playerAnimation.SetTrigger("SkillTri");
    }

    public void SkillAnimationChange(PlayerStatus _playerStatus)
    {
        ChangeSkillCollider(_playerStatus);
        ChangeClip(_playerStatus);
        //ChangeDisplay();
    }

    //攻撃用コライダーの入れ替え(既についているものを削除して、新しいオブジェクトを生成)
    private void ChangeSkillCollider(PlayerStatus _playerStatus)
    {
        Destroy(_skillColliderObj);
        _skillColliderObj = Instantiate(_playerStatus._playerSkill[_playerStatus.PlayerUseSkillNo].SkillColliderObj, this.transform.position + new Vector3(0, 0.5f, 0), this.transform.rotation * Quaternion.AngleAxis(90f, Vector3.right));
        _skillColliderObj.transform.parent = this.gameObject.transform;
        if (_skillColliderObj.name != "SkillTypeC(Clone)")
        {
            _skillColliderObj.transform.localPosition += new Vector3(0, 0, 1);
        }
        _skillColliderObj.SetActive(false);
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

    /*
    //攻撃実行
    public async UniTask AttackStart()
    {
        _attackColliderObj.SetActive(true);
        await UniTask.Delay(TimeSpan.FromMilliseconds(
            GameManager.Instance._playerStatus[GameManager.Instance.PlayerOperate].AttackDurationTime));
        _attackColliderObj.SetActive(false);
    }

    //プレイヤーのスキル
    public async UniTask SkillStart()
    {
        _skillColliderObj.SetActive(true);
        await UniTask.Delay(TimeSpan.FromMilliseconds(
            GameManager.Instance._playerStatus[GameManager.Instance.PlayerOperate]
                       ._playerSkill[GameManager.Instance._playerStatus[GameManager.Instance.PlayerOperate].PlayerUseSkillNo].SkillDurationTime));
        _skillColliderObj.SetActive(false);
    }*/
}
