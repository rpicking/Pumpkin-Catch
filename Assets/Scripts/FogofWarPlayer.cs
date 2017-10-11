using UnityEngine;
using System.Collections;

public class FogofWarPlayer : MonoBehaviour {

	public Transform FogofWarPlane;
	public Camera PlayerCamera;
	public int Number;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 screenPos = PlayerCamera.WorldToScreenPoint (transform.position);
		Ray raytoPlayerPos = PlayerCamera.ScreenPointToRay (screenPos);

		RaycastHit hit;
		if(Physics.Raycast(raytoPlayerPos, out hit, 1000)) {
			FogofWarPlane.GetComponent<Renderer>().material.SetVector("_Player" + Number.ToString () +"_Pos", hit.point);
		}
	}
}
