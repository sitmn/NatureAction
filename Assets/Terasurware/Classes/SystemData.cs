using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SystemData : ScriptableObject
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
		
		public float button_speed;
		public float main_menu_button_distance;
		public float button_move_time;
		public float slideTime;
	}
}

