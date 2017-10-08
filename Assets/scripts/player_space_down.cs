using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_space_down : MonoBehaviour {

	private time_keeper timescript;

	// Use this for initialization
	void Start () {
		GameObject tk = GameObject.FindGameObjectWithTag ("timekeeper");	// find the time keeper
		timescript = tk.GetComponent<time_keeper> ();						//find the time keep script
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.DownArrow) && timescript.validInput) {	//check for timing + input
			Debug.Log("down key was pressed");
		} else if (Input.GetKeyDown (KeyCode.DownArrow) && !timescript.validInput) {
			Debug.Log ("down key was missed");
		}
	}
}
