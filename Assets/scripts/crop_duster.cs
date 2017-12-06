using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this is the script that controls the behavior of the boss for the crop duster battle

public class crop_duster : MonoBehaviour {

	private Animator animator;
	private time_keeper timekeeper;	// to keep track of the beats that have passed
	private int beatCounter;
	private int actionCycle;		// modulus value that keeps track of where the duster is in his cycle
	public GameObject[] players;	// the players
	private boss_object boss;		// get generic boss script
	// get sounds set up
	public AudioClip gustAud;
	public AudioClip dustAud;
	public AudioClip windBladeAud;
	public AudioClip telegraphAud;
	private AudioSource audsrc;
	// positioning info and attack animations for directional attacks
	public GameObject[] dusts;		// UP, DOWN, LEFT, RIGHT
	public GameObject[] gusts;


	// Use this for initialization
	void Start () {
		// get timing stuff
		GameObject tk = GameObject.FindGameObjectWithTag ("timekeeper");
		timekeeper = tk.GetComponent<time_keeper> ();
		beatCounter = 0;
		// get stats
		boss = this.GetComponent<boss_object> ();
		// get animations
		animator = this.GetComponent<Animator> ();
		animator.speed = .75f;		// because this is an 80bpm fight
		// get audio stuff
		audsrc = this.GetComponent<AudioSource> ();
	}
	
	// LateUpdate is called once per frame after the normal update so boss attacks have lower priority
	void LateUpdate () {
		if (beatCounter != timekeeper.beatCounter) {
			if (boss.stunned == false) {		// don't do anything if you're stunned
				
				if (actionCycle == 7) {		// he telegraphs his attack 4 beats ahead
					// telegraph
					audsrc.clip = telegraphAud;			// play sound
					audsrc.Play ();				// wind up sound
				}
				if (actionCycle == 11) {		// he attacks

					int atk = Random.Range (0, 5);
					if (atk == 4) {
						// 20 percent chance
						this.windBlade ();
					} else if (atk == 3) {
						// 20 percent chance
						this.dust ();
					} else {
						// most common attack (60 percent)
						this.gust ();
					}
				}


				actionCycle = (actionCycle + 1) % 12;			// update cycle pos
			}
				
			beatCounter = timekeeper.beatCounter;	// update beat number
		}

	}


	// attacks
	private void gust(){		// basic attack that hits all characters
		int dmg = 0;

		this.transform.localScale = new Vector3 (0.3f, 0.3f, 1f);
		animator.SetTrigger ("gust");	 // play animation
		this.transform.localScale = new Vector3 (0.15f, 0.15f, 1f);
		audsrc.clip = gustAud;			// play sound
		audsrc.Play ();

		if (boss.gimped == true) {				// assign damage
			dmg = boss.basedmg / 2;
		} else {
			dmg = boss.basedmg;
		}

		foreach (GameObject p in players){		// hit players
			p.GetComponent<player_object> ().takeHit (dmg);
		}
	}

	private void windBlade(){		// strong attack that hits one character
		int hit = Random.Range(0,4);
		int dmg = 0;

		//animator.SetTrigger ("gust");	// play animation
		audsrc.clip = windBladeAud;			// play sound
		audsrc.Play ();

		// find the player position and playing correct attack animation for it
		string targetdir = players [hit].GetComponent<player_object> ().position;
		GameObject clone;
		Vector3 pos = players [hit].GetComponent<player_object> ().transform.position;		// establish the location of the sprite
		switch(targetdir){
		case "up":
			clone = Instantiate (gusts[0], pos, this.transform.rotation);	// create the sprite
			Destroy (clone, clone.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).length);
			break;
		case "down":
			clone = Instantiate (gusts[1], pos, this.transform.rotation);	// create the sprite
			Destroy (clone, clone.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).length);
			break;
		case "left":
			clone = Instantiate (gusts[2], pos, this.transform.rotation);	// create the sprite
			Destroy (clone, clone.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).length);
			break;
		case "right":
			clone = Instantiate (gusts[3], pos, this.transform.rotation);	// create the sprite
			Destroy (clone, clone.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).length);
			break;
		default:
			Debug.Log("error in the gust animation script!!!");
			break;
		}

		if (boss.gimped == true) {				// assign damage
			dmg = boss.basedmg;
		} else {
			dmg = boss.basedmg * 2;
		}

		players [hit].GetComponent<player_object> ().takeHit (dmg);	// hit one guy
	}

	private void dust(){		// attack that hits and poisons one character
		int hit = Random.Range(0,4);
		int dmg = 0;

		//animator.SetTrigger ("pesticide"); // play animation
		audsrc.clip = dustAud;			// play sound
		audsrc.Play ();

		// find the player position and playing correct attack animation for it
		string targetdir = players [hit].GetComponent<player_object> ().position;
		GameObject clone;
		Vector3 pos = players [hit].GetComponent<player_object> ().transform.position;		// establish the location of the sprite
		switch(targetdir){
		case "up":
			clone = Instantiate (dusts[0], pos, this.transform.rotation);	// create the sprite
			Destroy (clone, clone.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).length);
			break;
		case "down":
			clone = Instantiate (dusts[1], pos, this.transform.rotation);	// create the sprite
			Destroy (clone, clone.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).length);
			break;
		case "left":
			clone = Instantiate (dusts[2], pos, this.transform.rotation);	// create the sprite
			Destroy (clone, clone.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).length);
			break;
		case "right":
			clone = Instantiate (dusts[3], pos, this.transform.rotation);	// create the sprite
			Destroy (clone, clone.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0).length);
			break;
		default:
			Debug.Log("error in the dust animation script!!!");
			break;
		}

		if (boss.gimped == true) {				// assign damage
			dmg = boss.basedmg / 2;
		} else {
			dmg = boss.basedmg;
		}

		players [hit].GetComponent<player_object> ().takeHit (dmg);	// hit one guy
		players [hit].GetComponent<player_object> ().poison ();	// poison one guy
	}

}
