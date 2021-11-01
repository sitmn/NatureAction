using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*プレイヤー1,2のデータ（ステータス、所持アイテム、習得技、装備）*/
/*アイテムボックス(2キャラ共有アイテム)　＋　魔石）*/
/*システム（日付、時間、イベントフラグ）　　　を保持するクラス　シングルトン、　　セーブ時、このクラスのみ保存*/
public class GameManager : MonoBehaviour
{
    //シングルトン
    public static GameManager Instance = null;

    /*------------------------------natureScene----------------------------*/
    //プレイヤーの情報を保持
    public PlayerStatus[] _playerStatus;
    //操作中のキャラ
    /*0:キャラ1
      1:キャラ2*/
    private int _playerOperate;
    public int PlayerOperate { 
        get { return _playerOperate; }
        set{
                if (value < ConstValue._playerAmount) _playerOperate = value;
                else { _playerOperate = 0; }
           }
    }

    
    //ゴールドや魔石等の共有アイテムを保持
    public ShareItem _shareItem;

    //日付情報を保持
    public DayManage _dayManage;
    
    /*************************************************************************/


    /*******************************DungeonSenece*****************************/


    /*************************************************************************/

    void Awake()
    {
        Singleton();

        InitializePlayerStatus();

        //初期スキルセット
        InstantiateSkill();
        _playerOperate = 0;


    }

    void Start()
    {

    }

    private void Singleton()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    private void InitializePlayerStatus()
    {
        _playerStatus = new PlayerStatus[ConstValue._playerAmount];
        InitialPlayerStatusData _initialPlayerStatusData;
        _initialPlayerStatusData = Resources.Load("InitialPlayerStatusData") as InitialPlayerStatusData;

        for (int i = 0; i < _playerStatus.Length; i++)
        {
            _playerStatus[i] = new PlayerStatus(_initialPlayerStatusData.sheets[0].list[i]._initialHealth
                                              , _initialPlayerStatusData.sheets[0].list[i]._initialHp
                                              , _initialPlayerStatusData.sheets[0].list[i]._initialMp
                                              , _initialPlayerStatusData.sheets[0].list[i]._initialAttack
                                              , _initialPlayerStatusData.sheets[0].list[i]._initialDefense
                                              , _initialPlayerStatusData.sheets[0].list[i]._initialSpeed
                                              , _initialPlayerStatusData.sheets[0].list[i]._initialAttackStartTime
                                              , _initialPlayerStatusData.sheets[0].list[i]._initialAttackEndTime
                                              , _initialPlayerStatusData.sheets[0].list[i]._initialAttackDurationTime
                                              , _initialPlayerStatusData.sheets[0].list[i]._initialAttackSpeed
                                              , _initialPlayerStatusData.sheets[0].list[i]._initialAttackColliderObj
                                              , _initialPlayerStatusData.sheets[0].list[i]._attackAudioClip
                                              , _initialPlayerStatusData.sheets[0].list[i]._damageAudioClip);
        }


        _shareItem = new ShareItem();

        _dayManage = new DayManage();
    }

    private void InstantiateSkill()
    {
        SkillData _skillData;
        _skillData = Resources.Load("SkillData") as SkillData;
        SkillScript _skillScr = new SkillScript();

        //初期スキルを習得してセット
        for (int i = 0; i < ConstValue._skillSetMax; i++)
        {
            for (int j = 0; j < ConstValue._playerAmount; j++)
            {
                _skillScr.SkillObtain(_skillData, j, i);
                _skillScr.SkillSet(j, i, i);
            }
        }
    }
}

/***************プレイヤー情報********************/
//ゴールド、魔石等の共有アイテム
public class ShareItem
{
    public MagicStone[] _magicStone;

    public ShareItem(){
        _magicStone = new MagicStone[5];

        for(int i = 0;i <  _magicStone.Length; i++)
        {
            _magicStone[i] = new MagicStone();
        }

        _magicStone[0].Name = "パープルストーン";
        _magicStone[1].Name = "レッドストーン";
        _magicStone[2].Name = "ブルーストーン";
        _magicStone[3].Name = "グリーンストーン";
        _magicStone[4].Name = "イエローストーン";

        foreach(MagicStone magicStone in _magicStone)
        {
            magicStone.Amount = 1000;
        }
    }

    public int SumMagicStone()
    {
        int _sumMagicStone = 0;
        for(int i= 0; i < _magicStone.Length; i++) {
            _sumMagicStone += _magicStone[i].Amount;
        }

        return _sumMagicStone;
    }
}


public class MagicStone
{
    private string _name;
    public string Name{ set { _name = value; } }
    private int _amount;
    public int Amount
    {
        get { return _amount; }
        set { _amount = value; }
    }
}

/********************************************/


/**************日付管理****************/
public class DayManage {

    private int _day;
    public int Day {
        get { return _day; }
        set { _day += value; }
    }

    private int _dayFlag;

    public DayManage(){
        _day = 1;
    }
}