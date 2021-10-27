using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _playerObj;
    [SerializeField]
    private Transform[] _playerTr;

    private IInputPlayerProvider _inputPlayerProvider;
    private PlayerMoveScript _playerMoveScr;

    [SerializeField]
    private SkillIcon _playerIconScr;
    [SerializeField]
    private StatusBarModel _statusBarScr;

    [SerializeField]
    private CinemachineVirtualCamera _camera;
    
    //プレイヤーアニメーション
    [SerializeField]
    private PlayerAnimation[] _playerAnimation;

    // Start is called before the first frame update
    void Start()
    {
        _inputPlayerProvider = GetComponent<IInputPlayerProvider>();

        _playerMoveScr = GetComponent<PlayerMoveScript>();

        //プレイヤー関係のストリームを発行
        InputPlayerStream();

        //シーン開始時の初期値セット
        for (int i = 0; i < _playerObj.Length; i++)
        {
            //スキルをセット
            InstantiateSkillSet(i);

            //プレイヤー1以外を非アクティブに
            if(i != 0)
            {
                _playerObj[i].SetActive(false);
            }
        }
    }


    //プレイヤーストリームの発行
    private void InputPlayerStream()
    {
        //移動
        _inputPlayerProvider.
            MoveVector.
            Subscribe((x) =>
            {
                MovePlayer(x);
            }).AddTo(this);

        //攻撃発動
        _inputPlayerProvider
            .Attack
            .Subscribe((_) => 
            {
                _playerAnimation[GameManager.Instance.PlayerOperate].AttackAnimation(GameManager.Instance.PlayerOperate);
            }).AddTo(this);

        //スキル発動
        _inputPlayerProvider
            .Skill
            .Subscribe((_) =>
            {
                //MP消費
                GameManager.Instance._playerStatus[GameManager.Instance.PlayerOperate].Mp -= GameManager.Instance._playerStatus[GameManager.Instance.PlayerOperate]._playerSkill[GameManager.Instance._playerStatus[GameManager.Instance.PlayerOperate].PlayerUseSkillNo]._skillMp;
                _playerAnimation[GameManager.Instance.PlayerOperate].SkillAnimation(GameManager.Instance.PlayerOperate);
            }).AddTo(this);

        //スキル替え
        _inputPlayerProvider
            .SkillChange
            .Subscribe((_) =>
            {
                SkillChange();
            }).AddTo(this);

        //キャラ替え
        _inputPlayerProvider
            .CharChange
            .Subscribe((_) =>
            {
                PlayerCharChange();
            }).AddTo(this);


        //HPが0になったら、キャラを変える。２キャラとも0の場合ゲームオーバー
        for (int i = 0; i < ConstValue._playerAmount; i++)
        {
            GameManager.Instance._playerStatus[i].ReactiveHp
                .Where((x) => x <= 0)
                .Subscribe((_) => {
                    CharDeath();
            }).AddTo(this);
        }
    }

    //スキルアニメーションの初期値をセット
    private void InstantiateSkillSet(int _playerNo)
    {
        GameManager.Instance._playerStatus[_playerNo].PlayerUseSkillNo = 0;
        _playerAnimation[_playerNo].SkillAnimationChange(GameManager.Instance._playerStatus[_playerNo]);
    }

    //プレイヤーの移動
    private void MovePlayer(Vector3 _moveVector)
    {
        _playerMoveScr.PlayerMove(_moveVector);
        if (_moveVector != Vector3.zero)
        {
            _playerMoveScr.PlayerRotate(_moveVector);
        }

        if (_moveVector != Vector3.zero)
        {
            _playerAnimation[GameManager.Instance.PlayerOperate].MoveAnimation(GameManager.Instance.PlayerOperate, true);
        }
        else
        {
            _playerAnimation[GameManager.Instance.PlayerOperate].MoveAnimation(GameManager.Instance.PlayerOperate, false);
        }

        //親オブジェクト追従
        transform.position = _playerTr[GameManager.Instance.PlayerOperate].position;
    }

    //プレイヤーのスキル切り替え
    private void SkillChange()
    {
        GameManager.Instance._playerStatus[GameManager.Instance.PlayerOperate].PlayerUseSkillNo++;
        _playerAnimation[GameManager.Instance.PlayerOperate].SkillAnimationChange(GameManager.Instance._playerStatus[GameManager.Instance.PlayerOperate]);
        _playerIconScr.SelectIconMove();
    }

    //プレイヤーのキャラ切り替え
    private void PlayerCharChange()
    {
        //操作キャラ変更
        GameManager.Instance.PlayerOperate++;
        CharObjChange();

        _playerIconScr.SkillIconSet(GameManager.Instance.PlayerOperate);
        _playerIconScr.SelectIconMove();
        _statusBarScr.SetHpUI(GameManager.Instance._playerStatus[GameManager.Instance.PlayerOperate].Hp, GameManager.Instance.PlayerOperate);
        _statusBarScr.SetMpUI(GameManager.Instance._playerStatus[GameManager.Instance.PlayerOperate].Mp, GameManager.Instance.PlayerOperate);
    }

    //オブジェクトの有効無効を切り替え、ポジションを同じに
    private void CharObjChange()
    {
        for(int i = 0;i < ConstValue._playerAmount; i++)
        {
            if (GameManager.Instance.PlayerOperate == i)
            {
                _playerObj[i].SetActive(true);

                _playerMoveScr._playerCharController[i].enabled = false;

                if (GameManager.Instance.PlayerOperate != 0)
                {
                    _playerTr[i].position = _playerTr[i - 1].position;
                    _playerTr[i].rotation = _playerTr[i - 1].rotation;
                }
                else
                {
                    _playerTr[i].position = _playerTr[ConstValue._playerAmount - 1].position;
                    _playerTr[i].rotation = _playerTr[ConstValue._playerAmount - 1].rotation;
                }

                _playerMoveScr._playerCharController[i].enabled = true;

                //カメラの追従切り替え
                _camera.Follow = _playerTr[i];
            }
            else
            {
                _playerObj[i].SetActive(false);
            }
        }
    }


    //操作キャラの次のキャラNoを取得
    public static int GetNextCharNo()
    {
        int _nextCharNo;
        if (GameManager.Instance.PlayerOperate != ConstValue._playerAmount - 1) _nextCharNo = GameManager.Instance.PlayerOperate + 1;
        else _nextCharNo = 0;

        return _nextCharNo;
    }

    //倒されたとき
    private void CharDeath()
    {
        if (GameManager.Instance._playerStatus[GetNextCharNo()].Hp > 0) PlayerCharChange();
        else { Debug.Log("ゲームオーバー"); }//ボタンを押してタイトルに 
    }
}
