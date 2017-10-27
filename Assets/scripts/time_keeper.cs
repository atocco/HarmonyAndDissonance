using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script is intended to take in song data and dictate the window of time a player has to input commands
// this is to be attached to the rhythm bar

public class time_keeper : MonoBehaviour {

	private static Song s;
	private static float note_length;
	public float input_window; 				// this is used with delta time to establish the time a player has before and after beat input to act
	private static float frame_time;		// this is the time it takes for a note to pass
	private float time_passed;				// time since last beat
	public float beatsPerMin;
	private SpriteRenderer currsprite;
	public Sprite[] sprites;				// sprite array of 4 objects for the rhythm bar
	private int spriteIndex;
	private GameObject music;				// get the music player in this scene
	private AudioSource audsrc;				// establish an audio source var
	public bool validInput;					// indicates whether the game can recieve input
	public int beatCounter;					// global indicator of how many beats have passed

	// Use this for initialization
	void Start () {
		beatCounter = 0;
		s = new Song (beatsPerMin);
		note_length = s.Note_Length;					// calculate the length of notes in the song in seconds
		currsprite = GetComponent<SpriteRenderer>(); 	// get the current sprite
		spriteIndex = 0;
		music = GameObject.FindGameObjectWithTag ("music");		// load the music player
		audsrc = music.GetComponent<AudioSource> ();
		validInput = false;
		frame_time = Time.fixedDeltaTime;				// time between frames is fixed
		// input_window = 0.1f;					// half the time players have for input
	}
	
	// FixedUpdate is called 30 times a second and before Update
	void FixedUpdate () {
		time_passed = (time_passed + frame_time);
		if ((time_passed >= note_length) && audsrc.isPlaying){		// if about a note length has passed / music is on...
			BeatHit ();											// trigger beat hit and,
			time_passed = time_passed % note_length;		// reset time passed since last beat
		}
	}

	// this function triggers actions when the beat hits
	private void BeatHit(){							// beat related actions only trigger when the music starts
		beatCounter++;
		spriteIndex++;
		spriteIndex = spriteIndex % 4;
		currsprite.sprite = sprites [spriteIndex];	// swapping the sprite rendered for the rhythm meter
		Invoke ("invalidate", input_window);		// after the input window time passes, player can't input
	}

	private void invalidate(){
		validInput = false;
		float negativeSpace = note_length - (2.0f * input_window);	// the space of time the player can't act
		Invoke ("validate", negativeSpace);							// after that time the player can act again
	}
	private void validate(){
		validInput = true;
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
