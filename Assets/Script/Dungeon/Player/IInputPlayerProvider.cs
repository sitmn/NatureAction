using System;
using UnityEngine;
using UniRx;

interface IInputPlayerProvider
{
    //プレイヤーの移動量
    IObservable<Vector3> MoveVector { get; }

    //プレイヤー攻撃の実行
    IObservable<Unit> Attack { get; }
    //プレイヤー技の実行
    IObservable<Unit> Skill { get; }

    //プレイヤー技の切り替え実行
    IObservable<Unit> SkillChange { get; }

    //プレイヤーキャラの切り替え実行
    //0: キャラ1
    //1: キャラ2
    IObservable<Unit> CharChange { get; }
}
