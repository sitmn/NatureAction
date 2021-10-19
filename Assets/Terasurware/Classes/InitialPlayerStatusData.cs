using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InitialPlayerStatusData : ScriptableObject
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
		
		public string 対象プレイヤー;
		public int _initialHealth;
		public int _initialHp;
		public int _initialMp;
		public int _initialAttack;
		public int _initialDefense;
		public int _initialSpeed;
	}
}

