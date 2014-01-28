using UnityEngine;
using System.Collections;

public class Menu_Object_Continue : MonoBehaviour 
{
	public bool isStart = false;
	public bool isContinue = false;
	public bool isOptions = false;
	public bool isCredits = false;
	
	public static bool isA = false;
	
	void Start () 
	{
		
	}
	
	void OnMouseEnter()
	{
		renderer.material.color = Color.black;
		isA = true;
	}
	
	void OnMouseExit()
	{
		renderer.material.color = Color.white;
		isA = false;
		
		
	}
	
	void OnMouseDown()
	{
		if(isStart)
		{
			Application.LoadLevel("Level_One");
		}
	}
}
