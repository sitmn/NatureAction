using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Gitから落とすとScriptableObjectから値が外れるので、ここで再セット
public class StaticAssets : MonoBehaviour
{
    [SerializeField]
    private AnimationClip[] _skillAnimationSerialize;
    public static AnimationClip[] _skillAnimation;
    [SerializeField]
    private GameObject[] _skillCollderObjSerialize;
    public static GameObject[] _skillCollderObj;
    [SerializeField]
    private Sprite[] _skillImageSerialize;
    public static Sprite[] _skillImage;
    [SerializeField]
    private AudioClip[] _skillAudioClipSerialize;
    public static AudioClip[] _skillAudioClip;

    [SerializeField]
    private AudioClip[] _juelAttackAudioSerialize;
    public static AudioClip[] _juelAttackAudio;
    [SerializeField]
    private AudioClip[] _juelDamageAudioSerialize;
    public static AudioClip[] _juelDamageAudio;

    [SerializeField]
    private GameObject[] _initialAttackColliderObjSerialize;
    public static GameObject[] _initialAttackColliderObj;
    [SerializeField]
    private AudioClip[] _attackAudioClipSerialize;
    public static AudioClip[] _attackAudioClip;
    [SerializeField]
    private AudioClip[] _damageAudioClipSerialize;
    public static AudioClip[] _damageAudioClip;

    [SerializeField]
    private AudioClip[] _enemyAttackAudioSerialize;
    public static AudioClip[] _enemyAttackAudio;
    [SerializeField]
    private AudioClip[] _enemyDamageAudioSerialize;
    public static AudioClip[] _enemyDamageAudio;


    private void Awake()
    {
        _skillAnimation = _skillAnimationSerialize;
        _skillCollderObj = _skillCollderObjSerialize;
        _skillImage = _skillImageSerialize;
        _skillAudioClip = _skillAudioClipSerialize;

        _juelAttackAudio = _juelAttackAudioSerialize;
        _juelDamageAudio = _juelDamageAudioSerialize;

        _initialAttackColliderObj = _initialAttackColliderObjSerialize;
        _attackAudioClip = _attackAudioClipSerialize;
        _damageAudioClip = _damageAudioClipSerialize;

        _enemyAttackAudio = _enemyAttackAudioSerialize;
        _enemyDamageAudio = _enemyDamageAudioSerialize;
    }

    //Assetファイルから呼び出したものに値を入れる（SkillData）
    public static SkillData SetAssetValue1(SkillData _skillData)
    {
        for (int i = 0; i < _skillData.sheets[0].list.Count; i++)
        {
            _skillData.sheets[0].list[i]._skillAnimation = _skillAnimation[i];
            _skillData.sheets[0].list[i]._skillColliderObj = _skillCollderObj[i];
            _skillData.sheets[0].list[i]._skillImage = _skillImage[i];
            _skillData.sheets[0].list[i]._skillAudioClip = _skillAudioClip[i];
        }
        return _skillData;
    }
    
    public static JuelStatusData SetAssetValue2(JuelStatusData _juelStatusData)
    {
        for(int i = 0; i < _juelStatusData.sheets[0].list.Count; i++)
        {
            _juelStatusData.sheets[0].list[i]._juelAttackAudio = _juelAttackAudio[i];
            _juelStatusData.sheets[0].list[i]._juelDamageAudio = _juelDamageAudio[i];
        }
        return _juelStatusData;
    }
    public static InitialPlayerStatusData SetAssetValue3(InitialPlayerStatusData _initialPlayerStatusData)
    {
        for (int i = 0; i < _initialPlayerStatusData.sheets[0].list.Count; i++)
        {
            _initialPlayerStatusData.sheets[0].list[i]._initialAttackColliderObj = _initialAttackColliderObj[i];
            _initialPlayerStatusData.sheets[0].list[i]._attackAudioClip = _attackAudioClip[i];
            _initialPlayerStatusData.sheets[0].list[i]._damageAudioClip = _damageAudioClip[i];
        }


        return _initialPlayerStatusData;
    }
    public static EnemyStatusData SetAssetValue4(EnemyStatusData _enemyStatusData)
    {
        for (int i = 0; i < _enemyStatusData.sheets[0].list.Count; i++)
        {
            _enemyStatusData.sheets[0].list[i]._enemyAttackAudio = _enemyAttackAudio[i];
            _enemyStatusData.sheets[0].list[i]._enemyDamageAudio = _enemyDamageAudio[i];
        }
        return _enemyStatusData;
    }
}
