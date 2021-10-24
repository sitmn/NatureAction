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
    public ReactiveProperty<int> Hp
    {
        get { return _hp; }
        set
        {
            if (value.Value > _maxHp) _hp.Value = _maxHp;
            else if (value.Value < 0) _hp.Value = value.Value;
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
    public ReactiveProperty<int> MP
    {
        get { return _mp; }
        set
        {
            if (value.Value > _maxMp) _mp.Value = _maxMp;
            else if (value.Value < 0) _mp.Value = value.Value;
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


    public List<Skill> _playerSkillList;
    public Skill[] _playerSkill = new Skill[ConstValue._skillSetMax];
    public List<Item> _playerItemList;
    public Equipment _playerEquipment;

    //コンストラクタ、初期ステータス
    public PlayerStatus(int _initialHealth
                      , int _initialHp
                      , int _initialMp
                      , int _initialAttack
                      , int _initialDefense
                      , int _initialSpeed
                      , int _initialAttackStartTime
                      , int _initialAttackEndTime
                      , int _initialAttackDurationTime)
    {
        

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

        _playerSkillList = new List<Skill>();
        _playerItemList = new List<Item>();
        _playerEquipment = new Equipment();
    }
}

//スキルクラス
public class Skill
{
    private string _skillName;
    public string SkillName => _skillName;
    private float _skillAttackMagnification;
    public float SkillAttackMagnification => _skillAttackMagnification;
    //private int _skillStartTime;
    //public int SkillStartTime => _skillStartTime;
    //アニメーション長さをデフォルト値から短縮
    private float _skillAnimationLength;
    public float SkillAnimationLength => _skillAnimationLength;
    private int _skillEndTime;
    public int SkillEndTime => _skillEndTime;
    private int _skillDurationTime;
    public int SkillDurationTime => _skillDurationTime;
    private float _skillAttackRange;
    public float SkillAttackRange => _skillAttackRange;
    private string _skillTypeName;
    public string SkillTypeName => _skillTypeName;
    private AnimationClip _skillAnimation;
    public AnimationClip SkillAnimation => _skillAnimation;
    private GameObject _skillColliderObj;
    public GameObject SkillColliderObj => _skillColliderObj;
    private Sprite _skillImage;
    public Sprite SkillImage => _skillImage;

    public Skill(string _skillName,
                 float _skillAttackMagnification,
                 float _skillAnimationLength,
                 int _skillEndTime,
                 int _skillDurationTime,
                 float _skillAttackRange,
                 string _skillTypeName,
                 AnimationClip _skillAnimation,
                 GameObject _skillColliderObj,
                 Sprite _skillImage)
    {

        this._skillName = _skillName;
        this._skillAttackMagnification = _skillAttackMagnification;
        //this._skillStartTime = _skillStartTime;
        this._skillAnimationLength = _skillAnimationLength;
        this._skillEndTime = _skillEndTime;
        this._skillDurationTime = _skillDurationTime;
        this._skillAttackRange = _skillAttackRange;
        this._skillTypeName = _skillTypeName;
        this._skillAnimation = _skillAnimation;
        this._skillColliderObj = _skillColliderObj;
        this._skillColliderObj = _skillColliderObj;
        this._skillImage = _skillImage;
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


