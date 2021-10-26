using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JuelStatusData : ScriptableObject
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
		
		public string _juelName;
		public int _juelMaxHp;
		public int _juelAttack;
		public int _juelDefense;
		public int _juelAttackDurationTime;
		public float _juelAttackSpeed;
		public int _juelAttackSpan;
		public float _juelAttackRangeMagnification;
		public int _juelSearchRange;
	}
}

