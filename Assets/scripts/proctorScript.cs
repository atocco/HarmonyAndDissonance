using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// this guy ends the game if you lost or killed the boss

public class proctorScript : MonoBehaviour {

	private bool pls;

	public GameObject panda;
	public GameObject rabbit;
	public GameObject bee;
	public GameObject bird;
	public GameObject boss;
	public GameObject scriptmonkey;
	public GameObject fancyContro;
	public GameObject music;

	// tracking shit
	private bool alivepanda;
	private bool aliverabbit;
	private bool alivebee;
	private bool alivebird;

	private bool gameOver;
	private bool win;
	private int deadPlayers;

	// countdown icons and stuff
	public GameObject upicon;
	public GameObject downicon;
	public GameObject lefticon;
	public GameObject righticon;
	public GameObject countdown;

	// win / lost text and sound
	public GameObject victory;
	public GameObject defeat;

	// Use this for initialization
	void Start () {
		gameOver = false;
		win = false;
		deadPlayers = 0;
		alivepanda = true;
		alivebee = true;
		alivebird = true;
		aliverabbit = true;

		// pre battle stuff
		countdown.GetComponent<Animator> ().speed = 1f/12f;
		Destroy (countdown, countdown.GetComponent<Animator> ().GetCurrentAnimatorClipInfo (0).Length*3.75f);

		pls = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (!countdown) {
			Destroy (upicon, 1f);
			Destroy (downicon, 1f);
			Destroy (lefticon, 1f);
			Destroy (righticon, 1f);
		}

		if (panda.GetComponent<player_object> ().alive != alivepanda){
			deadPlayers++;
			alivepanda = false;
		}
		if (bird.GetComponent<player_object> ().alive != alivebird){
			deadPlayers++;
			alivebird = false;
		}
		if (bee.GetComponent<player_object> ().alive != alivebee){
			deadPlayers++;
			alivebee = false;
		}
		if (rabbit.GetComponent<player_object> ().alive != aliverabbit){
			deadPlayers++;
			aliverabbit = false;
		}
		if (boss.GetComponent<boss_object> ().alive == false) {
			gameOver = true;
			win = true;
		}


		if (deadPlayers >= 2) {
			gameOver = true;
			win = false;
		}

		if (gameOver == true && pls) {
			pls = false;
			Destroy (scriptmonkey.GetComponent<ability_bar> ());
			scriptmonkey.GetComponent<ability_controller> ().enabled = false;
			fancyContro.GetComponent<flashy_ability_controller> ().enabled = false;
			music.GetComponent<AudioSource> ().enabled = false;
			boss.GetComponent<crop_duster> ().enabled = false;
			Invoke ("endFight", 3.0f);
		}
	}

	private void endFight(){
		if (win == true) {
			// you win!!!
			GameObject clone = Instantiate (victory);
			Invoke ("stopGame", 5f);
		} else {
			// you lose!!!
			GameObject clone = Instantiate (defeat);
			Invoke ("stopGame", 5f);
		}
	}

	private void stopGame(){
		SceneManager.LoadScene ("main_menu", LoadSceneMode.Single);
	}

}
