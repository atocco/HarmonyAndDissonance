using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script keeps track of which character is on which space in a boss battle

public class position_keeper : MonoBehaviour {

	private GameObject[] players;
	private GameObject[] spaces;

	// Use this for initialization
	void Start () {
		players = GameObject.FindGameObjectsWithTag ("player");
		spaces = GameObject.FindGameObjectsWithTag ("space");
		int index = 0;
		float x = 0f;
		float y = 0f;
		foreach (GameObject p in players) {						// initialize all player characters on their spaces
			x = spaces [index].transform.position.x + 1.25f;	// TODO: edit the added values to center the sprites
			y = spaces [index].transform.position.y + 1.25f;
			p.transform.position = new Vector2(x, y);
			index++;
			Debug.Log (p.name);
		}
	}

	// this method is called to swap the positions of two characters
	public void swapPosition(string p1, string p2){		// takes in the two character names
		
	}

	// this method is called to rotate the players clockwise or counterclockwise
	public void fullRotate(){
	
	}
}
