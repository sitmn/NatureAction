using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillData : ScriptableObject
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
		
		public string _skillName;
		public float _skillAttackMagnification;
		public float _skillAttackRange;
		public int _skillStartTime;
		public int _skillEndTime;
		public int _skillDurationTime;
	}
}

