using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : CharacterStatus
{
    public DungeonMasterData MasterData;
    private int enemy_no;
    public EnemyState(string name)
    {
        //タグの名前から、エクセルデータの番号を取得
        if (name.Contains("Bat"))
        {
            enemy_no = 0;
        }
        else if (name.Contains("Ghost"))
        {
            enemy_no = 1;
        }
        else
        {
            Debug.Log("想定されたタグの敵でないため、ステータスが生成できません");
            Destroy(this);
        }


        //エクセルデータから敵のステータスを取得
        Set_max_hp(MasterData.enemyStatusData.sheets[0].list[enemy_no].max_hp);
        Set_hp(Get_max_hp());
        Set_max_mp(MasterData.enemyStatusData.sheets[0].list[enemy_no].max_mp);
        Set_mp(Get_max_mp());
        Set_origin_attack(MasterData.enemyStatusData.sheets[0].list[enemy_no].origin_attack);
        Set_attack(Get_attack());
        Set_origin_defense(MasterData.enemyStatusData.sheets[0].list[enemy_no].origin_defense);
        Set_attack(Get_defense());
        Set_origin_speed(MasterData.enemyStatusData.sheets[0].list[enemy_no].origin_speed);
        Set_speed(Get_speed());
        Set_technique(MasterData.enemyStatusData.sheets[0].list[enemy_no].technique);
        Set_range(MasterData.enemyStatusData.sheets[0].list[enemy_no].range);
    }


}
