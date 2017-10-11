using UnityEngine;
using System.Collections;

using PumpkinJam;

public class HunterWinsMenu : MonoBehaviour {

	void Start () {

	}

	IEnumerator wait() {
		yield return new WaitForSeconds (0.2f);
		Application.LoadLevel ("MainMenu");
	}
	// Update is called once per frame
	void Update () {
		if(PlayerData.getPrey().ability1() || PlayerData.getHunter().ability1()) {
			StartCoroutine("wait");
		}
	}
}
