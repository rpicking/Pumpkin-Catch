using UnityEngine;
using System.Collections;

public class GameAudio : MonoBehaviour {

	public AudioSource baselineMusic;
	public AudioSource dangerMusic;
	public AudioSource Whoosh;
	public AudioSource KeyCollect;
	public AudioSource GunCock;
	public AudioSource Gunshot;
	public AudioSource DoorOpen;
	public AudioSource PumpkinFootsteps;
	public AudioSource ScarecrowFootsteps;
	public AudioSource PumpkinDeath;
	public AudioSource ScarecrowDeath;
	public AudioSource VO_LetHuntBegin;
	public AudioSource VO_GameOver;
	public AudioSource VO_HunterToHunted;
	public AudioSource VO_HunterVictory;
	public AudioSource VO_PreyVictory;

	public int farDistance;
	public int closeDistance;
	private GameObject Hunter;
	private GameObject[] preyList;
	private float distance;

	void Start() {

		DontDestroyOnLoad (this);
		// Define Hunter and PreyList
		Hunter = GameObject.FindWithTag("Hunter");
		preyList = GameObject.FindGameObjectsWithTag("Prey");

		// Stop menu music and start game music
		GameObject.Find("MenuAudio").SetActive(false);
		baselineMusic.PlayDelayed(2.0f);
		dangerMusic.PlayDelayed(2.0f);

		// Initial sound effects
		VO_LetHuntBegin.Play();
		// Loop pumpkin and Scarecrow Footsteps, set to mute for both of them

	}

	void Update() {
		// Get the distance, gets minimum distance between hunter and preys
		if (preyList.Length == 3)
			distance = Mathf.Min(Vector3.Distance (Hunter.transform.position, preyList[0].transform.position), Mathf.Min(Vector3.Distance (Hunter.transform.position, preyList[1].transform.position), Vector3.Distance(Hunter.transform.position, preyList[2].transform.position)));
		if (preyList.Length == 2)
			distance = Mathf.Min (Vector3.Distance (Hunter.transform.position, preyList [0].transform.position), Vector3.Distance (Hunter.transform.position, preyList [1].transform.position));
		if (preyList.Length == 1)
			distance = Vector3.Distance (Hunter.transform.position, preyList [0].transform.position);
		if (distance <= closeDistance)
			dangerMusic.volume = 1.0f;
		// Volume varies based on the square of 1 - (distance - closeDistance) / (farDistance - closeDistance)
		else if (distance <= farDistance)
			dangerMusic.volume = Mathf.Pow(1.0f - ((distance - closeDistance) / (farDistance - closeDistance)), 2.0f);
		else
			dangerMusic.volume = 0.0f;
	}
}
