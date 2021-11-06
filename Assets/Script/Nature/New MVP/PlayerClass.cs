using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

//プレイヤーステータスクラス
public class PlayerStatus
{
    private int _attackStartTime;
    public int AttackStartTime
    {
        get { return _attackEndTime; }
        set { _attackEndTime = value; }
    }
    private int _attackEndTime;
    public int AttackEndTime
    {
        get { return _attackEndTime; }
        set { _attackEndTime = value; }
    }
    private int _attackDurationTime;
    public int AttackDurationTime
    {
        get { return _attackDurationTime; }
        set { _attackDurationTime = value; }
    }

    private float _attackSpeed;
    public float AttackSpeed => _attackSpeed;

    private int _maxHealth;
    public int MaxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth += value; }
    }

    private int _health;
    public int Health
    {
        get { return _health; }
        set
        {
            if (value > 0) _health = value;
            else _health = 0;
        }
    }

    private int _maxHp;
    public int MaxHp
    {
        get { return _maxHp; }
        set 
        { 
            if(value > 0) _maxHp = value;
            else _maxHp = 1;
        }
    }
    private ReactiveProperty<int> _hp;
    public ReactiveProperty<int> ReactiveHp { 
        get { return _hp; } 
        set { _hp = value; } 
    }
    public int Hp
    {
        get { return _hp.Value; }
        set
        {
            if (value > _maxHp) _hp.Value = _maxHp;
            else if (value > 0) _hp.Value = value;
            else _hp.Value = 0;
        }
    }
    private int _maxMp;
    public int MaxMp
    {
        get { return _maxMp; }
        set
        {
            if (value > 0) _maxMp = value;
            else _maxMp = 1;
        }
    }
    private ReactiveProperty<int> _mp;
    public ReactiveProperty<int> ReactiveMp { 
        get { return _mp; } 
        set { _mp = value; } 
    }
    public int Mp
    {
        get { return _mp.Value; }
        set
        {
            if (value > _maxMp) _mp.Value = _maxMp;
            else if (value > 0) _mp.Value = value;
            else _mp.Value = 0;
        }
    }
    private int _attack;
    public int Attack
    {
        get { return _attack; }
        set
        {
            if (value > 0) _attack = value;
            else _attack = 1;
        }
    }
    private int _defense;
    public int Defense
    {
        get { return _defense; }
        set
        {
            if (value > 0) _defense = value;
            else _defense = 1;
        }
    }
    private int _speed;
    public int Speed
    {
        get { return _speed; }
        set
        {
            if (value > 0) _speed = value;
            else _speed = 1;
        }
    }
    private int _playerUseSkillNo;
    public int PlayerUseSkillNo
    {
        get { return _playerUseSkillNo; }
        set
        {
            if (value <= 2) _playerUseSkillNo = value;
            else { _playerUseSkillNo = 0; }
        }
    }

    //スキル習得か未収得かを判別
    public bool[] _playerSkillList;
    //装備しているスキル
    public Skill[] _playerSkill = new Skill[ConstValue._skillSetMax];
    public GameObject _attackColliderObj;
    public AudioClip _attackAudio;
    public AudioClip _damageAudio;

    //コンストラクタ、初期ステータス
    public PlayerStatus(int _initialHealth
                      , int _initialHp
                      , int _initialMp
                      , int _initialAttack
                      , int _initialDefense
                      , int _initialSpeed
                      , int _initialAttackStartTime
                      , int _initialAttackEndTime
                      , int _initialAttackDurationTime
                      , float _attackSpeed
                      , GameObject _attackColliderObj
                      , AudioClip _attackAudio
                      , AudioClip _damageAudio)
    {
        SkillData _skillData = Resources.Load("SkillData") as SkillData;

        _maxHealth = _initialHealth;
        _health = _maxHealth;
        _maxHp = _initialHp;
        _hp = new ReactiveProperty<int>(_maxHp);
        _maxMp = _initialMp;
        _mp = new ReactiveProperty<int>(_maxMp);
        _attack = _initialAttack;
        _defense = _initialDefense;
        _speed = _initialSpeed;
        _attackStartTime = _initialAttackStartTime;
        _attackEndTime = _initialAttackEndTime;
        _attackDurationTime = _initialAttackDurationTime;
        _playerUseSkillNo = 0;
        this._attackSpeed = _attackSpeed;

        _playerSkillList = new bool[_skillData.sheets[0].list.Count];
        for(int i = 0; i < _playerSkill.Length; i++)
        {
            _playerSkillList[i] = false;
        }

        this._attackColliderObj = _attackColliderObj;
        this._attackAudio = _attackAudio;
        this._damageAudio = _damageAudio;
    }
}

//スキルクラス
public class Skill
{
    private string _skillName;
    public string SkillName => _skillName;
    //消費MP
    public int _skillMp;
    public int SkillMp => _skillMp;
    //攻撃倍率
    private float _skillAttackMagnification;
    public float SkillAttackMagnification => _skillAttackMagnification;
    //アニメーション長さをデフォルト値から短縮
    private float _skillAnimationLength;
    public float SkillAnimationLength => _skillAnimationLength;
    //スキル後隙
    private int _skillEndTime;
    public int SkillEndTime => _skillEndTime;
    //スキル判定持続時間
    private int _skillDurationTime;
    public int SkillDurationTime => _skillDurationTime;
    //スキルコライダー範囲
    private float _skillAttackRange;
    public float SkillAttackRange => _skillAttackRange;
    //スキル移動速度
    private float _skillSpeed;
    public float SkillSpeed => _skillSpeed;
    private string _skillTypeName;
    public string SkillTypeName => _skillTypeName;
    //スキル毎のアニメーション
    private AnimationClip _skillAnimation;
    public AnimationClip SkillAnimation => _skillAnimation;
    //スキルオブジェクト
    private GameObject _skillColliderObj;
    public GameObject SkillColliderObj => _skillColliderObj;
    //スキルアイコン
    private Sprite _skillImage;
    public Sprite SkillImage => _skillImage;

    private AudioClip _skillAudioClip;
    public AudioClip SkillAudioClip
    {
        get { return _skillAudioClip; }
        set { _skillAudioClip = value; }
    }

    public Skill(string _skillName,
                 int _skillMp,
                 float _skillAttackMagnification,
                 float _skillAnimationLength,
                 int _skillEndTime,
                 int _skillDurationTime,
                 float _skillAttackRange,
                 float _skillSpeed,
                 string _skillTypeName,
                 AnimationClip _skillAnimation,
                 GameObject _skillColliderObj,
                 Sprite _skillImage,
                 AudioClip _skillAudioClip)
    {

        this._skillName = _skillName;
        this._skillMp = _skillMp;
        this._skillAttackMagnification = _skillAttackMagnification;
        this._skillAnimationLength = _skillAnimationLength;
        this._skillEndTime = _skillEndTime;
        this._skillDurationTime = _skillDurationTime;
        this._skillAttackRange = _skillAttackRange;
        this._skillSpeed = _skillSpeed;
        this._skillTypeName = _skillTypeName;
        this._skillAnimation = _skillAnimation;
        this._skillColliderObj = _skillColliderObj;
        this._skillColliderObj = _skillColliderObj;
        this._skillImage = _skillImage;
        this._skillAudioClip = _skillAudioClip;
    }
}

//アイテムクラス
public class Item
{
    public string _name;
}

//装備クラス
public class Equipment
{
    public string _name;
    public int _attack;
}


