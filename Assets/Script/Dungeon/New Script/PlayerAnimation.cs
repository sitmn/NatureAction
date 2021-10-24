
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
    //スキルの攻撃判定
    private GameObject _skillColliderObj;

    private void Awake()
    {
        _playerAnimation = GetComponent<Animator>();
        _animatorOverrideController = new AnimatorOverrideController();
        OverrideAnimationClipSet();
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

    public void AttackAnimation(int PlayerOperate)
    {
        _playerAnimation.SetTrigger("AttackTri");
    }


    public void SkillAnimation(int PlayerOperate)
    {
        Debug.Log(GameManager.Instance._playerStatus[GameManager.Instance.PlayerOperate].PlayerUseSkillNo+"No");
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
        _skillColliderObj = Instantiate(_playerStatus._playerSkill[_playerStatus.PlayerUseSkillNo].SkillColliderObj, this.transform.position, this.transform.rotation * Quaternion.AngleAxis(90f, Vector3.right));
        _skillColliderObj.transform.parent = this.gameObject.transform;
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
    }


    //攻撃をくらったとき
    public void damagePlayer(int damage)
    {
        GameManager.Instance._playerStatus[GameManager.Instance.PlayerOperate].Hp.Value -= damage;
        if (GameManager.Instance._playerStatus[GameManager.Instance.PlayerOperate].Hp.Value <= 0)
        {
            Destroy(gameObject);
        }
    }
    /*
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag + "タグ");
        //敵に攻撃が当たった場合
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("B");
            if (other.gameObject.GetComponent<EnemyController>() != null)
            {
                Debug.Log("C");
                playerAttackDamage(other.gameObject.GetComponent<EnemyController>());
            }
        }
    }*/


    //敵へのダメージを計算
    private void playerAttackDamage(EnemyController enemyController)
    {
        int damage = playerStatus.player_Attack - enemyController.thisEnemyState.Get_defense();

        enemyController.damageEnemy(damage);
    }
}
