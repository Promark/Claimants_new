using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class objectiveList : MonoBehaviour {
	public GameObject goToTrack;
	public Camera cam;
	public List<GameObject> list;
	// Use this for initialization
	void Start () {
		list = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {

//		foreach (GameObject g in list){
//			Vector3 v3Screen = cam.WorldToViewportPoint(goToTrack.transform.position);
//			//if (v3Screen.x > -0.01f && v3Screen.x < 1.01f && v3Screen.y > -0.01f && v3Screen.y < 1.01f)
//			//renderer.enabled = false;
//			//else
//			{
//				//renderer.enabled = true;
//				v3Screen.x = Mathf.Clamp (v3Screen.x, 0.01f, 0.99f);
//				v3Screen.y = Mathf.Clamp (v3Screen.y, 0.01f, 0.99f);
//				transform.position = cam.ViewportToWorldPoint (v3Screen);
			//}
		//}
	
	}
}
