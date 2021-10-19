using UnityEngine;
using UniRx;
using UniRx.Triggers;
using System;
using UnityEngine.InputSystem;
using Cysharp.Threading.Tasks;


public class InputPlayerProvider : MonoBehaviour, IInputPlayerProvider
{
    //プレイヤープロバイダーのインターフェースのゲッター
    public IObservable<Vector3> MoveVector => _move;
    public IObservable<Unit> Attack => _attack;
    public IObservable<Unit> Skill => _skill;
    public IObservable<Unit> SkillChange => _skillChange;
    public IObservable<Unit> CharChange => _charChange;


    //プレイヤー操作時のイベント
    private readonly Subject<Vector3> _move = new Subject<Vector3>();
    private readonly Subject<Unit> _attack = new Subject<Unit>();
    private readonly Subject<Unit> _skill = new Subject<Unit>();
    private readonly Subject<Unit> _skillChange = new Subject<Unit>();
    private readonly Subject<Unit> _charChange = new Subject<Unit>();


    //キャラ移動用のアクション
    private InputAction _inputMove, _inputAttack, _inputSkill, _inputSkillChange, _inputCharChange;
    
    //アクション不可フラグ
    public static bool _coolTimeFlag = true;


    void Start()
    {
        //Input用のアクションマップのセット
        SetInputAction();
        //プレイヤープロバイダーのDispose
        ProviderDispose();


        //攻撃用入力でのイベント発行
        this.UpdateAsObservable()
            .Select((_) => _inputAttack.triggered)
            .Skip(1)
            .Where(_ => _coolTimeFlag)
            .Subscribe((x) => {
                if (x)
                {
                    //他のイベント発行を無効にし、攻撃実行
                    UniTask _noWait = CoolTimeSet(GameManager._playerStatus[GameManager._playerOperate].AttackEndTime);
                    _attack.OnNext(Unit.Default);
                }
            }
        ).AddTo(this);

        //技用入力でのイベント発行
        this.UpdateAsObservable()
            .Select((_) => _inputSkill.triggered)
            .Skip(1)
            .Where(_ => _coolTimeFlag)
            .Subscribe((x) => {
                if (x)
                {
                    Debug.Log(GameManager._playerStatus[GameManager._playerOperate]._playerSkill[GameManager._playerUseSkillNo]);
                    //他のイベント発行を無効にし、技実行
                    UniTask _noWait = CoolTimeSet(GameManager._playerStatus[GameManager._playerOperate]._playerSkill[GameManager._playerUseSkillNo].SkillEndTime);
                    _skill.OnNext(Unit.Default);
                }
            }
        ).AddTo(this);

        //技の切り替え入力でのイベント発行
        this.UpdateAsObservable()
            .Select((_) => _inputSkillChange.triggered)
            .Skip(1)
            .Where(_ => _coolTimeFlag)
            .Subscribe((x) => {
                if (x)
                {
                    //他のイベント発行を無効にし、技切り替え実行
                    UniTask _noWait = CoolTimeSet(ConstValue._coolTimeSkillChange);
                    _skillChange.OnNext(Unit.Default);
                }
            }
        ).AddTo(this);

        //キャラの切り替え入力でのイベント発行
        this.UpdateAsObservable()
            .Select((_) => _inputCharChange.triggered)
            .Skip(1)
            .Where(_ => _coolTimeFlag)
            .Subscribe((x) => {
                if (x)
                {
                    //他のイベント発行を無効にし、キャラ切り替え実行
                    UniTask _noWait = CoolTimeSet(ConstValue._coolTimeCharChange);
                    _charChange.OnNext(Unit.Default);
                }
            }
        ).AddTo(this);

        //移動用入力でのイベント発行
        this.UpdateAsObservable()
            .Select((_) => ToVector3_XZ(_inputMove.ReadValue<Vector2>()))
            .Where(_ => _coolTimeFlag)
            .Subscribe((x) =>
            {
                //他のイベント発行を無効にし、攻撃実行
                UniTask _noWait = CoolTimeSet(0);
                _move.OnNext(x);
            }
        ).AddTo(this);
    }



    //アクションマップのセット
    private void SetInputAction()
    {
        PlayerInput _playerInput = GetComponent<PlayerInput>();

        _inputMove = _playerInput.currentActionMap["Move"];
        _inputAttack = _playerInput.currentActionMap["Attack"];
        _inputSkill = _playerInput.currentActionMap["Skill"];
        _inputSkillChange = _playerInput.currentActionMap["SkillChange"];
        _inputCharChange = _playerInput.currentActionMap["CharChange"];
    }


    //プレイヤープロバイダーのDispose
    private void ProviderDispose()
    {
        _move.AddTo(this);
        _attack.AddTo(this);
        _skill.AddTo(this);
        _skillChange.AddTo(this);
        _charChange.AddTo(this);
    }

    //イベントのクールタイム
    private async UniTask CoolTimeSet(int _waitFrame)
    {
        _coolTimeFlag = false;
        await UniTask.Delay(TimeSpan.FromMilliseconds(_waitFrame));
        _coolTimeFlag = true;
    }

    private Vector3 ToVector3_XZ(Vector2 _vector2)
    {
        Vector3 _vector3 = new Vector3(_vector2.x, 0, _vector2.y);

        return _vector3;
    }
}
