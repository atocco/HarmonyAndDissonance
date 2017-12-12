using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu_key_controls : MonoBehaviour {

	public AudioClip msover;
	public AudioClip start;
	public AudioClip exit;
	private AudioSource aud;

	// Use this for initialization
	void Start () {
		aud = this.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Return)) {
			if(this.transform.position.y == 2f)
			{
				aud.clip = start;
				aud.Play ();

				SceneManager.LoadScene ("boss_battle_demo", LoadSceneMode.Single);
			}
			if (this.transform.position.y == -1f)
			{
				aud.clip = exit;
				aud.Play ();
				Application.Quit();
			}
		}

		if (Input.GetKeyUp (KeyCode.UpArrow)) {
			if(this.transform.position.y == 2f)
			{
				aud.clip = msover;
				aud.Play ();
				this.transform.position = new Vector2 (-4f, -1f);
			}
			if (this.transform.position.y == -1f)
			{
				aud.clip = msover;
				aud.Play ();
				this.transform.position = new Vector2 (-4f, 2f);
			}
		}

		if (Input.GetKeyUp (KeyCode.DownArrow)) {
			if(this.transform.position.y == 2f)
			{
				aud.clip = msover;
				aud.Play ();
				this.transform.position = new Vector2 (-4f, -1f);
			}
			if (this.transform.position.y == -1f)
			{
				aud.clip = msover;
				aud.Play ();
				this.transform.position = new Vector2 (-4f, 2f);
			}
		}
	}
}
