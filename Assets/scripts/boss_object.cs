using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this is a generic class for keeping track of boss parameters as well as having a master list of all their abilities
// this works in conjunction with a boss specific behavior script

public class boss_object : MonoBehaviour {

	public int health;
	private int maxHP;
	public bool alive;
	public int basedmg;

	private Animator animator;

	private time_keeper timekeeper;	// to keep track of the beats that have passed
	private int beatCounter;

	private bool poisoned;
	private int poisonCounter;		// keep track of time it was poisoned and how long it will last
	private int poisonLimit;

	private int stunCounter;		// keep track of stun info
	private int stunLimit;
	public bool stunned;

	private int gimpCounter;		// keep track of gimp info
	private int gimpLimit;
	public bool gimped;

	private SpriteRenderer r;		// coloring
	private Color basecolor;
	private Color tempColor;
	private Color realcolor;

	// status icons
	public GameObject poisonIcon;
	//private GameObject stunIcon;
	public GameObject attackIcon;
	public GameObject healIcon;
	// location for status icons
	private float py;
	private float px;

	// Use this for initialization
	void Start () {
		maxHP = health;		// record total hp

		// set status
		poisoned = false;
		stunned = false;
		gimped = false;

		// get timing stuff
		GameObject tk = GameObject.FindGameObjectWithTag ("timekeeper");
		timekeeper = tk.GetComponent<time_keeper> ();
		beatCounter = 0;

		// get animation script
		animator = this.GetComponent<Animator> ();

		// assign colors
		r = this.GetComponent<SpriteRenderer> ();
		basecolor = r.color;
		realcolor = r.color;

		// icon setup
		//poisonIcon = GameObject.Find ("psn");
		//stunIcon = GameObject.Find ("stun");
		//attackIcon = GameObject.Find ("atk");
		//healIcon = GameObject.Find ("heal");
		// record position
		px = this.transform.position.x;
		py = this.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {

		if (alive == false) {					// boss is kill
			//this.GetComponent<SpriteRenderer> ().enabled = false;
			r.color = realcolor;
			this.GetComponent<Animator> ().SetInteger ("state", 5); // 5 means the death state
			this.GetComponent<SpriteRenderer> ().sortingOrder = -1;
			poisoned = false;
			stunned = false;

		}

		if (stunned == true) {
			r.color = Color.cyan;	// change color if stunned
		}
		if (poisoned == true) {
			r.color = Color.green;
		}
		if (health <= 25) {				// indicates low hp
			basecolor = Color.red;
		} else if (health <= 75) {
			basecolor = Color.gray;
		}

		if (beatCounter != timekeeper.beatCounter) {		// when a new beat hits apply effects

			if (poisoned == true) {
				if (poisonLimit == (beatCounter - poisonCounter)) {		// check if time limit up for poison
					poisoned = false;
				} else {
					this.applyPoison (2);
				}
			}

			if (stunned == true) {
				if (stunLimit == (beatCounter - stunCounter)) {		// check if time limit up for stun
					stunned = false;
				}
			}

			if (gimped == true) {
				if (gimpLimit == (beatCounter - gimpCounter)) {		// check if time limit up for gimp
					gimped = false;
				}
			}
		
			r.color = basecolor;		// reset color
			beatCounter = timekeeper.beatCounter;
		}
	}

	// status effects
	public void poison(){
		Vector3 tpos = new Vector3 (px, py, 0);			// establish the location of the sprite
		GameObject pi = Instantiate (poisonIcon, tpos, gameObject.transform.rotation);
		pi.transform.localScale = new Vector3 (.4f, .4f, 1f);
		Destroy (pi, 5f * .75f);
		poisoned = true;
		poisonCounter = beatCounter;	// current beat is held
		poisonLimit = 5;				// poison always lasts 5 beats
	}

	public void stun(){
		//Vector3 tpos = new Vector3 (px, py, 0);			// establish the location of the sprite
		//GameObject si = Instantiate (stunIcon, tpos, gameObject.transform.rotation);
		//Destroy (si, 1.0f);
		stunned = true;
		stunCounter = beatCounter;	// current beat is held
		stunLimit = 2;				// stun always lasts 2 beats
	}

	public void gimp(){
		Vector3 tpos = new Vector3 (px, py, 0);			// establish the location of the sprite
		Quaternion trot = gameObject.transform.rotation;	// modify rotation so arrow points down
		trot.x = -180;
		GameObject ai = Instantiate (attackIcon, tpos, trot);
		ai.transform.localScale = new Vector3 (.4f, .4f, 1f);
		Destroy (ai, 2f * .75f);
		gimped = true;
		gimpCounter = beatCounter;
		gimpLimit = 2;				// gimp lasts for a couple beats
	}

	public void heal(int hp){
		Vector3 tpos = new Vector3 (px, py, 0);			// establish the location of the sprite
		GameObject hi = Instantiate (healIcon, tpos, gameObject.transform.rotation);
		Destroy (hi, 1.0f);
		int newHP = health + hp;	// get new health 
		if (newHP <= maxHP) {		// prevent hp overflow
			health = newHP;
		} else {
			health = maxHP;
		}
	}

	public void takeHit(int dmg){
		r.color = Color.red;
		animator.SetTrigger ("hit"); // play hit animation
		health = health - dmg;		// assign damage
		if (health <= 0){			// u good, bro?
			health = 0;
			alive = false;
		}
	}

	private void applyPoison(int dmg){		// deals unblockable damage
		health = health - dmg;		// assign damage
		if (health <= 0){			// u good, bro?
			health = 0;
			alive = false;
		}
	}

//	public void telegraph(){		// for demo only
//		tempColor = basecolor;
//		basecolor = Color.yellow;
//	}
//	public void endTelegraph(){
//		basecolor = tempColor;
//	}
}
