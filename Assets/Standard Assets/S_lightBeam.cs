﻿using UnityEngine;
using System.Collections;

public class S_lightBeam : MonoBehaviour {
	float rotationSpeed = 40f;
	

	// Use this for initialization
	void Start () {
		
	
	}
	
	// Update is called once per frame
	void Update () {
		
		
	transform.Rotate(Vector3.forward,rotationSpeed);
    
	
		
	
	}
}
