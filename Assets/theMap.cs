using UnityEngine;
using System.Collections;

public class theMap : MonoBehaviour {
	bool entered = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerEnter(Collider other){
		if (other.tag == "Player"){
			entered = true;



		}
	}

}
