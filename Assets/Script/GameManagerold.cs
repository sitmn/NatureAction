using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerold : MonoBehaviour
{
    //マスタデータ
    public PlayerStatusData playerStatusData;
    public SystemData systemData;
    public TrainingPointData trainingPointData;

    //トレーニング用2次配列　[トレーニングNo][各値]
    public int[,] TrainingArray;

    //プレイヤーステータス
    public GameObject Player1HPSlider;
    public GameObject Player2HPSlider;
    public PlayerStatus playerStatus1;
    public PlayerStatus playerStatus2;

    //プレイヤーのHPバー
    private Slider player1HPSlider;
    private Slider player2HPSlider;

    //システムデータ
    public float main_menu_button_distance;

    public float button_move_time;


    //システム　前
    public static float speedMagnification;
    public static float rotateSpeedMagnification;
    public static float gravity;

    //カメラ　前
    public static float cameraDistancePlayerY;
    public static float cameraDistancePlayer;
    public static float cameraPositionDelay;
    public static float cameraRotateOffsetY;


    void Awake()
    {
        playerStatusData = Resources.Load("PlayerStatusData") as PlayerStatusData;
        systemData = Resources.Load("SystemData") as SystemData;
        trainingPointData = Resources.Load("trainingPointData") as TrainingPointData;

        player1HPSlider = Player1HPSlider.GetComponent<Slider>();
        player2HPSlider = Player2HPSlider.GetComponent<Slider>();

        //if()   ニューゲームを指定した場合
        PlayerStatusSet(false);
        SystemSet(false);
        TrainingPointSet();

        //else if  続けるを指定した場合
        /*セットする値を保存していたバイナリにする（要勉強）
        //PlayerStatusSet(true);
        //SystemSet(true);
    }

    // Start is called before the first frame update
    void Initialize()
    {/*
        masterData = Resources.Load<MasterDataRepository>("MasterDataRepository");

        /*プレイヤーステータス*/
        /*playerStatus.player_HP = masterData.GetPlayerState(1).playerHP;
        playerStatus.player_Attack = masterData.GetPlayerState(1).playerAttack;
        playerStatus.player_Defense = masterData.GetPlayerState(1).playerDefense;
        playerStatus.player_Speed = masterData.GetPlayerState(1).playerSpeed;
        playerStatus.player_RotateSpeed = playerStatus.player_Speed / 1.5f;

        /*システム定数*/
        /*speedMagnification = masterData.GetSystemParameter(1).speedMagnification;
        rotateSpeedMagnification = masterData.GetSystemParameter(1).rotateSpeedMagnification;
        gravity = masterData.GetSystemParameter(1).gravity;
        //カメラ
        /*cameraDistancePlayerY = masterData.GetSystemParameter(1).cameraDistancePlayerY;
        cameraDistancePlayer = masterData.GetSystemParameter(1).cameraDistancePlayer;
        cameraPositionDelay = masterData.GetSystemParameter(1).cameraPositionDelay;
        cameraRotateOffsetY = masterData.GetSystemParameter(1).cameraRotateOffsetY;
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void PlayerStatusSet(bool load_flag)
    {
       // playerStatus1 = new PlayerStatus();
       // playerStatus2 = new PlayerStatus();
        
        /*if (load_flag == false)
        {
            playerStatus1.Set_max_hp(playerStatusData.sheets[0].list[0].max_hp);
            playerStatus1.Set_hp(playerStatus1.Get_max_hp());
            playerStatus1.Set_max_mp(playerStatusData.sheets[0].list[0].max_mp);
            playerStatus1.Set_mp(playerStatus1.Get_max_mp());
            playerStatus1.Set_origin_attack(playerStatusData.sheets[0].list[0].origin_attack);
            playerStatus1.Set_attack(playerStatus1.Get_origin_attack());
            playerStatus1.Set_origin_defense(playerStatusData.sheets[0].list[0].origin_defense);
            playerStatus1.Set_defense(playerStatus1.Get_origin_defense());
            playerStatus1.Set_origin_speed(playerStatusData.sheets[0].list[0].origin_speed);
            playerStatus1.Set_speed(playerStatus1.Get_origin_speed());

            playerStatus2.Set_max_hp(playerStatusData.sheets[0].list[1].max_hp);
            playerStatus2.Set_hp(playerStatus1.Get_max_hp());
            playerStatus2.Set_max_mp(playerStatusData.sheets[0].list[1].max_mp);
            playerStatus2.Set_mp(playerStatus1.Get_max_mp());
            playerStatus2.Set_origin_attack(playerStatusData.sheets[0].list[1].origin_attack);
            playerStatus2.Set_attack(playerStatus1.Get_origin_attack());
            playerStatus2.Set_origin_defense(playerStatusData.sheets[0].list[1].origin_defense);
            playerStatus2.Set_defense(playerStatus1.Get_origin_defense());
            playerStatus2.Set_origin_speed(playerStatusData.sheets[0].list[1].origin_speed);
            playerStatus2.Set_speed(playerStatus1.Get_origin_speed());
        }
        else if(load_flag == true)
        {

        }*/
    }

    private void SystemSet(bool load_flag)
    {
        if (load_flag == false)
        {
            main_menu_button_distance = systemData.sheets[0].list[0].main_menu_button_distance;
            button_move_time = systemData.sheets[0].list[0].button_move_time;

        }else if(load_flag == true)
        {
            //日付とか、セーブする必要のあるもの
        }
    }

    //値は常に定数（セーブする必要なし）なので、load_flagによる条件分岐はなし
    private void TrainingPointSet()
    {
        TrainingArray = new int[5, 8];
        /*
        TrainingArray[0,0] = trainingPointData.sheets[0].list[0].health_tired;
        TrainingArray[0, 1] = trainingPointData.sheets[0].list[0].health_hp_base;
        TrainingArray[0, 2] = trainingPointData.sheets[0].list[0].health_mp_base;
        TrainingArray[0, 3] = trainingPointData.sheets[0].list[0].health_attack_base;
        TrainingArray[0, 4] = trainingPointData.sheets[0].list[0].health_defense_base;
        TrainingArray[0, 5] = trainingPointData.sheets[0].list[0].health_speed_base;
        TrainingArray[0, 6] = trainingPointData.sheets[0].list[0].health_technique_base;
        TrainingArray[0, 7] = trainingPointData.sheets[0].list[0].health_range_base;
        TrainingArray[1,0] = trainingPointData.sheets[0].list[0].attack_tired;
        TrainingArray[1, 1]= trainingPointData.sheets[0].list[0].attack_hp_base;
        TrainingArray[1, 2]= trainingPointData.sheets[0].list[0].attack_mp_base;
        TrainingArray[1, 3]= trainingPointData.sheets[0].list[0].attack_attack_base;
        TrainingArray[1, 4]= trainingPointData.sheets[0].list[0].attack_defense_base;
        TrainingArray[1, 5]= trainingPointData.sheets[0].list[0].attack_speed_base;
        TrainingArray[1, 6]= trainingPointData.sheets[0].list[0].attack_technique_base;
        TrainingArray[1, 7]= trainingPointData.sheets[0].list[0].attack_range_base;
        TrainingArray[2,0] = trainingPointData.sheets[0].list[0].defense_tired;
        TrainingArray[2, 1]= trainingPointData.sheets[0].list[0].defense_hp_base;
        TrainingArray[2, 2]= trainingPointData.sheets[0].list[0].defense_mp_base;
        TrainingArray[2, 3]= trainingPointData.sheets[0].list[0].defense_attack_base;
        TrainingArray[2, 4]= trainingPointData.sheets[0].list[0].defense_defense_base;
        TrainingArray[2, 5]= trainingPointData.sheets[0].list[0].defense_speed_base;
        TrainingArray[2, 6]= trainingPointData.sheets[0].list[0].defense_technique_base;
        TrainingArray[2, 7]= trainingPointData.sheets[0].list[0].defense_range_base;
        TrainingArray[3,0] = trainingPointData.sheets[0].list[0].speed_tired;
        TrainingArray[3, 1]= trainingPointData.sheets[0].list[0].speed_hp_base;
        TrainingArray[3, 2]= trainingPointData.sheets[0].list[0].speed_mp_base;
        TrainingArray[3, 3]= trainingPointData.sheets[0].list[0].speed_attack_base;
        TrainingArray[3, 4]= trainingPointData.sheets[0].list[0].speed_defense_base;
        TrainingArray[3, 5]= trainingPointData.sheets[0].list[0].speed_speed_base;
        TrainingArray[3, 6]= trainingPointData.sheets[0].list[0].speed_technique_base;
        TrainingArray[3, 7]= trainingPointData.sheets[0].list[0].speed_range_base;
        TrainingArray[4,0] = trainingPointData.sheets[0].list[0].searchenemy_tired;
        TrainingArray[4, 1]= trainingPointData.sheets[0].list[0].searchenemy_hp_base;
        TrainingArray[4, 2]= trainingPointData.sheets[0].list[0].searchenemy_mp_base;
        TrainingArray[4, 3]= trainingPointData.sheets[0].list[0].searchenemy_attack_base;
        TrainingArray[4, 4]= trainingPointData.sheets[0].list[0].searchenemy_defense_base;
        TrainingArray[4, 5]= trainingPointData.sheets[0].list[0].searchenemy_speed_base;
        TrainingArray[4, 6]= trainingPointData.sheets[0].list[0].searchenemy_technique_base;
        TrainingArray[4, 7] = trainingPointData.sheets[0].list[0].searchenemy_range_base;
        */
    }

    //HPSlider増減用
    /*public void HpSlider1UpDown()
    {
        player1HPSlider.value = playerStatus1.Get_hp() / playerStatus1.Get_max_hp();
    }
    public void HpSlider2UpDown()
    {
        player2HPSlider.value = playerStatus2.Get_hp() / playerStatus2.Get_max_hp();
    }*/
}

//前
public static class playerStatus{
    //プレイヤー
    public static int player_HP;
    public static int player_MP;
    public static int player_Attack;
    public static int player_Defense;
    public static float player_Speed;
    public static float player_RotateSpeed;

    public static float player_WeaponAttack;

    public static GameObject weakAttackCollider;
    public static GameObject strongAttackCollider;


    //武器による当たり判定とダメージのつけかえ
    public static int weakAttackType;
    public static int WeakAttackType {
        set { 
            if(value == 1)
            {
                //既についている子コライダーを消す
                //weakAttackCollider = ColliderList[value];
                //playerの子にする
                weakAttackType = value;
            }
            else if(value == 2)
            {

            }
            else if (value == 3)
            {

            }
             
        
        }
    
    }

    //武器による当たり判定とダメージのつけかえ
    public static int strongAttackType;
    public static int StrongAttackType {
        set{
            //strongAttackCollider = 
            strongAttackType = value;
        }
    }
}


/*以下、宝石のステータス*/
//前
public class juelStatus
{
    public int juel_HP;             //宝石のHP
    public int juel_Attack;         //宝石の攻撃
    public int juel_AttackSpan;    //宝石の攻撃間隔（フレーム）
    public float juel_AttackSpeed;   //攻撃弾の速さ
}


//前
public class rubyStatus : juelStatus {

    public rubyStatus(){
        juel_HP = 10;
        juel_Attack = 3;
        juel_AttackSpan = 200;
        juel_AttackSpeed = 0.2f;
    }
}

//前
public class aquamarineStatus : juelStatus
{

    public aquamarineStatus()
    {
        juel_HP = 7;
        juel_Attack = 1;
        juel_AttackSpan = 300;
        juel_AttackSpeed = 0.1f;
    }
}