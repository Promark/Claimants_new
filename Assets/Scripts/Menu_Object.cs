using UnityEngine;
using System.Collections;

public class Menu_Object : MonoBehaviour 
{
	public bool isStart = false;
	public bool isContinue = false;
	public bool isOptions = false;
	public bool isCredits = false;
	
	bool isPressed = false, isPressed_2 = false;
	float i;
	private float startTime, elapsedTime;
	
	public static bool isActive = false;
	
	
	void Start () 
	{
		startTime = 0;
	}
	void Update()
	{
		
		if(isPressed == true)
		{
			this.audio.Play();
			isPressed = false;
		}
		if(isPressed_2 == true)
		{
			elapsedTime = Time.time - startTime;
			if(elapsedTime > this.audio.clip.length)
			{
				//this.audio.Stop();
				Application.LoadLevel("StoryBoard");
			}
		}
	}
	void OnMouseEnter()
	{
		renderer.material.color = Color.black;
		isActive = true;
	}
	
	void OnMouseExit()
	{
		renderer.material.color = Color.white;
		isActive = false;
		
		
	}
	
	void OnMouseDown()
	{
		if(isStart)
		{
			isPressed = true;
			isPressed_2 = true;
			startTime = Time.time;
			
		}
	}
}
