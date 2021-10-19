using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

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
    private int _hp;
    public int Hp
    {
        get { return _hp; }
        set { _hp = value; }
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
    private int _mp;
    public int MP
    {
        get { return _mp; }
        set { _mp = value; }
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
        _hp = _maxHp;
        _maxMp = _initialMp;
        _mp = _maxMp;
        _attack = _initialAttack;
        _defense = _initialDefense;
        _speed = _initialSpeed;
        _attackStartTime = _initialAttackStartTime;
        _attackEndTime = _initialAttackEndTime;
        _attackDurationTime = _initialAttackDurationTime;

        _playerSkillList = new List<Skill>();
        _playerItemList = new List<Item>();
        _playerEquipment = new Equipment();
    }
}

//スキルクラス
public class Skill
{
    private string _skillName;
    public string SkillName
    {
        get { return _skillName; }
        set { _skillName = value; }
    }
    private float _skillAttackMagnification;
    public float SkillAttackMagnification
    {
        get { return _skillAttackMagnification; }
        set { _skillAttackMagnification = value; }
    }
    private int _skillStartTime;
    public int SkillStartTime
    {
        get { return _skillStartTime; }
        set { _skillStartTime = value; }
    }
    private int _skillEndTime;
    public int SkillEndTime
    {
        get { return _skillEndTime; }
        set { _skillEndTime = value; }
    }
    private int _skillDurationTime;
    public int SkillDurationTime
    {
        get { return _skillDurationTime; }
        set { _skillDurationTime = value; }
    }
    private float _skillAttackRange;
    public float SkillAttackRange
    {
        get { return _skillAttackRange; }
        set { _skillAttackRange = value; }
    }

    public Skill(string _skillName,
                 float _skillAttackMagnification,
                 int _skillStartTime,
                 int _skillEndTime,
                 int _skillDurationTime,
                 float _skillAttackRange){

        this._skillName = _skillName;
        this._skillAttackMagnification = _skillAttackMagnification;
        this._skillStartTime = _skillStartTime;
        this._skillEndTime = _skillEndTime;
        this._skillDurationTime = _skillDurationTime;
        this._skillAttackRange = _skillAttackRange;
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


