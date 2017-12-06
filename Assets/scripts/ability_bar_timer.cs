using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ability_bar_timer : MonoBehaviour {

	// sprite controls
	public Sprite[] sprites;
	private SpriteRenderer currsprite;
	private int spriteIndex;
	//private Color basecolor;

	// Use this for initialization
	void Start () {
		// change sprite to show beat
		currsprite = GetComponent<SpriteRenderer>(); 	// get the current sprite
		spriteIndex = 0;
		//basecolor = currsprite.color;
	}
	
	// called by the timekeeper to change sprite color
	public void switchSprite(){
		spriteIndex++;
		spriteIndex = spriteIndex % 2;
		currsprite.sprite = sprites [spriteIndex];	// swapping the sprite rendered for the ability bar
	}

	// plays sound when you screw up
	public void badInput(){
		this.GetComponent<AudioSource> ().Play ();
	}

}
