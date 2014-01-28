using UnityEngine;
using System.Collections;

public class AI_Behavior : MonoBehaviour{
	
	public Transform m_Player;
	public float patrolSpeed = 2f;
	public float chaseSpeed = 5f;
	public float chaseWaitTime = 5f;
	public float patrolWaitTime = 1f;
	public Transform[] patrolWayPoints;

	//pivate EnemySight enemySight;
	private NavMeshAgent nav;
	//private Transform player;
	private float patrolTimer;
	private int wayPointIndex;  

	//public Object waypoint;
	// Use this for initialization
	void Start () 
	{
		animation["Default0"].layer = 0; animation["Default"].wrapMode = WrapMode.Loop;
		animation["Idle"].layer = 1; animation["Idle"].wrapMode = WrapMode.Loop;
		animation["Stand"].layer = 1; animation["stand"].wrapMode = WrapMode.Loop;
		animation["Stand"].layer = 1; animation["stand"].wrapMode = WrapMode.Loop;
		animation["Walk"].layer = 3; animation["Walk"].wrapMode = WrapMode.Loop;
		animation["injuredwalk"].layer = 3; animation["injuredwalk"].wrapMode = WrapMode.Once;
		animation["jumpaway"].layer = 3; animation["jumpaway"].wrapMode = WrapMode.Once;
		animation["jumpdown"].layer = 3; animation["jumpdown"].wrapMode = WrapMode.Once;
		animation["ladder"].layer = 3; animation["ladder"].wrapMode = WrapMode.Once;
		animation["push"].layer = 3; animation["push"].wrapMode = WrapMode.Once;
		animation["ropeclimb"].layer = 3; animation["ropeclimb"].wrapMode = WrapMode.Once;
		animation["hang"].layer = 3; animation["hang"].wrapMode = WrapMode.Once;
		animation["ropeswing"].layer = 3; animation["ropeswing"].wrapMode = WrapMode.Once;

		//waypoint = GameObject.FindGameObjectsWithTag ("waypoint_1");

		//lastPlayerSighting = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<LastPlayerSighting>();

		
	}
	
	// Update is called once per frame
	void Update () 
	{
		//GetComponent<NavMeshAgent> ().destination = m_Player.position;
		//GetComponent<NavMeshAgent> ().speed = 3.5f;
		//animation.Play("run");
		animation.Play("Walk");
		Patrolling();
		//if(GetComponent<NavMeshAgent> ().collider.
	}

	void Patrolling()
	{
		// Set an appropriate speed for the NavMeshAgent.
		//nav.speed = patrolSpeed;
		
		// If near the next waypoint or there is no destination...
		//if(nav.destination == lastPlayerSighting.resetPosition || nav.remainingDistance < nav.stoppingDistance)
		//{
			// ... increment the timer.
			patrolTimer += Time.deltaTime;
			
			// If the timer exceeds the wait time...
			if(patrolTimer >= patrolWaitTime)
			{
				// ... increment the wayPointIndex.
				if(wayPointIndex == patrolWayPoints.Length - 1)
					wayPointIndex = 0;
				else
					wayPointIndex++;
				
				// Reset the timer.
				patrolTimer = 0;
			}
		//}
		Vector3 pos = nav.nextPosition;
		if(pos != patrolWayPoints[wayPointIndex].position)
			// If not near a destination, reset the timer.
			patrolTimer = 0;

		// Set the destination to the patrolWayPoint.
		//GetComponent<NavMeshAgent> ().destination = m_Player.position;
		GetComponent<NavMeshAgent> ().destination = patrolWayPoints[wayPointIndex].position;
	}
}
