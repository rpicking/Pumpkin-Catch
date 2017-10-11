using UnityEngine;
using System.Collections;

using PumpkinJam;

public class HunterController : MonoBehaviour {

	public float speed;
	public float leapspeed;
	float ability1cooldown;
	float ability2cooldown;
	GameObject GameSound;

	IEnumerator boost() {
		GetComponent<Rigidbody>().AddForce (GetComponent<Rigidbody>().velocity.normalized * speed * Time.deltaTime * leapspeed);
		ability1cooldown = 10;
		yield return new WaitForSeconds (.2f);
		GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * speed * Time.deltaTime;
	}

	void Start() {
		GameSound = GameObject.Find("GameMusic");
		ability1cooldown = 0;
	}

	void OnGUI() {
		GUI.Box(new Rect(600, 10, 100, 30), "Jump: " + (int)ability1cooldown);
	}
	
	void FixedUpdate () {

		if (ability1cooldown > 0) {
			ability1cooldown -= Time.deltaTime;
		}

		if (PlayerData.getHunter().up()) {
			GetComponent<Rigidbody>().AddForce (Vector3.forward * speed * Time.deltaTime);
			transform.forward = GetComponent<Rigidbody>().velocity.normalized * Time.deltaTime;
		}
		if (PlayerData.getHunter().left()) {
			GetComponent<Rigidbody>().AddForce (Vector3.left * speed * Time.deltaTime);
			transform.forward = GetComponent<Rigidbody>().velocity.normalized * Time.deltaTime;
		}
		if (PlayerData.getHunter().down()) {
			GetComponent<Rigidbody>().AddForce (-Vector3.forward * speed * Time.deltaTime);
			transform.forward = GetComponent<Rigidbody>().velocity.normalized * Time.deltaTime;
		}
		if (PlayerData.getHunter().right()) {
			GetComponent<Rigidbody>().AddForce (-Vector3.left * speed * Time.deltaTime);
			transform.forward = GetComponent<Rigidbody>().velocity.normalized * Time.deltaTime;
		}

		if (PlayerData.getHunter().ability1() && ability1cooldown <= 0){
			GameSound.GetComponent<GameAudio>().Whoosh.Play();
			StartCoroutine("boost");
			Debug.Log ("Hunter ability 1");
		}
		//if (rigidbody.velocity.magnitude > 2)
		//	GameSound.GetComponent<GameAudio>().PumpkinFootsteps.mute = false;
		//else
		//	GameSound.GetComponent<GameAudio>().PumpkinFootsteps.mute = true;
	}
}
