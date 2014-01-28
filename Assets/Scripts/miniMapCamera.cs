using UnityEngine;
using System.Collections;

public class miniMapCamera : MonoBehaviour {
	public Transform target;

	// Use this for initialization
	void Start () {
		target = GameObject.Find("player 1").transform;

	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
	}
}
