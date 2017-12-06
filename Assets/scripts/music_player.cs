using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The purpose of this script is to control the playback of the audio during boss segments

public class music_player : MonoBehaviour {

	public float startDelay; // Number of seconds to delay the start of audio playback.
	private AudioSource audsrc;

	// Use this for initialization
	void Start () {
		audsrc = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!audsrc.isPlaying && Time.time >= startDelay) {
			audsrc.Play();
		}
	}
}
