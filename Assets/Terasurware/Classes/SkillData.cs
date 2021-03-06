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
		public int _skillMP;
		public float _skillAttackMagnification;
		public float _skillAttackRange;
		public float _skillAnimationLength;
		public int _skillEndTime;
		public int _skillDurationTime;
		public float _skillSpeed;
		public string _skillDetail;
		public string _skillTypeName;
		public int _consumePurpleStone;
		public int _consumeRedStone;
		public int _consumeBlueStone;
		public int _consumeGreenStone;
		public int _consumeYellowStone;
		public AnimationClip _skillAnimation;
		public GameObject _skillColliderObj;
		public Sprite _skillImage;
		public AudioClip _skillAudioClip;
	}
}

