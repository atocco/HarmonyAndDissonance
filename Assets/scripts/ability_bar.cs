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
	public GameObject beeIcon;
	public GameObject rabbitIcon;
	public GameObject pandaIcon;
	public GameObject birdIcon;
	private string[] abilityStack;
	private GameObject[] iconStack;
	private int cursor;				// keep track of our position in the ability array

	// Use this for initialization
	void Start () {
		// TODO change these values to reflect the final ability bar size
		x1 = -2.5f;		// establish the offset values of each point in the ability bar
		x2 = -0.85f;
		x3 = 0.9f;
		baseX = gameObject.transform.position.x;	//get the initial values for the ability bar location
		baseY = gameObject.transform.position.y;
		cursor = 0;
		abilityStack = new string[3];	// only three inputs allowed on the stack at a time
		iconStack = new GameObject[3];	// basically same as above but holds the icon objects
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// this script takes in an character name / spacebar and pushes it onto the ability bar stack
	// if an input is given at the end of a combo that isn't a spacebar, the ability stack drains
	public void pushAbility(string input) {
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
			Debug.Log ("ability cast!");
			popAbilities ();
			// TODO add ability cast method
			break;
		default:
			Debug.Log ("something went terribly wrong in the switch statement");
			popAbilities ();
			break;
		}
	}

	public void popAbilities() {
		Debug.Log ("drained ability queue");
		// TODO write code that removes the icons
		foreach(GameObject icon in iconStack){	// empty the icon stack if populated
			if (icon != null) {
				Destroy (icon);
			}
		}
		iconStack = new GameObject[3];		// reset the stack
		abilityStack = new string[3];		// empty the stack
		cursor = 0;							// reset the cursor
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
