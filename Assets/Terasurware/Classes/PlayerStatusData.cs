using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerStatusData : ScriptableObject
{	
	public List<Sheet> sheets = new List<Sheet> ();

	[System.SerializableAttribute]
	public class Sheet
	{
		public string name = string.Empty;
		public List<Param> list = new List<Param>();
	}

	[System.SerializableAttribute]
	public class Param
	{
		
		public int max_hp;
		public int max_mp;
		public int origin_attack;
		public int origin_defense;
		public int origin_speed;
		public int _attackRange;
		public int _attackDurationTime;
	}
}

