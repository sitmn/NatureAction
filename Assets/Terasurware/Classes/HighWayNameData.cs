using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HighWayNameData : ScriptableObject
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
		
		public string _sceneName;
		public string _townName;
		public string _townGateName;
		public float _startPosX;
		public float _startPosZ;
		public float _startPosY;
	}
}

