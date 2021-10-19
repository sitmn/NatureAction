using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrainingPointData : ScriptableObject
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
		
		public int trainingNo;
		public string _trainingName;
		public int _tired;
		public int _hpBase;
		public int _mpBase;
		public int _attackBase;
		public int _defenseBase;
		public int _speedBase;
		public float _purpleStoneConsumeRaito;
		public float _redStoneConsumeRaito;
		public float _blueStoneConsumeRaito;
		public float _greenStoneConsumeRaito;
		public float _yellowStoneConsumeRaito;
		public int _magicStoneRaito;
		public float _sameConsumeStoneCoefficient;
	}
}

