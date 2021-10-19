using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController2 : MonoBehaviour
{
    [SerializeField]
    private Transform[] PlayerTr;

    [SerializeField]
    private CharacterController[] PlayerCharController;

    [SerializeField]
    private CinemachineMixingCamera _mixingCamera;

    //キャラ移動用のアクション
    private InputAction _inputMoveVector;
    private InputAction _cameraRotateVector;

    //プレイヤーアニメーション
    [SerializeField]
    private PlayerAnimation _playerAnimation;

    //操作キャラフラグ 0:キャラ1    1:キャラ2
    private int _charOperate = 0;


    //カメラの回転
    [SerializeField]
    private Transform _mainCameraTr;

    //カメラの比重（移動時間に比例）
    private int _moveCameraWeight = 0;
    

    /*//2キャラ移動用の入力値
    private Vector3[] _inputVector3 = new Vector3[ConstValue._playerAmount];*/


    // Start is called before the first frame update
    void Start()
    {
        PlayerInput _playerInput = GetComponent<PlayerInput>();

        _inputMoveVector = _playerInput.currentActionMap["Move"];
        _cameraRotateVector = _playerInput.currentActionMap["Look"];

        _mixingCamera.m_Weight0 = ConstValue._idleCameraWeight;
    }

    void Update()
    {
        if (_cameraRotateVector.ReadValue<float>() != 0)
        {
            CameraRotate(_cameraRotateVector.ReadValue<float>());
        }
        

        if (_inputMoveVector.ReadValue<Vector2>() != Vector2.zero)
        {
            Vector3 _actionVector3 = ToVector3_XZ(_inputMoveVector.ReadValue<Vector2>());
            _actionVector3 = AngleDecide(_actionVector3);

            Move(_actionVector3);
            Rotate(_actionVector3);

            MixingMoveCameraUp();
        }
        else
        {
            
            MixingMoveCameraDonw();
        }
    }


    //Vector2をx,z方向のVector3にキャスト
    private Vector3 ToVector3_XZ(Vector2 _vector2)
    {
        Vector3 _vector3 = new Vector3(_vector2.x, 0, _vector2.y);

        return _vector3;
    }


    //現在の角度に対応した角度へ変換
    private Vector3 AngleDecide(Vector3 _actionVector3)
    {
        //カメラの角度に応じた移動の導出
        _actionVector3 = Quaternion.Euler(0,_mainCameraTr.eulerAngles.y, 0) * _actionVector3;
        
        return _actionVector3;
    }


    //キャラ移動
    private void Move(Vector3 _moveVector3)
    {
        _moveVector3 = Time.deltaTime * _moveVector3 * ConstValue._playerSpeed;

        PlayerCharController[_charOperate].Move(_moveVector3);

        _playerAnimation.MoveAnimation(_charOperate, true);
    }

    //プレイヤーの回転
    private void Rotate(Vector3 _rotateVector3)
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
        _playerRotate = Quaternion.Slerp(PlayerTr[_charOperate].rotation, _playerRotate, _rotateSpeed);
        //playerへ反映
        PlayerTr[_charOperate].rotation = _playerRotate;
    }


    private void CameraRotate(float _angle)
    {
        _angle = _angle * ConstValue._cameraRotateSpeed;
        Quaternion _rotateQuaternion = Quaternion.AngleAxis(_angle, Vector3.up);

        _mainCameraTr.rotation = _rotateQuaternion * _mainCameraTr.rotation;
    }


    //カメラ比重を変更（移動用カメラのみ比重を増やす）
    private void MixingMoveCameraUp()
    {
        if (_moveCameraWeight < ConstValue._moveCameraWeightMax) _moveCameraWeight = _moveCameraWeight + ConstValue._moveCameraWeightUp;

        _mixingCamera.m_Weight1 = _moveCameraWeight / 10f;
    }

    //カメラ比重を変更（移動用カメラのみ比重を減らす）
    private void MixingMoveCameraDonw()
    {
        if (_moveCameraWeight > 0)
        {
            _moveCameraWeight = _moveCameraWeight - ConstValue._moveCameraWightDown;
        }
        else
        {
            _moveCameraWeight = 0;
        }
        _mixingCamera.m_Weight1 = _moveCameraWeight / 10f;
    }



    /*******2キャラ同時操作(中止)*************
    // Update is called once per frame
    void Update()
    {
        for(int i = 0;i < ConstValue._playerAmount; i++)
        {
            _inputVector3[i] = InputVector3(i);
            
            //入力があるとき、プレイヤーを動かす
            if (_inputVector3[i] != Vector3.zero)
            {
                Debug.Log("A");
                //プレイヤーの回転
                Rotate(PlayerTr[i], _inputVector3[i]);
                //プレイヤーの移動
                Move(PlayerCharController[i], _inputVector3[i]);
            }
        }
    }


    private Vector3 InputVector3(int _playerNo){
        Vector3 _actionVector3 = Vector3.zero;

        if (_playerNo == 0)
        {
            _actionVector3.x = Input.GetAxis("KeyboardHorizontal1");
            _actionVector3.z = Input.GetAxis("KeyboardVertical1");
            if(_actionVector3 == Vector3.zero)
            {
                _actionVector3.x = Input.GetAxis("ControllerHorizontal1");
                _actionVector3.z = Input.GetAxis("ControllerVertical1");
            }        
        }
        else
        {
            _actionVector3.x = Input.GetAxis("KeyboardHorizontal2");
            _actionVector3.z = Input.GetAxis("KeyboardVertical2");
            if (_actionVector3 == Vector3.zero)
            {
                _actionVector3.x = Input.GetAxis("ControllerHorizontal2");
                _actionVector3.z = Input.GetAxis("ControllerVertical2");
            }
        }

        return _actionVector3;
    }


    


    //プレイヤーの移動
    private void Move(CharacterController _characterController, Vector3 _moveVector3)
    {
        //プレイヤーの移動
        _characterController.Move(_moveVector3 * ConstValue._playerSpeed);
    }

    //プレイヤーの回転
    private void Rotate(Transform _playerTr, Vector3 _angleVector3)
    {
        //最終的な回転姿勢
        Quaternion _playerRotate = Quaternion.LookRotation(_angleVector3);


        //最終回転姿勢への回転割合
        float _rotateSpeed = Time.deltaTime * ConstValue._rotateSpeed;
        //割合が上限を超えていれば最大値に
        if (_rotateSpeed > 1)
        {
            _rotateSpeed = 1;
        }

        //最終回転姿勢の途中の姿勢
        _playerRotate = Quaternion.Slerp(_playerTr.rotation, _playerRotate, _rotateSpeed);
        //playerへ反映
        _playerTr.rotation = _playerRotate;
    }
    */
}



    