using UnityEngine;
using System.Collections;

public class MenuAudio : MonoBehaviour {

	public AudioSource songMain;
	public AudioSource songLoop;

	//Plays main song, then plays loop section after the main song has finished playing
	void Start () {

		Destroy(GameObject.Find("GameMusic"));
		DontDestroyOnLoad (this);
		songMain.Play ();
		songLoop.PlayDelayed (songMain.clip.length);
	}
}
