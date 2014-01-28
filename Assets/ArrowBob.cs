using UnityEngine;
using System.Collections;

public class ArrowBob : MonoBehaviour {
	private GameObject Player;
	float amplitude,speed, y;
	Vector3 target;
	// Use this for initialization
	void Start () {

		y = transform.position.y;
		amplitude = .125f;
		speed = 2;
	
	}
	
	// Update is called once per frame
	void Update () {

		transform.position = new Vector3 (transform.position.x, y + amplitude * Mathf.Sin(speed*Time.time), transform.position.z);
	
	}
}
