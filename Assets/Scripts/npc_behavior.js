#pragma strict
var target : Transform;
var isInfected = false;
var isTalking = false;


var NextWaypoint = 5.0;
var rotationSpeed = 5.0;
var speed = 3.0;
var dist = 0;



function Start() 
{
	//if the trget is null then set the target with tags
	if (target == null && GameObject.FindWithTag("SimplePlayer"))
	{
		target = GameObject.FindWithTag("SimplePlayer").transform;
	}
			
	//Runs Patrol function
	patrol();
	
}

function patrol()
{
	var curWayPoint = AutoWayPoint.FindClosest(transform.position);
	
	while(isTalking == false && isInfected == false)
	{
		var waypointPosition = curWayPoint.transform.position;
		
		dist = Vector3.Distance(waypointPosition, transform.position);
		//If close enough to a waypoint pick next one
		if (Vector3.Distance(waypointPosition, transform.position) < NextWaypoint)
		{
			//sets next waypoint as your current waypont
			curWayPoint = PickNextWaypoint (curWayPoint);
		}
			
		Move(waypointPosition);
			
		yield;
	}
}

function CanSeePlayer() : boolean
{
	var hit : RaycastHit;

	//if the target is alive and our raycast is successful the return true
	if (target != null && Physics.Linecast (transform.position, target.position, hit))
	{
		return hit.transform == target;
	}
	
	return false;
}

function SearchForPlayer()
{
	
}

function RotateToTarget()
{
	
}

function Move (position : Vector3)
{
	var direction = position - transform.position;
		direction.y = 0;
		if (direction.magnitude < 4.0) 
		{
			//SetSpeed(0.0);
			return;
		}
	// Rotate towards the target
	transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
	transform.eulerAngles = Vector3(0, transform.eulerAngles.y, 0);

	// Modify speed so we slow down
	var forward = transform.TransformDirection(Vector3.forward);
	var speedModifier = Vector3.Dot(forward, direction.normalized);
	speedModifier = Mathf.Clamp01(speedModifier);

	// Move the character
	direction = forward * speed * speedModifier;
	GetComponent (CharacterController).SimpleMove(direction);
}

function Update () 
{
	
}

function SetSpeed()
{
	
}

function PickNextWaypoint (currentWaypoint : AutoWayPoint) 
	{
	
		// The direction in which we are walking
		var forward = transform.TransformDirection(Vector3.forward);

		// The closer the two vectors are, the larger the dot product will be.
		var best = currentWaypoint;
		var bestDot = -10.0;
		for (var cur : AutoWayPoint in currentWaypoint.connected) 
		{
			var direction = Vector3.Normalize(cur.transform.position - transform.position);
			var dot = Vector3.Dot(direction, forward);
			if (dot > bestDot && cur != currentWaypoint) 
			{
				bestDot = dot;
				best = cur;
			}
		}
	
		return best;
	}