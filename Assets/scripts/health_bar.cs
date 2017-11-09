using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script manages the health bar display for all of the player characters

public class health_bar : MonoBehaviour {

	public GameObject player;
	private player_object pscript;
	private float maxHp;
	private float currentHp;
	private float percentage;		// percent of total hp
	private Vector3 scale;
	private float maxX;				// total scale of health bar

	// adjust hp bar when shrinking
	public float centerToLeft;		// enter manually
	private float newCenter;

	// Use this for initialization
	void Start () {
		pscript = player.GetComponent<player_object> ();
		maxHp = (float)pscript.health;						// casting int values as floats for division purposes
		currentHp = (float)maxHp;
		percentage = currentHp / maxHp;
		scale = this.transform.localScale;
		maxX = scale.x;
		newCenter = -centerToLeft;
	}
	
	// Update is called once per frame
	void Update () {
		currentHp = (float)pscript.health;		// update hp tracker
		percentage = currentHp / maxHp;			// update percent

		scale.x = maxX * percentage;			// modify size in proportion to hp

		newCenter = (-centerToLeft) * percentage;		// modify distance to center

		this.transform.localScale = scale;
		this.transform.localPosition = new Vector3 (newCenter+centerToLeft, 0f, 0f);
	}
}
