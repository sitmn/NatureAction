using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyStatusData : ScriptableObject
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
		
		public string _enemyName;
		public int _enemyMaxHp;
		public int _enemyAttack;
		public int _enemyDefense;
		public float _enemySpeed;
		public int _enemyAttackDurationTime;
		public int _enemyAttackEndTime;
		public float _enemyAttackRangeMagnification;
	}
}

