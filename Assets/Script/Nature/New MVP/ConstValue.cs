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

    //テキスト増減にかかる時間
    public const float _textChangeTime = 0.5f;

    //画面フェードアウトの時間
    public const float _fadeOutTime = 0.5f;
}
