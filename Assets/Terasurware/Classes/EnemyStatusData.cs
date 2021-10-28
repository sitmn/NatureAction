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
		public int _enemySpeed;
		public int _enemyDistance;
		public int _enemyAttackDurationTime;
		public int _enemyAttackEndTime;
		public float _enemyAttackSpeed;
		public int _enemyAttackRange;
		public int _enemySearchRange;
		public AudioClip _enemyAttackAudio;
		public AudioClip _enemyDamageAudio;
	}
}

