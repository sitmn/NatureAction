using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*プレイヤー1と2のTrainingNoから、体力変動・上昇ステータス・各消費魔石を導出*/
//入力：TrainingNoの情報
//出力：プレイヤー
public class TrainingCalculateScript
{
    private TrainingPointData _trainingPointData;


    //計算メソッド
    public TrainingStruct TrainingCalculate(ButtonViewTraining[] _buttonViewTraining)
    {
        _trainingPointData = Resources.Load("TrainingPointData") as TrainingPointData;

        int?[] _trainingNoArray = InitializeTrainingNo(_buttonViewTraining);
        //2プレイヤー分の修行結果情報
        TrainingStruct _trainingStruct;
        //トレーニング毎の魔石消費レート基礎値
        /*魔石消費量割合配列 0:紫石 0:プレイヤー1
                             1:赤石 1:プレイヤー2
                             2:青石
                             3:緑石
                             4:黄石
        */
        float[,] _magicStoneBaseRaitoArray;

        //修行基礎値の取得
        TrainingStatusStruct _trainingStatusBaseStruct = InitializeTrainingBasePoint(_trainingNoArray);

        //魔石消費基礎値割合の取得
        _magicStoneBaseRaitoArray = InitializeMagicStoneRaitoBase(_trainingNoArray);

        //各魔石消費量を導出
        UseMagicStoneStruct _useMagicStoneBaseStruct = MagicStoneValueCalculate(_magicStoneBaseRaitoArray);
        
        //体力消費量を導出
        ChangeHealth _changeHealthBase = InitializeChangeHealthBase(_trainingNoArray);

        _trainingStruct._trainingStatusStruct = _trainingStatusBaseStruct;
        _trainingStruct._useMagicStoneStruct = _useMagicStoneBaseStruct;
        _trainingStruct._changeHealth = _changeHealthBase;

        //上昇パラメータ値導出
        _trainingStruct = StateValueCalculate(_trainingStruct);

        return _trainingStruct;
    }

    //選択しているボタンの修行Noを取得、Nullの場合、修行NoはNullとする
    private int?[] InitializeTrainingNo(ButtonViewTraining[] _buttonViewTraining)
    {
        int?[] _trainingNo = new int?[ConstValue._playerAmount];

        for(int i = 0; i < _trainingNo.Length; i++)
        {
            if(_buttonViewTraining[i] != null)
            {
                _trainingNo[i] = _buttonViewTraining[i].TrainingNo;
            }
            else
            {
                _trainingNo[i] = null;
            }
        }

        return _trainingNo;
    }

    //修行計算に使用する修行基礎値
    private TrainingStatusStruct InitializeTrainingBasePoint(int?[] _trainingNoArray)
    {
        int[,] _trainingBasePointArray = new int[ConstValue._playerStatuKind, ConstValue._playerAmount];

        //修行ボタンを選択していない場合、0を格納
        for (int i = 0; i < _trainingBasePointArray.GetLength(1); i++) {
            if (_trainingNoArray[i] != null)
            {
                _trainingBasePointArray[0, i] = _trainingPointData.sheets[0].list[(int)_trainingNoArray[i]]._hpBase;
                _trainingBasePointArray[1, i] = _trainingPointData.sheets[0].list[(int)_trainingNoArray[i]]._mpBase;
                _trainingBasePointArray[2, i] = _trainingPointData.sheets[0].list[(int)_trainingNoArray[i]]._attackBase;
                _trainingBasePointArray[3, i] = _trainingPointData.sheets[0].list[(int)_trainingNoArray[i]]._defenseBase;
                _trainingBasePointArray[4, i] = _trainingPointData.sheets[0].list[(int)_trainingNoArray[i]]._speedBase;
            }
            else
            {
                for (int j = 0; j < _trainingBasePointArray.GetLength(0); j++)
                {
                    _trainingBasePointArray[j, i] = 0;
                }
            }
        } 
        //基礎値を初期値に設定
        TrainingStatusStruct _baseStruct = new TrainingStatusStruct(_trainingBasePointArray[0,0],
                                                                    _trainingBasePointArray[1,0],
                                                                    _trainingBasePointArray[2,0],
                                                                    _trainingBasePointArray[3,0],
                                                                    _trainingBasePointArray[4,0],
                                                                    _trainingBasePointArray[0,1],
                                                                    _trainingBasePointArray[1,1],
                                                                    _trainingBasePointArray[2,1],
                                                                    _trainingBasePointArray[3,1],
                                                                    _trainingBasePointArray[4,1]);
        
        return _baseStruct;
    }


    //修行計算に使用する魔石消費割合基礎値の取得
    private float[,] InitializeMagicStoneRaitoBase(int?[] _trainingNoArray)
    {
        float[,] _magicStoneBaseRaitoArray = new float[ConstValue._stoneKind, ConstValue._playerAmount];

        //修行ボタンを選択していない場合、0を格納
        for (int i = 0; i < _magicStoneBaseRaitoArray.GetLength(1); i++)
        {
            if (_trainingNoArray[i] != null)
            {
                _magicStoneBaseRaitoArray[0, i] = _trainingPointData.sheets[0].list[(int)_trainingNoArray[i]]._purpleStoneConsumeRaito;
                _magicStoneBaseRaitoArray[1, i] = _trainingPointData.sheets[0].list[(int)_trainingNoArray[i]]._redStoneConsumeRaito;
                _magicStoneBaseRaitoArray[2, i] = _trainingPointData.sheets[0].list[(int)_trainingNoArray[i]]._blueStoneConsumeRaito;
                _magicStoneBaseRaitoArray[3, i] = _trainingPointData.sheets[0].list[(int)_trainingNoArray[i]]._greenStoneConsumeRaito;
                _magicStoneBaseRaitoArray[4, i] = _trainingPointData.sheets[0].list[(int)_trainingNoArray[i]]._yellowStoneConsumeRaito;
            }
            else
            {
                for (int j = 0; j < _magicStoneBaseRaitoArray.GetLength(0); j++)
                {
                    _magicStoneBaseRaitoArray[j, i] = 0;
                }
            }
        }
        return _magicStoneBaseRaitoArray;
    }

    //修行で消費する体力値の取得
    private ChangeHealth InitializeChangeHealthBase(int?[] _trainingNoArray)
    {
        int[] _trainingChangeHealthPointArray = new int[ConstValue._playerAmount];

        //修行ボタンを選択していない場合、0を格納
        for (int i = 0; i < _trainingChangeHealthPointArray.Length; i++)
        {
            if (_trainingNoArray[i] != null || _trainingNoArray.Equals(null))
            {
                _trainingChangeHealthPointArray[i] = _trainingPointData.sheets[0].list[(int)_trainingNoArray[i]]._tired;

                if (GameManager.Instance._playerStatus[i].Health - _trainingChangeHealthPointArray[i] <= 0)
                {
                    _trainingChangeHealthPointArray[i] = GameManager.Instance._playerStatus[i].Health;
                }else if (GameManager.Instance._playerStatus[i].Health - _trainingChangeHealthPointArray[i] >= GameManager.Instance._playerStatus[i].MaxHealth)
                {
                    _trainingChangeHealthPointArray[i] = GameManager.Instance._playerStatus[i].Health - GameManager.Instance._playerStatus[i].MaxHealth;
                }
            }
            else
            {
                _trainingChangeHealthPointArray[i] = 0;
            }
        }
        //基礎値を初期値に設定
        ChangeHealth _changeHealthBase = new ChangeHealth(_trainingChangeHealthPointArray[0],
                                                          _trainingChangeHealthPointArray[1]);

        return _changeHealthBase;
    }

    //各魔石消費量導出用の計算
    private UseMagicStoneStruct MagicStoneValueCalculate(float[,] _magicStoneBaseRaitoArray)
    {
        UseMagicStoneStruct _useMagicStoneStruct;

        //同魔石消費時、割合アップ
        float[] _sameStoneCoefficient = new float[ConstValue._stoneKind];
        for (int i = 0; i < _magicStoneBaseRaitoArray.GetLength(0); i++)
        {
            if (_magicStoneBaseRaitoArray[i,0] != 0 && _magicStoneBaseRaitoArray[i, 1] != 0)
            {
                _sameStoneCoefficient[i] = 1f + _trainingPointData.sheets[0].list[0]._sameConsumeStoneCoefficient;
            }
            else
            {
                _sameStoneCoefficient[i] = 1f;
            }
        }

        //各魔石の最終消費割合
        float[] _eachMagicStoneRaito = new float[ConstValue._stoneKind];
        for(int i = 0; i < _magicStoneBaseRaitoArray.GetLength(0); i++)
        {
            _eachMagicStoneRaito[i] = (_magicStoneBaseRaitoArray[i, 0] + _magicStoneBaseRaitoArray[i, 1]) * _sameStoneCoefficient[i];
        }

        //各魔石消費量をTrainingStructに格納
        _useMagicStoneStruct = new UseMagicStoneStruct((int)(GameManager.Instance._shareItem._magicStone[0].Amount * _eachMagicStoneRaito[0]),
                                                       (int)(GameManager.Instance._shareItem._magicStone[1].Amount * _eachMagicStoneRaito[1]),
                                                       (int)(GameManager.Instance._shareItem._magicStone[2].Amount * _eachMagicStoneRaito[2]),
                                                       (int)(GameManager.Instance._shareItem._magicStone[3].Amount * _eachMagicStoneRaito[3]),
                                                       (int)(GameManager.Instance._shareItem._magicStone[4].Amount * _eachMagicStoneRaito[4]));

        return _useMagicStoneStruct;
    }



    //ステータス増減値導出用の計算メソッド
    /*_magicStoneTotalAmount:魔石総消費量*/
    private TrainingStruct StateValueCalculate(TrainingStruct _trainingStruct)
    {
        //修行ポイント補正値導出　魔石総消費量
        for(int i= 0; i < _trainingStruct._useMagicStoneStruct._useMagicStone.Length; i++)
        {
            _trainingStruct._useMagicStoneStruct._useSumMagicStone += _trainingStruct._useMagicStoneStruct._useMagicStone[i];
        }


        //修行ポイント計算　（基礎値×魔石総消費量×係数×現在体力の割合）
        for(int i = 0; i < ConstValue._stoneKind; i++)
        {
            for (int j = 0; j < ConstValue._playerAmount; j++)
            {
                _trainingStruct._trainingStatusStruct._trainingPoint[i, j] 
                    = (int)(_trainingStruct._trainingStatusStruct._trainingPoint[i, j] 
                    * (1f + (float)_trainingStruct._useMagicStoneStruct._useSumMagicStone / _trainingPointData.sheets[0].list[0]._magicStoneRaito) 
                    * ((float)GameManager.Instance._playerStatus[j].Health / (float)GameManager.Instance._playerStatus[j].MaxHealth));
            }
        }
        
        return _trainingStruct;
    }
    /************************************************/
}


//修行によるパラメータ用構造体
public struct TrainingStruct
{
    public TrainingStatusStruct _trainingStatusStruct;
    public UseMagicStoneStruct _useMagicStoneStruct;
    public ChangeHealth _changeHealth;
}

public struct ChangeHealth{
    //体力消費or回復
    public int[] _tiredPoint;
    public ChangeHealth(int _tiredPointPlayer1Base,
                        int _tiredPointPlayer2Base)
    {
        _tiredPoint = new int[ConstValue._playerAmount];
        _tiredPoint[0] = _tiredPointPlayer1Base;
        _tiredPoint[1] = _tiredPointPlayer2Base;

    }

}

public struct TrainingStatusStruct
{
    //ステータス値の増減
    public int[,] _trainingPoint;

    public TrainingStatusStruct(int _hpBasePlayer1,
                                int _mpBasePlayer1,
                                int _attackBasePlayer1,
                                int _defenseBasePlayer1,
                                int _speedBasePlayer1,
                                int _hpBasePlayer2,
                                int _mpBasePlayer2,
                                int _attackBasePlayer2,
                                int _defenseBasePlayer2,
                                int _speedBasePlayer2)
    {
        _trainingPoint = new int[ConstValue._playerStatuKind, ConstValue._playerAmount];

        _trainingPoint[0, 0] = _hpBasePlayer1;
        _trainingPoint[1, 0] = _mpBasePlayer1;
        _trainingPoint[2, 0] = _attackBasePlayer1;
        _trainingPoint[3, 0] = _defenseBasePlayer1;
        _trainingPoint[4, 0] = _speedBasePlayer1;
        _trainingPoint[0, 1] = _hpBasePlayer2;
        _trainingPoint[1, 1] = _mpBasePlayer2;
        _trainingPoint[2, 1] = _attackBasePlayer2;
        _trainingPoint[3, 1] = _defenseBasePlayer2;
        _trainingPoint[4, 1] = _speedBasePlayer2;
    }
}

public struct UseMagicStoneStruct {

    //各魔石量
    public int[] _useMagicStone;
    //総魔石量
    public int _useSumMagicStone; 

    public UseMagicStoneStruct(int _purpleMagicStoneBase,
                               int _redMagicStoneBase,
                               int _blueMagicStoneBase,
                               int _greenMagicStoneBase,
                               int _yellowMagicStoneBase)
    {
        _useMagicStone = new int[ConstValue._stoneKind];

        _useMagicStone[0] = _purpleMagicStoneBase;
        _useMagicStone[1] = _redMagicStoneBase;
        _useMagicStone[2] = _blueMagicStoneBase;
        _useMagicStone[3] = _greenMagicStoneBase;
        _useMagicStone[4] = _yellowMagicStoneBase;

        _useSumMagicStone = 0;
    }
}
