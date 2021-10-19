using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MagicStoneRaitoData : ScriptableObject
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
		
		public string stone_name;
		public float health_training_stone_raito;
		public float attack_training_stone_raito;
		public float defense_training_stone_raito;
		public float speed_training_stone_raito;
		public float search_training_stone_raito;
		public float magic_stone_raito;
		public float same_consume_stone_coefficient;
	}
}

