using UnityEngine;
using System.Collections;
 
public class MoveCamera : MonoBehaviour 
{
	
	
		
	public float zoomSpeed = 4.0f;		
	
	private Vector3 target;	
	private bool Position2, Position3, Position4;		
		
	private bool isZooming = true;		
	private bool isFirst = false;
	public AudioClip[] myAudioClip;
	private int currentClip = 0;
	private float startTime, elapsedTime;
	
	
	void Update () 
	{
		
		
		 
		
		if (isZooming)
		{
			if(!isFirst)
			{
			startTime = Time.time;
			audio.clip = myAudioClip[currentClip];
			audio.Play();
			isFirst = true;
			}
			transform.position += transform.forward * zoomSpeed * (Time.deltaTime / 10);
			elapsedTime = Time.time - startTime;
			if(elapsedTime > this.audio.clip.length)
			{
				isZooming = false;
				isFirst = false;
				
				Position2 = true;
				transform.position = new Vector3(257.545f, 17.69753f, 137.0086f);
			}
	        	
		}
		if(Position2)
		{
			
			if(!isFirst)
			{
			audio.clip = myAudioClip[currentClip+1];
			audio.Play();
			startTime = Time.time;	
			isFirst = true;
			}
			
			transform.position += Vector3.back * zoomSpeed * (Time.deltaTime / 10);
			elapsedTime = Time.time - startTime;
			if(elapsedTime > this.audio.clip.length)
			{
				Position2 = false;
				isFirst = false;
				Position3 = true;
				transform.position = new Vector3(294.3767f, 15.83698f, 218.655f);
				//transform.rotation= new Quaternion(-23.78656f, 714.0504f, 2.793457f);
			}
		}
		if(Position3)
		{
			
			if(!isFirst)
			{
			audio.clip = myAudioClip[currentClip+2];
			audio.Play();
			startTime = Time.time;	
			isFirst = true;
			}
			
			//transform.position += Vector3.back * zoomSpeed * (Time.deltaTime / 10);
			elapsedTime = Time.time - startTime;
			if(elapsedTime > this.audio.clip.length)
			{
				Position3 = false;
				isFirst = false;
				Position4 = true;
				transform.position = new Vector3(294.545f, 30.69753f, 218.0086f);
				//transform.rotation= new Vector3(-23.78656f, 714.0504f, 2.793457f);
			}
		}
		
		if(Position4)
		{
			
			if(!isFirst)
			{
			audio.clip = myAudioClip[currentClip+3];
			audio.Play();
			startTime = Time.time;	
			isFirst = true;
			}
			
			//transform.position += Vector3.back * zoomSpeed * (Time.deltaTime / 10);
			elapsedTime = Time.time - startTime;
			if(elapsedTime > this.audio.clip.length)
			{
				Position4 = false;
				isFirst = false;
				
				 //this is where you lead the next secene or start of game
				Application.LoadLevel("level_Zero");
			
			}
		}
		
		
		
		
	}
}
