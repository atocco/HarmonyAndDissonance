using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script keeps track of which character is on which space in a boss battle

public class position_keeper : MonoBehaviour {

	private GameObject[] players;
	private GameObject[] spaces;

	// THE ORDER OF THIS LIST IS ** UP DOWN LEFT RIGHT **
	public string[] playerOnSpace;

	// Use this for initialization
	void Start () {
		playerOnSpace = new string[4];
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
		}

		// the following code lets other scripts know who is where
		// the player and space lists at this point should be in an order where
		// player[0] is on space[0], etc...
		index = 0;
		foreach (GameObject s in spaces) {
			player_object pscript = players [index].GetComponent<player_object> ();	// gets the related player variables
			switch (s.name) {
			case "player space up":
				playerOnSpace [0] = players [index].name;
				pscript.position = "up";
				break;
			case "player space down":
				playerOnSpace [1] = players [index].name;
				pscript.position = "down";
				break;
			case "player space left":
				playerOnSpace [2] = players [index].name;
				pscript.position = "left";
				break;
			case "player space right":
				playerOnSpace [3] = players [index].name;
				pscript.position = "right";
				break;
			default:
				Debug.Log ("something went wrong when assigning players to spaces");
				break;
			}
			index ++;
		}
	}

	// this method is called to swap the positions of two characters
	public void swapPosition(string p1, string p2){		// takes in the two character names
		
	}

	// this method is called to rotate the players clockwise or counterclockwise
	public void fullRotate(){
	
	}
		
}
