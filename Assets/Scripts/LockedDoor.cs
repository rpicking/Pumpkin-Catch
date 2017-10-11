using UnityEngine;
using System.Collections;

using PumpkinJam;

public class LockedDoor : MonoBehaviour {
	bool opening;
	float amt_moved;
	float movement;
	const float MOV_AMT = 0.02f;
	GameObject GameSound;

	// Use this for initialization
	void Start () {
		opening = false;
		GameSound = GameObject.Find("GameMusic");
	}
	void OnTriggerEnter(Collider col) {
		if (col.gameObject.tag == "Prey" && PreyController.keycount == 1) {
			// Reset the amount moved and activate the cooldown/opening
			amt_moved = 0.0f;
			opening = true;
			// make some sound!!!
			GameSound.GetComponent<GameAudio>().DoorOpen.Play();
		}
	}
	// Update is called once per frame
	void Update () {

		if (opening) {
			movement = MOV_AMT;
			// Move the doors depending on their tag (each door is tagged according to position)
			if (gameObject.tag == "doorZPos" || gameObject.tag == "doorXNeg") {
				gameObject.transform.Translate(new Vector3(-movement, 0, 0));
			} else if (gameObject.tag == "doorZNeg" || gameObject.tag == "doorXPos") {
				gameObject.transform.Translate(new Vector3(movement, 0, 0));
			}
			amt_moved += MOV_AMT;

			if(amt_moved >= 2.0f) {
				opening = false;
			}
		}
	}
}
