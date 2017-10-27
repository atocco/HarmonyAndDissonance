using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script manages the ability bar
// it displays the called icon in the appropriate spot
// and then telegraphs the completed combo to an attack script

public class ability_bar : MonoBehaviour {

	// establish values for the offset for the ability bar
	static private float baseX;
	static private float baseY;
	private float x1;
	private float x2;
	private float x3;
	private float x4;
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
	private time_keeper timescript;
	private ability_controller abilityControls;

	// Use this for initialization
	void Start () {
		// TODO change these values to reflect the final ability bar size
		x1 = -2.85f;		// establish the offset values of each point in the ability bar
		x2 = -0.93f;
		x3 = 0.93f;
		x4 = 2.9f;
		baseX = gameObject.transform.position.x;	//get the initial values for the ability bar location
		baseY = gameObject.transform.position.y + 0.01f;
		cursor = 0;
		abilityStack = new string[3];	// only three inputs allowed on the stack at a time
		iconStack = new GameObject[3];	// basically same as above but holds the icon objects

		// find the time keeper and attached script
		GameObject tk = GameObject.FindGameObjectWithTag ("timekeeper");
		timescript = tk.GetComponent<time_keeper> ();

		abilityControls = this.GetComponent<ability_controller>();		// import the ability script
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space) && timescript.validInput) {
			pushAbility ("space");
		} else if (Input.GetKeyDown (KeyCode.Space) && !timescript.validInput) {
			popAbilities ();
		}
	}

	// this script takes in an character name / spacebar and pushes it onto the ability bar stack
	// if an input is given at the end of a combo that isn't a spacebar, the ability stack drains
	// all inputs on stack must be unique
	public void pushAbility(string input) {

		bool inputValid = true;		// flag for input

		int check = System.Array.IndexOf (abilityStack, input);		// sanity check for input

		if (check >= 0){
			popAbilities ();
			inputValid = false;
		} 
		if (input == panda.name){
			// check for dead char input
			if (panda.GetComponent<player_object> ().alive == false) {
				popAbilities ();
				inputValid = false;
			}
		} else if (input == rabbit.name){
			if (rabbit.GetComponent<player_object> ().alive == false) {
				popAbilities ();
				inputValid = false;
			};
		}else if (input == bee.name){
			// check for dead char input
			if (bee.GetComponent<player_object> ().alive == false) {
				popAbilities ();
				inputValid = false;
			}
		}else if (input == bird.name){
			// check for dead char input
			if (bird.GetComponent<player_object> ().alive == false) {
				popAbilities ();
				inputValid = false;
			}
		}

		if (inputValid == true){ 
			switch (input) {
			case "bee":
				if (cursor < 3) {
					abilityStack [cursor] = "bee";
					showIcon (beeIcon);
					cursor++;
				} else {
					popAbilities ();
				}
				break;
			case "panda":
				if (cursor < 3) {
					abilityStack [cursor] = "panda";
					showIcon (pandaIcon);
					cursor++;
				} else {
					popAbilities ();
				}
				break;
			case "bird":
				if (cursor < 3) {
					abilityStack [cursor] = "bird";
					showIcon (birdIcon);
					cursor++;
				} else {
					popAbilities ();
				}
				break;
			case "rabbit":
				if (cursor < 3) {
					abilityStack [cursor] = "rabbit";
					showIcon (rabbitIcon);
					cursor++;
				} else {
					popAbilities ();
				}
				break;
			case "space":
				Debug.Log (cursor);
				if (cursor == 3) {
					Debug.Log ("ability cast!");
					castAbilitty (abilityStack);
					popAbilities ();
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
		Debug.Log ("drained ability queue");
		foreach(GameObject icon in iconStack){	// empty the icon stack if populated
			if (icon != null) {
				Destroy (icon);
			}
		}
		iconStack = new GameObject[3];		// reset the stack
		abilityStack = new string[3];		// empty the stack
		cursor = 0;							// reset the cursor
	}

	// takes the stack and passes it to the ability script + shows confirm icon
	private void castAbilitty(string[] stack){
		float finalX = (baseX + x4);
		Vector3 pos = new Vector3 (finalX, baseY, 0);			// establish the location of the sprite
		GameObject clone = Instantiate (confirmIcon, pos, gameObject.transform.rotation);	// create the confirm sprite
		Destroy (clone, clone.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);		// remove after animation

		abilityControls.parseAbility (stack);		// pass stack to the controller
	}

	// The following collection of methods display the ability icons in the appropriate place in the bar
	private void showIcon(GameObject icon) {
		if (cursor == 0) {
			float finalX = (baseX + x1);							// calculate the offset x value
			Vector3 pos = new Vector3 (finalX, baseY, 0);			// establish the location of the sprite
			GameObject clone = Instantiate (icon, pos, gameObject.transform.rotation);	// create the sprite
			iconStack[cursor] = clone;
		} else if (cursor == 1) {
			float finalX = (baseX + x2);
			Vector3 pos = new Vector3 (finalX, baseY, 0);
			GameObject clone = Instantiate (icon, pos, gameObject.transform.rotation);
			iconStack [cursor] = clone;
		} else {
			float finalX = (baseX + x3);
			Vector3 pos = new Vector3 (finalX, baseY, 0);
			GameObject clone = Instantiate (icon, pos, gameObject.transform.rotation);
			iconStack [cursor] = clone;
		}
	}

}
