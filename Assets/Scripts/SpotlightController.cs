using UnityEngine;
using System.Collections;

public class SpotlightController : MonoBehaviour {
	
	public GameObject player;
	public float height;
	private Vector3 offset;
	
	void start() {
		offset = transform.position;

	}
	
	void LateUpdate() {
		transform.position = player.transform.position + offset + new Vector3 (0, height, 0);
	
	}
}