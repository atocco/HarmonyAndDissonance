using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_object : MonoBehaviour {

	// This class will hold all relevent player information for each player character during the boss battle phase
	// EXAMPLE USE:
	// public player_object.character po;
	// po = new player_object.character (true, 10, "nice");
	// Debug.Log (po.Class_Name);

	// establish the stats and properties of the characters
	public class character
	{
		private bool alive;
		private int health;
		private string class_name; // as in character class

		public character (){	// constructor
			alive = true;
			health = 100;
			class_name = "null";
		}
		public character (bool a, int h, string cn){	// params with constructor
			alive = a;
			health = h;
			class_name = cn;
		}

		// PROPERTIES FOR THE STATS
		public bool Alive{
			get
			{
				return alive;
			}
			set
			{
				alive = value;
			}
		}
		public int Health{
			get
			{
				return health;
			}
			set
			{
				health = value;
			}
		}
		public string Class_Name{
			get
			{
				return class_name;
			}
			set
			{
				class_name = value;
			}
		}
	}
}