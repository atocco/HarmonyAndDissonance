using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script controls the player animations during a fight sequence

public class player_fight_animations : MonoBehaviour {

	public float animationspeed;
	public string direction;

	Animator animator;
	player_object pscript;

	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator> ();
		pscript = this.GetComponent<player_object> ();
	}
	
	// Update is called once per frame
	void Update () {
		direction = pscript.position;	// reconcile the animation direction with current player position
		switch (direction) {
		case "up":
			animator.SetInteger ("state", 1);		// IMPORTANT: if they are in position up, they are FACING down!!!
			break;
		case "down":
			animator.SetInteger ("state", 0);
			break;
		case "left":
			animator.SetInteger ("state", 3);		// IMPORTANT: if they are in position left, they are FACING right!!!
			break;
		case "right":
			animator.SetInteger ("state", 2);
			break;
		default:
			Debug.Log("error in the fight animation script!!!");
			break;
		}
	}

	public void abilityCast(){				// public function to invoke the ability trigger
		animator.SetTrigger ("attack");		// trigger the ability cast animation
	}
}
