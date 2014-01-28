using UnityEngine;
using System.Collections;

public class pointing : MonoBehaviour {
	public GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.Find("player 1");
	
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.rotation =  player.gameObject.transform.rotation;
		gameObject.transform.position = player.gameObject.transform.position;
	
	}
}
