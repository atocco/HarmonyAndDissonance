using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script controls the player animations during a fight sequence

public class player_fight_animations : MonoBehaviour {

	public float animationspeed;
	public string direction;

	private Animator animator;
	private player_object pscript;
	private AudioSource audsrc;
	private GameObject music;

	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator> ();
		pscript = this.GetComponent<player_object> ();
		music = GameObject.FindGameObjectWithTag ("music");		// load the music player
		audsrc = music.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (audsrc.isPlaying) {
		
			if (pscript.alive == false) {
				animator.speed = 0.5f;
				animator.SetInteger ("state", 4);
			} else {
				animator.speed = animationspeed;	// set speed to be in time with the song
			}

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
		
		} else {
			animator.speed = 0.0f;
		}
	}

	public void abilityCast(){				// public function to invoke the ability trigger
		animator.SetTrigger ("attack");		// trigger the ability cast animation
	}
}
