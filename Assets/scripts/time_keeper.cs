using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script is intended to take in song data and dictate the window of time a player has to input commands

public class time_keeper : MonoBehaviour {

	private static Song s;
	private static float note_length;
	private float time_elapsed; 			// this is used with delta time to keep track of time since last note
	public float beatsPerMin;
	private SpriteRenderer currsprite;
	public Sprite[] sprites;				// sprite array of 4 objects for the rhythm bar
	private int spriteIndex;
	private GameObject music;				// get the music player in this scene
	private AudioSource audsrc;				// establish an audio source var
	public bool validInput;					// indicates whether the game can recieve input

	// Use this for initialization
	void Start () {
		s = new Song (beatsPerMin);
		note_length = s.Note_Length;					// calculate the length of notes in the song
		currsprite = GetComponent<SpriteRenderer>(); 	// get the current sprite
		spriteIndex = 0;
		music = GameObject.FindGameObjectWithTag ("music");		// load the music player
		audsrc = music.GetComponent<AudioSource> ();
		validInput = false;
	}
	
	// Update is called once per frame
	void Update () {
		time_elapsed = Time.deltaTime + time_elapsed;		// keeping track of time
		if (time_elapsed >= note_length){					// if about a note length has passed...
			BeatHit ();										// trigger beat hit and,
			time_elapsed = time_elapsed % note_length;		// reset time elapsed
		}
		// Invoke("BeatHit", note_length);	// find suitable replacement for this
	}

	// this function triggers actions when the beat hits
	void BeatHit(){
		if (audsrc.isPlaying) {							// beat related actions only trigger when the music starts
			validInput = true;
			Invoke ("invalidate", Time.deltaTime * 10);
			spriteIndex++;
			spriteIndex = spriteIndex % 4;
			currsprite.sprite = sprites [spriteIndex];	// swapping the sprite rendered for the rhythm meter
		}
	}

	void invalidate(){
		validInput = false;
	}

	// A class that keeps track of the song information
	public class Song
	{
		private float bpm;

		public Song(){			// constructor
			bpm = 1.0f;
		}
		public Song(float b){
			bpm = b;
		}

		// PROPERTIES FOR THE SONGS
		public float BPM{
			get
			{
				return bpm;
			}
			set
			{
				bpm = value;
			}
		}
		public float Note_Length{
			get
			{ 
				return (60.0f / bpm);
			}
		}
	}
}
