using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_space_up : MonoBehaviour {

	private time_keeper timescript;
	private ability_bar abilityscript;
	private position_keeper positionscript;
	private string playerOnSpace;	// name of character on this space

	// Use this for initialization
	void Start () {
		// find the time keeper and attached script
		GameObject tk = GameObject.FindGameObjectWithTag ("timekeeper");
		timescript = tk.GetComponent<time_keeper> ();
		// find the ability bar and attached script
		GameObject ab = GameObject.FindGameObjectWithTag ("abilitybar");
		abilityscript = ab.GetComponent<ability_bar> ();
		// find the positioner and the attached script
		GameObject ps = GameObject.FindGameObjectWithTag ("position");
		positionscript = ps.GetComponent<position_keeper> ();
	}
	
	// Update is called once per frame
	void Update () {
		playerOnSpace = positionscript.playerOnSpace [0];	// index zero is UP

		if (Input.GetKeyDown (KeyCode.UpArrow) && timescript.validInput) {	//check for timing + input
			Debug.Log ("up key was pressed");
			abilityscript.pushAbility (playerOnSpace);
		} else if (Input.GetKeyDown (KeyCode.UpArrow) && !timescript.validInput) {
			Debug.Log ("up key was missed");
			abilityscript.popAbilities ();
		}
	}
}