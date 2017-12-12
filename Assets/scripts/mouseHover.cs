using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mouseHover : MonoBehaviour {

	//private MeshRenderer renderer;
	public bool isStart;
	public bool isExit;
	public AudioClip msover;
	public AudioClip start;
	public AudioClip exit;
	private AudioSource aud;
	public GameObject cursor;

	void Start(){
		//renderer = this.GetComponent<MeshRenderer> ();
		aud = this.GetComponent<AudioSource> ();
	}

	void Update(){
		
	}

	void OnMouseEnter(){
		//aud.clip = msover;
		//aud.Play ();
		if(isStart)
		{
			aud.clip = msover;
			aud.Play ();
			cursor.transform.position = new Vector2 (-4f, 2f);
		}
		if (isExit)
		{
			aud.clip = msover;
			aud.Play ();
			cursor.transform.position = new Vector2 (-4f, -1f);
		}
	}

	void OnMouseExit() {
	}

	void OnMouseUp(){
		if(isStart)
		{
			aud.clip = start;
			aud.Play ();
			//Application.LoadLevel ("boss_battle_demo");
			SceneManager.LoadScene ("boss_battle_demo", LoadSceneMode.Single);
		}
		if (isExit)
		{
			aud.clip = exit;
			aud.Play ();
			Application.Quit();
		}
	}
}
