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
	//private SpriteRenderer currsprite;
	//public Sprite[] sprites;				// sprite array of 4 objects for the rhythm bar
	//private int spriteIndex;
	private GameObject music;				// get the music player in this scene
	private AudioSource audsrc;				// establish an audio source var
	public bool validInput;					// indicates whether the game can recieve input
	public int beatCounter;					// global indicator of how many beats have passed

	// ability queue for autoconfirms
	private ability_bar abscript;
	// ability sprite for beat keeping
	private ability_bar_timer abtimer;

	// Use this for initialization
	void Start () {
		beatCounter = -1;
		s = new Song (beatsPerMin);
		note_length = s.Note_Length;					// calculate the length of notes in the song in seconds
		//currsprite = GetComponent<SpriteRenderer>(); 	// get the current sprite
		//spriteIndex = 0;
		music = GameObject.FindGameObjectWithTag ("music");		// load the music player
		audsrc = music.GetComponent<AudioSource> ();
		validInput = false;
		frame_time = Time.fixedDeltaTime;				// time between frames is fixed
		time_passed = 0.0f;

		// get ability info
		GameObject ag = GameObject.FindGameObjectWithTag ("abilitybar");
		abscript = ag.GetComponent<ability_bar> ();
		abtimer = ag.GetComponent<ability_bar_timer> ();
		//Debug.Log ("time between notes:");
		//Debug.Log (note_length);
		//Debug.Log ("tiem between frames:");
		//Debug.Log (frame_time);
	}
	
	// FixedUpdate is called 30 times a second and before Update
	void FixedUpdate () {
		frame_time = Time.fixedDeltaTime;
		time_passed = (time_passed + frame_time);
		if ((time_passed >= note_length) && Time.time >= 3f){		// if about a note length has passed / music is on...
			//Debug.Log ("Time passed on beat hit:");
			//Debug.Log (time_passed);
			BeatHit ();											// trigger beat hit and,
			time_passed = time_passed % note_length;		// reset time passed since last beat
			//Debug.Log ("Time passed AFTER beat hit:");
			//Debug.Log (time_passed);
		}
	}

	// this function triggers actions when the beat hits
	private void BeatHit(){							// beat related actions only trigger when the music starts
		beatCounter++;
		abscript.resetAbFlag ();		// reset counter to stop multiple inputs per beat
		audsrc.Play ();
		//spriteIndex++;
		//spriteIndex = spriteIndex % 12;
		//currsprite.sprite = sprites [spriteIndex];	// swapping the sprite rendered for the rhythm meter
		Invoke ("invalidate", input_window - 0.01f);		// after the input window time passes, player can't input
	}

	private void invalidate(){
		abtimer.switchSprite ();
		validInput = false;
		float negativeSpace = note_length - (2.0f * input_window);	// the space of time the player can't act
		Invoke ("validate", negativeSpace - 0.05f);							// after that time the player can act again
	}
	private void validate(){
		validInput = true;
		abtimer.switchSprite ();
		abscript.checkForConfirm (input_window);				// blast that mf confirm button
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
