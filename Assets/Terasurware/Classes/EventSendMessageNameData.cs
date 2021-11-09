using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventSendMessageNameData : ScriptableObject
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
		
		public string _sendMessageName;
		public string _sceneName;
		public string _screenName;
		public bool _dungeonClearFlag;
	}
}

