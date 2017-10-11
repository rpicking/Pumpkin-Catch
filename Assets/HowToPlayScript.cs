using UnityEngine;
using System.Collections;

using PumpkinJam;

public class HowToPlayScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Return) ||
		    Input.GetButton("p1Ability1") ||
		    Input.GetButton("p2Ability1"))
		{
			Application.LoadLevel("PlayerSelect");
		}
	}
}
