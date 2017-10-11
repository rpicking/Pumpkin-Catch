using UnityEngine;
using System.Collections;

public class DoorControl : MonoBehaviour {

	// Is the door open or closed
	bool open;
	// Is the door currently moving
	bool opening;
	// Is the door on "cooldown"
	bool cooldown;
	// Amount to move per frame
	const float MOV_AMT = 0.02f;
	// Number of frames to wait between cooldowns
	const int TIMER_MAX = 180;
	// The current cooldown timer
	int timer;
	// Amount to move per frame (negative or positive)
	float movement;
	// How much we have moved so far
	float amt_moved;
	// Reference GameAudio for sound effect
	GameObject GameSound;

	// Use this for initialization
	void Start () {
		open = false;
		opening = false;
		cooldown = false;
		GameSound = GameObject.Find("GameMusic");
	}

	// When a player is in range of the door
	void OnTriggerEnter(Collider col) {
		if ((col.gameObject.tag == "Hunter" || col.gameObject.tag == "Prey") && !cooldown) {
			// Reset the amount moved and activate the cooldown/opening
			amt_moved = 0.0f;
			cooldown = true;
			opening = true;
			// Play the opening door sound effect
			GameSound.GetComponent<GameAudio>().DoorOpen.Play();

		}
	}
	
	// Update is called once per frame
	void Update () {
		// If the door is supposed to be moving...
		if (opening) {
			// If the door is currently open...
			if (open) {
				// Move in the opposite direction
				movement = -1 * MOV_AMT;
			} else {
				movement = MOV_AMT;
			}

			// Move the doors depending on their tag (each door is tagged according to position)
			if (gameObject.tag == "doorZPos" || gameObject.tag == "doorXNeg") {
				gameObject.transform.Translate(new Vector3(-movement, 0, 0));
			} else if (gameObject.tag == "doorZNeg" || gameObject.tag == "doorXPos") {
				gameObject.transform.Translate(new Vector3(movement, 0, 0));
			}

			// Every frame, keep track of how far we've moved
			amt_moved += MOV_AMT;

			// If the amount moved is 2.0 (one unit) AND the door is currently moving
			if (amt_moved >= 2.0f && opening) {
				// Set variables
				opening = false;
				open = !open;
				// If the door is closed, reset the cooldown
				if (!open) {
					cooldown = false;
				// Otherwise, start timing
				} else {
					timer = 0;
				}
			}
		// If we are currently on cooldown and the door is NOT opening
		} else if (cooldown) {
			// Increase the timer every frame until we reach our target time
			timer++;
			if (timer >= TIMER_MAX) {
				// Setup to close the doors
				amt_moved = 0.0f;
				opening = true;
			}
		}
	}
}
