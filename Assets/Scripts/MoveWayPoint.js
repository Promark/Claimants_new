/*
PUBLIC VARIABLES

Points - array of waypoint gameobjects

Start Point Index - The point in the array character should start at

Move Speed - um, the move speed

Attack speed multiplier - the move speed will be multiplied by this when attacking

Player - if the character can attack you, set this to the player game object

Attack Range - how close you can get to the character before it will attack you

Hit Range - how close the character needs to get to you before it will stop chasing and start smacking

Search time - if you trying to hide behind something while being attacked, attacker will continue search for you for this long (in seconds)

Min Wait Time - when getting a new way point target, what is the minimum about of time to wait before proceeding

Max Wait Time - when getting a new way point target, what is the maximum about of time to wait before proceeding

Constantly randomise points - if true, then the array of points will be shuffled before the next waypoint is selected

--------
If you are not presetting any points and want the script to randomly generate points, set the following

Point Marker - the GameObject to instantiate for each point

Random Range - how far away from the characters starting position can the random points be generated

Random Points to create - the number of random points to generate

Random Points above char - the starting y position (above the character) for the random points before they drop to the ground.  In hilly areas this should be quite high, in flat areas it can be low


*/


var points: GameObject[];
var startPointIndex:int = 0;
private var targetPoint:int = startPointIndex;
var moveSpeed: float = .05;
var attackSpeedMultiplier:float=100.0;
var player:GameObject;
var attackRange:float=25.0;
var hitRange:float=5.0;
var searchTime:float=5.0;
private var targetPosition: Vector3;
private var changeDistance:float;
private var yPos: float;
private var paused: boolean=true;
var minWaitTime: float=1.0;
var maxWaitTime: float=5.0;
var constantlyRandomisePoints: boolean=false;


var pointMarker:GameObject;
var randomRange:int =20;
var randomPointsToCreate:int=6;
//this variable is used to control how high above the character random points should be created.  They will then drop down until they hit the ground.  
var randomPointsAboveChar:int=5;
private var targetPositionForRotation:Vector3;
private var controller : CharacterController;
//init char after all points have landed

private var initialised:boolean=false;

private var tempPoints:Array=new Array();
private var tempPointCounter:int=0;
private var tempPointPosition:Vector3;
private var minPointSpace:float=1;

private var attackingPlayer:boolean=false;
//the time that the attacker lost sight of the player
private var lostTime:float;
private var currentTargetObject:GameObject;

function init(){
	initialised=true;
	transform.position.x=points[targetPoint].transform.Find("Registration").transform.position.x;
	transform.position.z=points[targetPoint].transform.Find("Registration").transform.position.z;
	transform.position.y=points[targetPoint].transform.Find("Registration").transform.position.y;
	//this was just for testing
	//GameObject.Find("First Person Controller").transform.position=points[targetPoint].transform.position+Vector3(-3,2,-3);
	//GameObject.Find("First Person Controller").transform.rotation=Quaternion.identity;
	transform.Find("EmitFlames").GetComponent(ParticleEmitter).emit=true;
	getNewTarget(0);
	ignoreCollision("Point");

}


function Start(){
		if(points.length==0){
			//no points assign - create random points around character
			points=new Array(randomPointsToCreate);
			for(i=0;i<randomPointsToCreate;i++){
				var max=randomRange/2;
				var min=0-max;
				var ranPos=Vector3(transform.position.x+Random.Range(min,max), transform.position.y+randomPointsAboveChar, transform.position.z+Random.Range(min,max));
				points[i]=Instantiate(pointMarker,ranPos,Quaternion.identity);
			}
		}
		
		//regardless of whether the points were manually placed in scene or randomly generated, we set a few variables for them
		for(i=0;i<points.length;i++){
			//all point markers go on layer 8
			points[i].layer=8;
			points[i].name="Point - Main - "+transform.root.gameObject.name+" - "+i;
		}
		if(constantlyRandomisePoints){
			points=Shuffle(points);
		}
		changeDistance=moveSpeed*10;
		animation.wrapMode=WrapMode.Loop;
		animation.CrossFade("idle");
		transform.position.y=1000;
		controller = GetComponent(CharacterController);
}

function getNewTarget(waitTime: float){
	yield WaitForSeconds(waitTime);
	targetPoint++;
	if(targetPoint==points.length){
		targetPoint=0;
	}
	if(constantlyRandomisePoints){
		points=Shuffle(points);	
	}
	targetPosition=points[targetPoint].transform.Find("Registration").transform.position;
	targetPositionForRotation=targetPosition;
	animation.CrossFade("walk");
	//transform.LookAt(targetPosition);
	yield WaitForSeconds(.3);
	animation.CrossFade("idle");
	yield WaitForSeconds(1.5);
	var totalDistance=Vector3.Distance(transform.position, targetPosition);
	var pointSpacer=totalDistance/11;
	if(pointSpacer<minPointSpace){
		pointSpacer=minPointSpace;
	}
	for(i=0;i<tempPoints.length;i++){
		Destroy(tempPoints[i]);		
	}
	for(i=1;i<=10;i++){
		var tempPos=transform.position+(pointSpacer*i*transform.TransformDirection(Vector3.forward));
		tempPos.y+=2;
		tempPoints[i-1]=Instantiate(pointMarker,tempPos,Quaternion.identity);
		//all point markers go on layer 8
		tempPoints[i-1].layer=8;
		tempPoints[i-1].name="Point - Temp - "+transform.root.gameObject.name+" - "+(i-1);
	}
	ignoreCollision("Point");
	yield WaitForSeconds(2);
	tempPointCounter=0;
	tempPointPosition=tempPoints[tempPointCounter].transform.Find("Registration").transform.position;
	targetPositionForRotation=tempPointPosition;
	currentTargetObject=tempPoints[tempPointCounter];
	//transform.LookAt(tempPointPosition);
	animation.CrossFade("walk");
	paused=false;
}

function FixedUpdate () {
	
	if(!initialised){
		//see if all main points are on ground
		var allGrounded:boolean=true;
		for(i=0;i<points.length;i++){
			if(points[i].GetComponent(FallToGround).isOnGround()==false){
				allGrounded=false;
				break;	
			}	
		}
		if(allGrounded){
			init();		
		}	
	}
	if(!paused && !attackingPlayer){
		if(Vector3.Distance(transform.position, targetPosition)<changeDistance){
			paused=true;
			animation.CrossFade("idle");
			getNewTarget(Random.Range(minWaitTime,maxWaitTime));
		}else if(Vector3.Distance(transform.position, tempPointPosition)<changeDistance){
			tempPointCounter++;
			if(tempPointCounter<tempPoints.length){
				tempPointPosition=tempPoints[tempPointCounter].transform.Find("Registration").transform.position;
				targetPositionForRotation=tempPointPosition;
				currentTargetObject=tempPoints[tempPointCounter];
				animation.CrossFade("walk");
				//transform.LookAt(tempPointPosition);
			}else{
				//transform.LookAt(targetPosition);
				targetPositionForRotation=targetPosition;
				currentTargetObject=points[targetPoint];
			}
		}
		if(!paused){
			transform.position+=moveSpeed*transform.TransformDirection(Vector3.forward);
			if(!CanSeePoint(currentTargetObject)){
				paused=true;
				animation.CrossFade("idle");
				getNewTarget(Random.Range(minWaitTime,maxWaitTime));
			}
		}
	}else if(attackingPlayer){
		transform.LookAt(player.transform.position);
		if(Vector3.Distance(transform.position, player.transform.position)>hitRange){
			animation.CrossFade("run");
			controller.SimpleMove(moveSpeed*attackSpeedMultiplier*transform.TransformDirection(Vector3.forward));
		}else{
			animation.CrossFade("jump");	
		}
	}
	//don't rotate at the very start
	if(targetPositionForRotation!=Vector3.zero && targetPositionForRotation!=null){
		if((targetPositionForRotation - transform.position)!=Vector3.zero){
			var rotation = Quaternion.LookRotation(targetPositionForRotation - transform.position);
			transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 3);
		}
	}
	//Debug.Log(Vector3.Distance(transform.position, player.transform.position));
	if(player!=null && Vector3.Distance(transform.position, player.transform.position)<attackRange){
		if(CanSeePlayer(player)){
			attackingPlayer=true;
			lostTime=0;
		}else{
			//before we set attackingPlayer to false, check if we were just attacking character and if so get a new target.  We now wait for 'searchTime' before we give up and go back to the points
			if(lostTime==0){
				lostTime=Time.time;
			}
			if(Time.time-lostTime>searchTime){
				if(attackingPlayer){
					paused=true;
					animation.CrossFade("idle");
					getNewTarget(Random.Range(minWaitTime,maxWaitTime));
				}
				attackingPlayer=false;
			}
		}
	}else{
		//before we set attackingPlayer to false, check if we were just attacking character and if so get a new target
		if(attackingPlayer){
			paused=true;
			animation.CrossFade("idle");
			getNewTarget(Random.Range(minWaitTime,maxWaitTime));
		}
		attackingPlayer=false;
	}		
}

function CanSeePoint (t:GameObject) : boolean {
	var startPos:Vector3=transform.position;
	startPos.y+=controller.height/2;
	var hit : RaycastHit;
	if(t!=null){
		if (Physics.Linecast (startPos, t.transform.position, hit)){
			//Debug.Log(hit.collider.gameObject.name);
			return hit.collider.gameObject == currentTargetObject;
		}
	}
	return false;
}

function CanSeePlayer (t:GameObject) : boolean {
	var startPos:Vector3=transform.position;
	startPos.y+=1;
	var hit : RaycastHit;
	//when looking for character, ignore hits with point objects
	  // Bit shift the index of the layer (8) to get a bit mask
  	var layerMask = 1 << 8;
  	// This would cast rays only against colliders in layer 8.
  	// But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
 	layerMask = ~layerMask;
	if (Physics.Linecast (startPos, t.transform.position, hit,layerMask)){
		//Debug.Log(hit.collider.gameObject.name);
		return hit.collider.gameObject == player;
	}
	return false;
}


function Shuffle(a:Array){
	var temp:GameObject;
	for(i=0;i<a.length;i++){
		temp=a[i];
		var swapIndex:int=Random.Range(0,a.length);
		a[i]=a[swapIndex];
		a[swapIndex]=temp;
	}
	//this ensures that after the points are shuffled, the current point is not the same as the next target point
	while(a[targetPoint].transform.Find("Registration").transform.position==targetPosition){
		a=ShuffleB(a);
	}
	return a;
}

function ShuffleB(a:Array){
	var temp:GameObject;
	for(i=0;i<a.length;i++){
		temp=a[i];
		var swapIndex:int=Random.Range(0,a.length);
		a[i]=a[swapIndex];
		a[swapIndex]=temp;
	}
	return a;
}

function ignoreCollision(tag : String) { 
    var objects = GameObject.FindGameObjectsWithTag(tag); 
    for (o in objects) { 
        if (o.GetComponent("Collider") && o != gameObject)
        Physics.IgnoreCollision(collider, o.collider); 
    } 
} 

