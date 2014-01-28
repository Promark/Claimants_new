using UnityEngine;
using System.Collections;

public class Halo : MonoBehaviour {
	
	public bool active;

	// Use this for initialization
	void Start () {
		gameObject.SetActive (false);

	}
	
	// Update is called once per frame
	void Update () 
	{
		active = Menu_Object.isActive;
		if(active)
		{
			gameObject.SetActive (true);
		}
		
	
	}
}
