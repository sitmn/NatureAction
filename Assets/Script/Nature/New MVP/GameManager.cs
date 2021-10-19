using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*プレイヤー1,2のデータ（ステータス、所持アイテム、習得技、装備）*/
/*アイテムボックス(2キャラ共有アイテム)　＋　魔石）*/
/*システム（日付、時間、イベントフラグ）　　　を保持するクラス　シングルトン、　　セーブ時、このクラスのみ保存*/
public class GameManager : MonoBehaviour
{
    //プレイヤーの情報を保持
    public static PlayerStatus[] _playerStatus = new PlayerStatus[ConstValue._playerAmount];
    //ゴールドや魔石等の共有アイテムを保持
    public static ShareItem _shareItem;

    //日付情報を保持
    public static DayManage _dayManage;

    void Awake()
    {
        InitialPlayerStatusData _initialPlayerStatusData;
        _initialPlayerStatusData = Resources.Load("InitialPlayerStatusData") as InitialPlayerStatusData;

        for(int i = 0; i < _playerStatus.Length; i++)
        {
            _playerStatus[i] = new PlayerStatus(_initialPlayerStatusData.sheets[0].list[i]._initialHealth
                                              , _initialPlayerStatusData.sheets[0].list[i]._initialHp
                                              , _initialPlayerStatusData.sheets[0].list[i]._initialMp
                                              , _initialPlayerStatusData.sheets[0].list[i]._initialAttack
                                              , _initialPlayerStatusData.sheets[0].list[i]._initialDefense
                                              , _initialPlayerStatusData.sheets[0].list[i]._initialSpeed);
        }
        

        _shareItem = new ShareItem();

        _dayManage = new DayManage();
    }
}

/***************プレイヤー情報********************/
//ゴールド、魔石等の共有アイテム
public class ShareItem
{
    public Gold _gold;
    
    public MagicStone[] _magicStone;

    public ShareItem(){
        _gold = new Gold();
        _magicStone = new MagicStone[5];

        for(int i = 0;i <  _magicStone.Length; i++)
        {
            _magicStone[i] = new MagicStone();
        }

        _gold.Amount = 0;

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

public class Gold
{
    private int _amount;
    public int Amount
    {
        get { return _amount; }
        set { _amount = _amount + value; }
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