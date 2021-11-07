using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ConstValue
{
    //魔石数
    public const int _stoneKind = 5;

    //プレイヤー数
    public const int _playerAmount = 2;

    //ステータスの種類
    public const int _playerStatuKind = 5;

    //修行種類
    public const int _trainingKind = 6;

    //体力ゲージのWidth
    public const float _hpBarWidth = 600f;

    //減少時のテキストの色
    public const float _decreaseColorR = 1f;
    public const float _decreaseColorG = 0;
    public const float _decreaseColorB = 0;
    public const float _decreaseColorA = 1f;

    //通常時のテキストの色
    public const float _normalColorR = 0;
    public const float _normalColorG = 0;
    public const float _normalColorB = 0;
    public const float _normalColorA = 1f;

    //点滅体力ゲージ点滅間隔
    public const float _hPBarBlinkTime = 0.6f;

    //ボタン点滅間隔
    public const float _buttonBlinkTime = 5f;

    //テキスト増減にかかる時間
    public const float _textChangeTime = 0.5f;

    //画面フェードアウトの時間
    public const float _fadeOutTime = 0.5f;

    //スキルセット最大数
    public const int _skillSetMax = 3;


    /*****************DungeonScene************************/
    //キャラクターの移動と回転速度
    public const float _rotateSpeed = 15f;
    public const float _playerSpeed = 8f;

    //カメラの回転速度
    public const float _cameraRotateSpeed = 1f;
    //カメラの停止中比重
    public const int _idleCameraWeight = 30;
    //カメラの移動中比重最大値
    public const int _moveCameraWeightMax = 1000;

    //カメラの移動比重の上昇量
    public const int _moveCameraWeightUp = 2;
    //カメラの移動比重の減少量
    public const int _moveCameraWightDown = 3;

    //スキル変更クールタイム
    public const int _coolTimeSkillChange = 50;
    //キャラ変更クールタイム
    public const int _coolTimeCharChange = 500;

    //キャラのスキルアイコン間距離
    public const float _skillIconDistance = 80f;

    //敵の停止中回転速度
    public const float _enemyrotateSpeed = 3f;


    //クリア時の日数経過
    public const int _clearProgressDay = 3;
    //ゲームオーバー時の日数経過
    public const int _gameoverProgressDay = 7;
}
