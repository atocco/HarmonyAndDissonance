using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class confirmIcon : MonoBehaviour {

	public Sprite[] sprites;
	private SpriteRenderer currsprite;
	private int spriteIndex;

	// Use this for initialization
	void Start () {
		currsprite = GetComponent<SpriteRenderer>(); 	// get the current sprite
		spriteIndex = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		spriteIndex++;
		spriteIndex = spriteIndex % 2;
		currsprite.sprite = sprites [spriteIndex];	// swapping the sprite rendered for the ability bar
	}
}
