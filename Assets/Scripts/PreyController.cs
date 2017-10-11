using UnityEngine;
using System.Collections;

namespace PumpkinJam {
public class PreyController : MonoBehaviour {

	Animator anim;
	public GameObject bullet_prefab;
	public float speed;
	public float leapspeed;
	public GUIText countText;

	public static int keycount;
	public Vector3 direction;
	public static int shotgun;
	float ability1cooldown;
	bool ability2cooldown;
	GameObject GameSound;
		GameObject GunSpriteGameObject;

	void SetCountText(int keycount) {
		countText.text = "= " + keycount.ToString ();
	}

	void Start() {
		anim = GetComponent<Animator>();
		keycount = 0;
		SetCountText (keycount);
		ability1cooldown = 0;
		ability2cooldown = false;
		shotgun = 0;
		GameSound = GameObject.Find("GameMusic");
		GunSpriteGameObject = GameObject.Find ("GunSprite");
	}

	//Collision detection with other player
	void OnCollisionEnter(Collision collision) {
		if(collision.gameObject.tag == "Hunter" && this.gameObject.tag == "Prey") {
			GameSound.GetComponent<GameAudio>().ScarecrowDeath.Play();
			this.gameObject.SetActive(false);
			//Hunter wins
			GameSound.GetComponent<GameAudio>().VO_GameOver.Play();
			GameSound.GetComponent<GameAudio>().VO_HunterVictory.PlayDelayed(2);
			Application.LoadLevel("HunterWins");
		}
	}

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.tag == "Key" && this.gameObject.tag == "Prey" && keycount != 1) {
			other.gameObject.SetActive(false);
			keycount = keycount + 1;
			SetCountText(keycount);
			GameSound.GetComponent<GameAudio>().KeyCollect.Play();
		}

		if(other.gameObject.tag == "Shotgun" && this.gameObject.tag == "Prey") {
			other.gameObject.SetActive(false);
			shotgun = 1;
			GunSpriteGameObject.SetActive(true);
			GameSound.GetComponent<GameAudio>().GunCock.Play();
			GameSound.GetComponent<GameAudio>().VO_HunterToHunted.PlayDelayed(1);
		}
	}

	IEnumerator boost() {
		GetComponent<Rigidbody>().AddForce (GetComponent<Rigidbody>().velocity.normalized * speed * Time.deltaTime * leapspeed);
		ability1cooldown = 10;
		yield return new WaitForSeconds (.2f);
		GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity.normalized * speed * Time.deltaTime;
	}

	IEnumerator shoot() {
		Instantiate(bullet_prefab, transform.position, transform.rotation);
		ability2cooldown = true;
		GunSpriteGameObject.SetActive(false);
		yield return new WaitForSeconds(1.5f);
		GunSpriteGameObject.SetActive(true);
		ability2cooldown = false;
	}

	void FixedUpdate () {
		if (GetComponent<Rigidbody>().velocity.magnitude <= 1 ) {
			anim.SetBool ("RunBool" , false);
		}

		if (ability1cooldown > 0) {
			ability1cooldown -= Time.deltaTime;
		}
		
		if (PlayerData.getPrey().up()) {
			anim.SetBool("RunBool" , true);
			GetComponent<Rigidbody>().AddForce (Vector3.forward * speed * Time.deltaTime);
			transform.forward = GetComponent<Rigidbody>().velocity.normalized * Time.deltaTime;
			direction = GetComponent<Rigidbody>().velocity.normalized;
		}

		if (PlayerData.getPrey().left()) {
			anim.SetBool("RunBool" , true);
			GetComponent<Rigidbody>().AddForce (Vector3.left * speed * Time.deltaTime);
			transform.forward = GetComponent<Rigidbody>().velocity.normalized * Time.deltaTime;
			direction = GetComponent<Rigidbody>().velocity.normalized;
		}

		if (PlayerData.getPrey().down()) {
			anim.SetBool("RunBool" , true);
			GetComponent<Rigidbody>().AddForce (-Vector3.forward * speed * Time.deltaTime);
			transform.forward = GetComponent<Rigidbody>().velocity.normalized * Time.deltaTime;
			direction = GetComponent<Rigidbody>().velocity.normalized;
		}

		if (PlayerData.getPrey().right()) {
			anim.SetBool("RunBool" , true);
			GetComponent<Rigidbody>().AddForce (-Vector3.left * speed * Time.deltaTime);
			transform.forward = GetComponent<Rigidbody>().velocity.normalized * Time.deltaTime;
			direction = GetComponent<Rigidbody>().velocity.normalized;
		}

		if (PlayerData.getPrey().ability1() && ability1cooldown <= 0) {
			GameSound.GetComponent<GameAudio>().Whoosh.Play();
			anim.SetTrigger("Jump");
			StartCoroutine("boost");
		}
		if(PlayerData.getPrey().ability2() && !ability2cooldown && shotgun == 1) {
			GameSound.GetComponent<GameAudio>().Gunshot.Play();
			StartCoroutine("shoot");
		}
		if (GetComponent<Rigidbody>().velocity.magnitude > 2)
			GameSound.GetComponent<GameAudio>().ScarecrowFootsteps.mute = false;
		else
			GameSound.GetComponent<GameAudio>().ScarecrowFootsteps.mute = true;
	}
}
}
