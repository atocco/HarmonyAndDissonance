using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_space_right : MonoBehaviour {

	private ability_bar_timer timescript;
	private ability_bar abilityscript;
	private position_keeper positionscript;
	private string playerOnSpace;	// name of character on this space

	// Use this for initialization
	void Start () {
		// find the time keeper and attached script

		// find the ability bar and attached script
		GameObject ab = GameObject.FindGameObjectWithTag ("abilitybar");
		abilityscript = ab.GetComponentInChildren<ability_bar> ();
		timescript = ab.GetComponent<ability_bar_timer> ();
		// find the positioner and the attached script
		GameObject ps = GameObject.FindGameObjectWithTag ("position");
		positionscript = ps.GetComponent<position_keeper> ();
	}
	
	// Update is called once per frame
	void Update () {
		playerOnSpace = positionscript.playerOnSpace [3];	// index three is RIGHT

		if (Input.GetKeyDown (KeyCode.RightArrow) && timescript.spriteIndex == 0) {	//check for timing + input
			//Debug.Log("right key was pressed");
			if (abilityscript.oneInput == 0) {
				abilityscript.pushAbility (playerOnSpace);
			} else {
				abilityscript.popAbilities ();
			}
		} else if (Input.GetKeyDown (KeyCode.RightArrow) && (timescript.spriteIndex == 1)) {
			//Debug.Log ("right key was missed");
			abilityscript.popAbilities ();
		}
	}
}
