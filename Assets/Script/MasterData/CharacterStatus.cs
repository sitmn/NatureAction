using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterStatus : ScriptableObject
{
    private int max_hp;
    private int hp;
    private int max_mp;
    private int mp;
    private int origin_attack;
    private int attack;
    private int origin_defense;
    private int defense;
    private int origin_speed;
    private int speed;
    private int technique;
    private int range;

    public int Get_max_hp()
    {
        return max_hp;
    }
    public int Get_hp()
    {
        return hp;
    }
    public int Get_max_mp()
    {
        return max_mp;
    }
    public int Get_mp()
    {
        return mp;
    }
    public int Get_origin_attack()
    {
        return origin_attack;
    }
    public int Get_attack()
    {
        return attack;
    }
    public int Get_origin_defense()
    {
        return origin_defense;
    }
    public int Get_defense()
    {
        return defense;
    }
    public int Get_origin_speed()
    {
        return origin_speed;
    }
    public int Get_speed()
    {
        return speed;
    }
    public int Get_technique()
    {
        return technique;
    }
    public int Get_range()
    {
        return range;
    }


    public void Set_max_hp(int set_max_hp)
    {
        max_hp = set_max_hp;
        if(max_hp < 1)
        {
            max_hp = 1;
        }
    }
    public void Set_hp(int set_hp)
    {
        hp = set_hp;
        if(hp < 0)
        {
            hp = 0;
        }
    }
    public void Set_max_mp(int set_max_mp)
    {
        max_mp = set_max_mp;
        if (max_mp < 1)
        {
            max_mp = 1;
        }
    }
    public void Set_mp(int set_mp)
    {
        mp = set_mp;
        if (mp < 0)
        {
            mp = 0;
        }
    }
    public void Set_origin_attack(int set_origin_attack)
    {
        origin_attack = set_origin_attack;
        if (origin_attack < 1)
        {
            origin_attack = 1;
        }
    }
    public void Set_attack(int set_attack)
    {
        attack = set_attack;
        if (attack < 1)
        {
            attack = 1;
        }
    }
    public void Set_origin_defense(int set_origin_defense)
    {
        origin_defense = set_origin_defense;
        if (origin_defense < 1)
        {
            origin_defense = 1;
        }
    }
    public void Set_defense(int set_defense)
    {
        defense = set_defense;
        if (defense < 1)
        {
            defense = 1;
        }
    }
    public void Set_origin_speed(int set_origin_speed)
    {
        origin_speed = set_origin_speed;
        if (origin_speed < 1)
        {
            origin_speed = 1;
        }
    }
    public void Set_speed(int set_speed)
    {
        speed = set_speed;
        if (speed < 1)
        {
            speed = 1;
        }
    }
    public void Set_technique(int set_technique)
    {
        technique = set_technique;
        if (technique < 1)
        {
            technique = 1;
        }
    }
    public void Set_range(int set_range)
    {
        range = set_range;
        if (range < 1)
        {
            range = 1;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
