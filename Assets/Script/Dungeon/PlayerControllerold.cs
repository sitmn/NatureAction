using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerControllerold : MonoBehaviour
{
    //クラス
    //private CharacterController characterController;
    public GameObject player2;
    private Rigidbody player1Rb;
    private Rigidbody player2Rb;
    public Camera MainCamera;
    private MainCameraControllerold mainCameraController;
    private Animator player1Animation;
    private Animator player2Animation;
    private GameObject weakAttackObj;    //弱攻撃当たり判定
    private GameObject strongAttackObj;  //今日攻撃当たり判定

    //移動・回転用変数
    Vector3 playerMove;
    Vector3 moveDirection;

    //モーション用
    private bool actionBool;


    // Start is called before the first frame update
    void Start()
    {
        mainCameraController = MainCamera.GetComponent<MainCameraControllerold>();

        //characterController = GetComponent<CharacterController>();
        player1Rb = GetComponent<Rigidbody>();
        player2Rb = player2.GetComponent<Rigidbody>();
        player1Animation = GetComponent<Animator>();
        player2Animation = player2.GetComponent<Animator>();
        weakAttackObj = transform.Find("weakAttackCollider").gameObject;
        strongAttackObj = transform.Find("strongAttackCollider").gameObject;


        //初期化
        playerMove = Vector3.zero;
        moveDirection = Vector3.zero;
        player1Animation.SetBool("move", false);
        player2Animation.SetBool("move", false);
        actionBool = false;

    }

    // Update is called once per frame
    void Update()
    {
        cameraRotate();

        if (!actionBool)
        {
            Move(this.gameObject);
            Move(player2);

            if (Input.GetKeyDown(KeyCode.Z))
            {
                weakAttack();
            }
            else if (Input.GetKeyDown(KeyCode.X))
            {
                strongAttack();
            }
        }
    }

    
    /*プレイヤー移動*/
    private void Move(GameObject Character)
    {
        float moveZ = 0;
        float moveX = 0;
        if (Character == this.gameObject)
        {
            moveZ = Input.GetAxis("Vertical");
            moveX = Input.GetAxis("Horizontal");
        }
        else
        {
            Debug.Log("EEEEEEEEEEEEE");
            moveZ = Input.GetAxis("Vertical2");
            moveX = Input.GetAxis("Horizontal2");
        }

        /* 移動入力がされているとき */
        if(moveX != 0 || moveZ != 0)
        {
            //移動モーション
            if (Character == this.gameObject)
            {
                player1Animation.SetBool("move", true);
            }
            else
            {
                player2Animation.SetBool("move", true);
            }
            /*移動*/
            playerMove.x = moveX;
            playerMove.z = moveZ;

            
            playerMove.y = 0;
            playerMove = playerMove.normalized;

            //速度導出
            playerMove = playerMove * playerStatus.player_Speed * GameManagerold.speedMagnification;
            
            //1フレーム毎の移動量
            playerMove = playerMove * Time.deltaTime;

            //移動方向をカメラ角度加味
            playerMove = Quaternion.Euler(0, -(mainCameraController.rad * Mathf.Rad2Deg -270), 0) * playerMove;

            //移動
            if (Character == this.gameObject)
            {
                player1Rb.transform.position += new Vector3(playerMove.x, 0, playerMove.z);
            }
            else
            {
                Debug.Log("WWWWWWWWWW");
                player2Rb.transform.position += new Vector3(playerMove.x, 0, playerMove.z);
            }
            

            /*回転 */
            //方向単位ベクトル
            moveDirection = new Vector3(playerMove.x, 0, playerMove.z);
            characterRotate(moveDirection,Character);
        }
        else
        {
            /*移動アニメーション*/
            if (Character == this.gameObject)
            {
                player1Animation.SetBool("move", false);
            }
            else
            {
                player2Animation.SetBool("move", false);
            }
            //移動・回転用変数リセット
            playerMove = Vector3.zero;
            moveDirection = Vector3.zero;
        }
    }


    /*プレイヤー回転*/
    private void characterRotate(Vector3 moveDirection, GameObject Character)
    {
        //最終的な回転姿勢
        Quaternion playerRotate = Quaternion.LookRotation(moveDirection);


        /*//最終回転姿勢への回転割合
        float rotateSpeed = Time.deltaTime * GameManagerold.rotateSpeedMagnification * playerStatus.player_RotateSpeed;
        //割合が上限を超えていれば最大値に
        if(rotateSpeed > 1)
        {
            rotateSpeed = 1;
        }*/

        //最終回転姿勢の途中の姿勢
        playerRotate = Quaternion.Slerp(Character.transform.rotation, playerRotate, 0.3f);
        //playerへ反映
        Character.transform.rotation = playerRotate;
    }

    
    /* 弱攻撃 */
    private void weakAttack()
    {
        actionBool = true;
        player1Animation.SetTrigger("weakAttackTri");
    }

    private void weakAttackStart()
    {
        Debug.Log("C");
        weakAttackObj.SetActive(true);
    }

    private void weakAttackEnd()
    {
        Debug.Log("D");
        weakAttackObj.SetActive(false);
        actionBool = false;
    }

    /* 強攻撃 */
    private void strongAttack()
    {
        actionBool = true;
        player1Animation.SetTrigger("strongAttackTri");
    }

    private void strongAttackStart()
    {
        strongAttackObj.SetActive(true);
    }

    private void strongAttackEnd()
    {
        strongAttackObj.SetActive(false);
        actionBool = false;
    }

    //敵へのダメージを計算
    private void playerAttackDamage(EnemyController enemyController)
    {
        int damage = playerStatus.player_Attack - enemyController.thisEnemyState.Get_defense();

        enemyController.damageEnemy(damage);
    }


    //攻撃をくらったとき
    public void damagePlayer(int damage)
    {
        playerStatus.player_HP = playerStatus.player_HP - damage;
        if (playerStatus.player_HP <= 0)
        {
            Destroy(gameObject);
        }
    }


    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag + "タグ");
        //敵に攻撃が当たった場合
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("B");
            if (other.gameObject.GetComponent<EnemyController>() != null)
            {
                Debug.Log("C");
                playerAttackDamage(other.gameObject.GetComponent<EnemyController>());
            }
        }
    }






    /*カメラを回転させる*/
    private void cameraRotate()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            mainCameraController.rad += -5 * Time.deltaTime;
        }else if (Input.GetKey(KeyCode.W))
        {
            mainCameraController.rad += 5 * Time.deltaTime;
        }
    }
}
