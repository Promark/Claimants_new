using UnityEngine;
using System.Collections;

public class Enter : MonoBehaviour {
	bool enterable = false;
	public GameObject g;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void OnTriggerEnter(Collider other){
		if(other.tag == "Player")
		{
			enterable = true;

		}
	}
	public void OnTriggerExit(Collider other){
		if (other.tag == "Player")
			enterable = false;
	}
	public void OnGUI()
	{
		if(enterable)
		{
			GUI.Label(new Rect(10,10, 100,50),"Press E to enter");
			if(Input.GetKeyDown(KeyCode.E))
			{

				Application.LoadLevel("Shack");
			}
		}
	}
}
