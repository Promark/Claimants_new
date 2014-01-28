using UnityEngine;
using System.Collections;

public class Halo_Me_Credits : MonoBehaviour {
	
	public bool active;

	// Use this for initialization
	void Start () 
	{
		//gameObject.SetActive (false);
		

	}
	
	
	
	// Update is called once per frame
	
	 void Update ()
    {
		
		active = Menu_Object_Credits.isA;
       foreach (Transform child in this.transform)
       {
			
			if(active == true)
		{
			child.gameObject.SetActive (true);
		}
			
         	if(active == false)
		{
			child.gameObject.SetActive (false);
		}
       }
    }
	
	
	
}
