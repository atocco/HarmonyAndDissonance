using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script manages the ability bar
// it displays the called icon in the appropriate spot
// and then telegraphs the completed combo to an attack script

public class ability_bar : MonoBehaviour {

	// establish values for the offset for the ability bar
	// static private float baseX;
	static private float baseY;
	// x coords of the icons
	private float x1;
	private float x2;
	private float x3;

	public GameObject beeIcon;
	public GameObject rabbitIcon;
	public GameObject pandaIcon;
	public GameObject birdIcon;
	public GameObject confirmIcon;

	// characters to check if they died
	public GameObject panda;
	public GameObject bird;
	public GameObject bee;
	public GameObject rabbit;

	private string[] abilityStack;
	private GameObject[] iconStack;
	private int cursor;				// keep track of our position in the ability array
	//private time_keeper timescript;
	private ability_controller abilityControls;
	public GameObject fc;

	public int oneInput;
	private bool inputValid;
	// sprite controls
	//public Sprite[] sprites;
	//private SpriteRenderer currsprite;
	//private int spriteIndex;

	// Use this for initialization
	void Start () {
		// TODO change these values to reflect the absolute position of the ability chart
		x1 = -2.0f;
		x2 = 0f;
		x3 = 2.0f;

		//baseX = gameObject.transform.position.x;	//get the initial values for the ability bar location
		baseY = gameObject.transform.position.y;

		cursor = 0;
		abilityStack = new string[3];	// only three inputs allowed on the stack at a time
		iconStack = new GameObject[3];	// basically same as above but holds the icon objects

		// find the time keeper and attached script
		//GameObject tk = GameObject.FindGameObjectWithTag ("timekeeper");
		//timescript = tk.GetComponent<time_keeper> ();

		abilityControls = this.GetComponent<ability_controller>();		// import the ability script

		oneInput = 0;

		// change sprite to show beat
		//currsprite = GetComponent<SpriteRenderer>(); 	// get the current sprite
		//spriteIndex = 0;
	}

	// called by the timekeeper to change sprite color
	//public void switchSprite(){
		//spriteIndex++;
		//spriteIndex = spriteIndex % 2;
		//currsprite.sprite = sprites [spriteIndex];	// swapping the sprite rendered for the ability bar
	//}

	// this script takes in an character name / spacebar and pushes it onto the ability bar stack
	// if an input is given at the end of a combo that isn't a spacebar, the ability stack drains
	// all inputs on stack must be unique
	public void pushAbility(string input) {

		oneInput++;		// flag to make sure no duplicates
		inputValid = true;		// flag for input

		int check = System.Array.IndexOf (abilityStack, input);		// sanity check for input

		if (check >= 0){
			popAbilities ();
			inputValid = false;
		} 
		if (input == panda.name) {
			// check for dead char input
			if (panda.GetComponent<player_object> ().alive == false) {
				popAbilities ();
				inputValid = false;
			}
		} else if (input == rabbit.name) {
			if (rabbit.GetComponent<player_object> ().alive == false) {
				popAbilities ();
				inputValid = false;
			}
			;
		} else if (input == bee.name) {
			// check for dead char input
			if (bee.GetComponent<player_object> ().alive == false) {
				popAbilities ();
				inputValid = false;
			}
		} else if (input == bird.name) {
			// check for dead char input
			if (bird.GetComponent<player_object> ().alive == false) {
				popAbilities ();
				inputValid = false;
			}
		} else if (oneInput != 1) {
			popAbilities ();
			inputValid = false;
		}

		if (inputValid == true){ 
			switch (input) {
			case "bee":
				if (cursor < 3) {
					abilityStack [cursor] = "bee";
					showIcon (beeIcon);
					cursor++;
					fc.GetComponent<flashy_ability_controller> ().beeCall ();
				} else {
					popAbilities ();
				}
				break;
			case "panda":
				if (cursor < 3) {
					abilityStack [cursor] = "panda";
					showIcon (pandaIcon);
					cursor++;
					fc.GetComponent<flashy_ability_controller> ().pandaCall ();
				} else {
					popAbilities ();
				}
				break;
			case "bird":
				if (cursor < 3) {
					abilityStack [cursor] = "bird";
					showIcon (birdIcon);
					cursor++;
					fc.GetComponent<flashy_ability_controller> ().birdCall ();
				} else {
					popAbilities ();
				}
				break;
			case "rabbit":
				if (cursor < 3) {
					abilityStack [cursor] = "rabbit";
					showIcon (rabbitIcon);
					cursor++;
					fc.GetComponent<flashy_ability_controller> ().rabbitCall ();
				} else {
					popAbilities ();
				}
				break;
			default:
				Debug.Log ("something went terribly wrong in the switch statement");
				popAbilities ();
				break;
			}
		}
			
	}

	// clears the ability stack and its icons
	public void popAbilities() {
		//Debug.Log ("drained ability queue");
		this.resetAbFlag ();
		foreach(GameObject icon in iconStack){	// empty the icon stack if populated
			if (icon != null) {
				Destroy (icon);
			}
		}
		if (cursor > 0) {
			this.GetComponent<ability_bar_timer> ().badInput ();	// show red bar
		}
		iconStack = new GameObject[3];		// reset the stack
		abilityStack = new string[3];		// empty the stack
		cursor = 0;							// reset the cursor
	}

	// takes the stack and passes it to the ability script + shows confirm icon
	private void castAbilitty(string[] stack){
		Vector3 pos = this.transform.position;
		GameObject clone = Instantiate (confirmIcon, pos, gameObject.transform.rotation);	// create the confirm sprite
		Destroy (clone, .5f);		// remove after 2/4 second
		this.resetAbFlag ();
		foreach(GameObject icon in iconStack){	// empty the icon stack if populated
			if (icon != null) {
				Destroy (icon);
			}
		}
		abilityControls.parseAbility (stack);		// pass stack to the controller
		iconStack = new GameObject[3];		// reset the stack
		abilityStack = new string[3];		// empty the stack
		cursor = 0;							// reset the cursor
	}

	// The following collection of methods display the ability icons in the appropriate place in the bar
	private void showIcon(GameObject icon) {
		if (cursor == 0) {
			Vector3 pos = new Vector3 (x1, baseY, 0);		// establish the location of the sprite
			Quaternion rot = new Quaternion (0f,0f,0f,0f);
			GameObject clone = Instantiate (icon, pos, rot);	// create the sprite
			iconStack[cursor] = clone;
		} else if (cursor == 1) {
			Vector3 pos = new Vector3 (x2, baseY, 0);
			Quaternion rot = new Quaternion (0f,0f,0f,0f);
			GameObject clone = Instantiate (icon, pos, rot);
			iconStack [cursor] = clone;
		} else {
			Vector3 pos = new Vector3 (x3, baseY, 0);
			Quaternion rot = new Quaternion (0f,0f,0f,0f);
			GameObject clone = Instantiate (icon, pos, rot);
			iconStack [cursor] = clone;
		}
	}

	public void checkForConfirm(float delay){
		// this is called by the time_keeper every beat hit to check for three inputs to autoconfirm
		if (cursor == 3) {
			Invoke ("blastEm", delay);
		}
	}

	private void blastEm(){		// the invoked sister method to the check for confirm
		castAbilitty (abilityStack);
	}

	// called so that one ability can be pushed per beat
	public void resetAbFlag(){
		oneInput = 0;
	}
}
