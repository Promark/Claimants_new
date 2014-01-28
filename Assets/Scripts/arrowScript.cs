using UnityEngine;
using System.Collections;

public class arrowScript : MonoBehaviour {
	public GameObject goToTrack;
	public Camera cam;
	
	void Update () {
		Vector3 v3Screen = cam.WorldToViewportPoint(goToTrack.transform.position);
		//if (v3Screen.x > -0.01f && v3Screen.x < 1.01f && v3Screen.y > -0.01f && v3Screen.y < 1.01f)
			//renderer.enabled = false;
		//else
		{
			renderer.enabled = true;
			v3Screen.x = Mathf.Clamp (v3Screen.x, 0.01f, 0.99f);
			v3Screen.y = Mathf.Clamp (v3Screen.y, 0.01f, 0.99f);
			transform.position = cam.ViewportToWorldPoint (v3Screen);
		}
		
	}
}
