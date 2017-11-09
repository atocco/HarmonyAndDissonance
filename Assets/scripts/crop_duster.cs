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
		// get audio stuff
		audsrc = this.GetComponent<AudioSource> ();
	}
	
	// LateUpdate is called once per frame after the normal update so boss attacks have lower priority
	void LateUpdate () {
		if (beatCounter != timekeeper.beatCounter) {
			if (boss.stunned == false) {		// don't do anything if you're stunned
				
				if (actionCycle == 7) {		// he telegraphs his attack 4 beats ahead
					// telegraph
					boss.telegraph ();		// change color
				}
				if (actionCycle == 11) {		// he attacks

					boss.endTelegraph (); 	// end telegraph color

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
	private void gust(){		// basic attack that hits 3 characters
		int notHit = Random.Range (0,4);	// pick number 0 thru 3
		int dmg = 0;
		int index = 0;	// keep track of who to hit

		animator.SetTrigger ("gust");	 // play animation
		audsrc.clip = gustAud;			// play sound
		audsrc.Play ();

		if (boss.gimped == true) {				// assign damage
			dmg = boss.basedmg / 2;
		} else {
			dmg = boss.basedmg;
		}

		foreach (GameObject p in players){		// hit players
			if (index != notHit) {
				p.GetComponent<player_object> ().takeHit (dmg);
			}
			index++;	// increment
		}
	}

	private void windBlade(){		// strong attack that hits one character
		int hit = Random.Range(0,4);
		int dmg = 0;

		animator.SetTrigger ("gust");	// play animation
		audsrc.clip = windBladeAud;			// play sound
		audsrc.Play ();

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

		animator.SetTrigger ("pesticide"); // play animation
		audsrc.clip = dustAud;			// play sound
		audsrc.Play ();

		if (boss.gimped == true) {				// assign damage
			dmg = boss.basedmg / 2;
		} else {
			dmg = boss.basedmg;
		}

		players [hit].GetComponent<player_object> ().takeHit (dmg);	// hit one guy
		players [hit].GetComponent<player_object> ().poison ();	// poison one guy
	}

}
