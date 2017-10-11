using UnityEngine;
using System.Collections;

public class bulletscript : MonoBehaviour {

	public float speed;
	GameObject GameSound;
	
	// Use this for initialization
	void Start () {
		Debug.Log (this.transform.forward * speed);
		this.gameObject.GetComponent<Rigidbody>().velocity = this.transform.forward * speed;
		Destroy (this.gameObject, 1.0f);
		GameSound = GameObject.Find ("GameMusic");
	}

	void OnCollisionEnter (Collision coll) {
		if (coll.gameObject.tag == "Prey") {
			Physics.IgnoreCollision(coll.collider, GetComponent<Collider>());
		}
		if(coll.gameObject.tag == "Hunter") {
			//Prey wins
			coll.gameObject.SetActive(false);
			GameSound.GetComponent<GameAudio>().PumpkinDeath.Play();
			GameSound.GetComponent<GameAudio>().VO_GameOver.Play();
			GameSound.GetComponent<GameAudio>().VO_PreyVictory.PlayDelayed(2);
			Application.LoadLevel("PreyWins");
		}
	}
}
