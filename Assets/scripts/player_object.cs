using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_object : MonoBehaviour {

	public bool alive;
	public int health;
	private int maxHP;			// total health for the character
	public string class_name; 	// as in character class [panda, bee, bird, rabbit]
	public string position;		// [up, down, left, or right]
	public int basedmg;
	private int dmg;			// new damage after mods

	private bool poisoned;

	private bool empowered;
	private int empowerCounter;		// keeps track of empowerment
	private int empowerLimit;		// holds the number of beats empowerment lasts

	private bool defending;
	private int defenseCounter;		// keeps track of defense
	private int defenseLimit;		// holds the number of beats defense lasts

	private time_keeper timekeeper;	// to keep track of the beats that have passed

	private float ogTimewindow;		// initial value of timing window
	private bool harmonizing;		// for harmony ability
	private int harmonyCounter;		// keeps track of harmony timing
	private int harmonyLimit;		// holds the number of beats harmony ability lasts

	private int beatCounter;

	private SpriteRenderer r;	// get the renderer to change colors
	private Color basecolor;

	void Start(){
		// get timing objects
		GameObject tk = GameObject.FindGameObjectWithTag ("timekeeper");
		timekeeper = tk.GetComponent<time_keeper> ();
		// record timing stuff
		ogTimewindow = timekeeper.input_window;
		// setting status variables
		empowered = false;
		poisoned = false;
		defending = false;
		// record max hp
		maxHP = health;
		beatCounter = 0;
		// record starting color
		r = this.GetComponent<SpriteRenderer> ();
		basecolor = r.color;
	}

	void Update(){
		
		if (alive == false) {					// player is kill
			this.GetComponent<SpriteRenderer> ().enabled = false;
		}

		if (poisoned == true) {			// coloring for flavor
			r.color = Color.green;
		}
		if (health <= 35) {				// indicates low hp
			r.color = Color.gray;
		}

		if (beatCounter != timekeeper.beatCounter){		// when a new beat hits apply effects
			
			if (poisoned == true){
				this.applyPoison (2);	// take poison dmg
			}

			if (empowered == true) {
				if (empowerLimit == (beatCounter - empowerCounter)) {		// check if time limit up for empowerment
					empowered = false;
				}
			}

			if (defending == true) {
				if (defenseLimit == (beatCounter - defenseCounter)) {		// check if time limit up for defending
					defending = false;
				}
			}

			if (harmonizing == true) {
				if (harmonyLimit == (beatCounter - harmonyCounter)) {		// check if time limit up for harmonizing
					harmonizing = false;
					timekeeper.input_window = ogTimewindow;					// reset timing window length
				}
			}

			r.color = basecolor; 				// reset char color
			beatCounter = timekeeper.beatCounter;		// update the beat counter
		}
	}


	// damage related methods

	public int attack(){		// returns damage dealt
		if (empowered) {
			return dmg;
		} else {
			return basedmg;
		}
	}

	public void takeHit(int dmg){
		r.color = Color.red;
		if (defending) {
			dmg = dmg - 5;		// apply defense value
			if (dmg < 0) {
				dmg = 0;
			}
		} else {
			health = health - dmg;		// assign damage
			if (health <= 0){			// u good, bro?
				health = 0;
				alive = false;
			}
		}
	}

	public void heal(int hp){
		r.color = Color.yellow;		// color when healed
		int newHP = health + hp;	// get new health 
		if (newHP <= maxHP) {		// prevent hp overflow
			health = newHP;
		} else {
			health = maxHP;
		}
	}


	// status effect methods
	public void poison(){
		poisoned = true;
	}

	private void applyPoison(int dmg){		// deals unblockable damage
		health = health - dmg;		// assign damage
		if (health <= 0){			// u good, bro?
			health = 0;
			alive = false;
		}
	}

	public void empower(){
		empowered = true;
		empowerCounter = beatCounter;	// current beat is held
		empowerLimit = 5;				// empower always lasts 5 beats
		dmg = basedmg + 10;				// empower is always + 10 dmg
	}

	public void defend(){
		defending = true;
		defenseCounter = beatCounter;
		defenseLimit = 3;
	}
		
	public void cured(){
		poisoned = false;
	}

	// unique player abilities
	public void buffHarmony(){
		// doubles window of time players have for input
		if (!harmonizing){			// can't stack
			harmonizing = true;
			harmonyCounter = beatCounter;
			harmonyLimit = 9;
			timekeeper.input_window = ogTimewindow * 2.0f;
		}
	}

}