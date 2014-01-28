using UnityEngine;
using System.Collections;

public class Quest : MonoBehaviour {
	public bool questComplete = false, pickedUp = false;
	public string questDetail,questName;
	public bool questStarted = false;
	public float questNumber;


	public Quest(string sName,string sDetail, float number)
	{
		questName = sName;
		questDetail = sDetail;
		questNumber = number;

	}
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
