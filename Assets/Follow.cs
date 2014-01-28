using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {

	public Transform m_Player;
	//public Object waypoint;
	// Use this for initialization
	void Start () 
	{
		animation["idle"].layer = 0; animation["idle"].wrapMode = WrapMode.Loop;
		animation["walk"].layer = 1; animation["walk"].wrapMode = WrapMode.Loop;
		animation["run"].layer = 1; animation["run"].wrapMode = WrapMode.Loop;
		animation["eaglejump"].layer = 3; animation["eaglejump"].wrapMode = WrapMode.Once;
		animation["injuredwalk"].layer = 3; animation["injuredwalk"].wrapMode = WrapMode.Once;
		animation["jumpaway"].layer = 3; animation["jumpaway"].wrapMode = WrapMode.Once;
		animation["jumpdown"].layer = 3; animation["jumpdown"].wrapMode = WrapMode.Once;
		animation["ladder"].layer = 3; animation["ladder"].wrapMode = WrapMode.Once;
		animation["push"].layer = 3; animation["push"].wrapMode = WrapMode.Once;
		animation["ropeclimb"].layer = 3; animation["ropeclimb"].wrapMode = WrapMode.Once;
		animation["hang"].layer = 3; animation["hang"].wrapMode = WrapMode.Once;
		animation["ropeswing"].layer = 3; animation["ropeswing"].wrapMode = WrapMode.Once;

		//waypoint = GameObject.FindGameObjectsWithTag ("waypoint_1");

	}
	
	// Update is called once per frame
	void Update () 
	{
		GetComponent<NavMeshAgent> ().destination = m_Player.position;
		GetComponent<NavMeshAgent> ().speed = 3.5f;
		//animation.Play("run");
		animation.Play("Walk");
		//if(GetComponent<NavMeshAgent> ().collider.
	}
}
