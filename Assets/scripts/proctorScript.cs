using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this guy ends the game if you lost or killed the boss

public class proctorScript : MonoBehaviour {

	public GameObject panda;
	public GameObject rabbit;
	public GameObject bee;
	public GameObject bird;
	public GameObject boss;

	// tracking shit
	private bool alivepanda;
	private bool aliverabbit;
	private bool alivebee;
	private bool alivebird;

	private bool gameOver;
	private bool win;
	private int deadPlayers;

	// Use this for initialization
	void Start () {
		gameOver = false;
		win = false;
		deadPlayers = 0;
		alivepanda = true;
		alivebee = true;
		alivebird = true;
		aliverabbit = true;
	}
	
	// Update is called once per frame
	void Update () {
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

		if (gameOver == true) {
			Invoke ("endFight", 2.0f);
		}
	}

	private void endFight(){
		if (win == true) {
			// you win!!!
			this.GetComponent<TextMesh> ().text = "You Won!";
			this.GetComponent<MeshRenderer> ().enabled = true;
			Time.timeScale = 0.0f; // stop game
		} else {
			// you lose!!!
			this.GetComponent<TextMesh> ().text = "You Lost :(";
			this.GetComponent<MeshRenderer> ().enabled = true;
			Time.timeScale = 0.0f; // stop game
		}
	}

}
