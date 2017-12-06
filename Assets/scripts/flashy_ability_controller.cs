using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script manages all of the theatrics that go alongside the abilities 
// this includes the sounds effects and the icons

public class flashy_ability_controller : MonoBehaviour {

	public AudioClip pandaSound;
	public AudioClip rabbitSound;
	public AudioClip birdSound;
	public AudioClip beeSound;

	public AudioClip pandaAttack;
	public AudioClip rabbitAttack;
	public AudioClip beeAttack;
	public AudioClip birdAttack;

	public GameObject pandaAttIcon;
	public GameObject beeAttIcon;
	public GameObject birdAttIcon;
	public GameObject rabbitAttIcon;

	public GameObject panda;
	public GameObject bee;
	public GameObject bird;
	public GameObject rabbit;
	public GameObject boss;

	private AudioSource aud;

	// Use this for initialization
	void Start () {
		aud = this.GetComponent<AudioSource> ();
	}
	
	public void pandaCall(){
		aud.clip = pandaSound;
		aud.Play ();
		panda.GetComponent<player_fight_animations> ().abilityCast ();
	}
	public void rabbitCall(){
		aud.clip = rabbitSound;
		aud.Play ();
		rabbit.GetComponent<player_fight_animations> ().abilityCast ();
	}
	public void birdCall(){
		aud.clip = birdSound;
		aud.Play ();
		bird.GetComponent<player_fight_animations> ().abilityCast ();
	}
	public void beeCall(){
		aud.clip = beeSound;
		aud.Play ();
		bee.GetComponent<player_fight_animations> ().abilityCast ();
	}

	public void pandaCast(){
		Vector3 pos = boss.transform.position;
		GameObject clone = Instantiate (pandaAttIcon, pos, gameObject.transform.rotation);	// create the pounce sprite
		Destroy (clone, .5f);		// remove after 1/2 second
		aud.clip = pandaAttack;
		aud.Play ();
	}
	public void birdCast(){
		Vector3 pos = boss.transform.position;
		GameObject clone = Instantiate (birdAttIcon, pos, gameObject.transform.rotation);	// create the defend sprite
		Destroy (clone, .5f);		// remove after 1/2 second
		aud.clip = birdAttack;
		aud.Play ();
	}
	public void beeCast(){
		Vector3 pos = boss.transform.position;
		GameObject clone = Instantiate (beeAttIcon, pos, gameObject.transform.rotation);	// create the recover sprite
		Destroy (clone, .5f);		// remove after 1/2 second
		aud.clip = beeAttack;
		aud.Play ();
	}
	public void rabbitCast(){
		Vector3 pos = boss.transform.position;
		GameObject clone = Instantiate (rabbitAttIcon, pos, gameObject.transform.rotation);	// create the rabbit sprite
		Destroy (clone, .5f);		// remove after 1/2 second
		aud.clip = rabbitAttack;
		aud.Play ();
	}
}
