using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveScript : MonoBehaviour
{
    //プレイヤーの位置情報
    [SerializeField]
    private Transform[] _playerTr;
    //プレイヤーのキャラクターコントローラー
    [SerializeField]
    public CharacterController[] _playerCharController;

    //プレイヤーの移動
    public void PlayerMove(Vector3 _moveVector3)
    {
        _moveVector3 = Time.deltaTime * _moveVector3 * ConstValue._playerSpeed;

        _playerCharController[GameManager.Instance.PlayerOperate].Move(_moveVector3);
    }
    //プレイヤーの回転
    public void PlayerRotate(Vector3 _rotateVector3)
    {
        //最終的な回転姿勢
        Quaternion _playerRotate = Quaternion.LookRotation(_rotateVector3);


        //最終回転姿勢への回転割合
        float _rotateSpeed = Time.deltaTime * ConstValue._rotateSpeed;
        //割合が上限を超えていれば最大値に
        if (_rotateSpeed > 1)
        {
            _rotateSpeed = 1;
        }

        //最終回転姿勢の途中の姿勢
        _playerRotate = Quaternion.Slerp(_playerTr[GameManager.Instance.PlayerOperate].rotation, _playerRotate, _rotateSpeed);
        //playerへ反映
        _playerTr[GameManager.Instance.PlayerOperate].rotation = _playerRotate;
    }
}
