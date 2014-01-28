

//------------------------------------------------------------------
var walkSpeed = 3.0;
var rotateSpeed = 30.0;
var attackSpeed = 8.0;
var attackRotateSpeed = 80.0;

var directionTraveltime = 2.0;
var idleTime = 1.5;
var attackDistance = 15.0;

var damage = 1;

var attackRadius = 2.5;
var viewAngle = 20.0;
var attackTurnTime = 1.0;
var attackPosition = new Vector3 (0, 1, 0);


//------------------------------------------------------------------

private var isAttacking = false;
private var lastAttackTime = 0.0;
private var nextPauseTime = 0.0;
private var distanceToPlayer;
private var timeToNewDirection = 0.0;

var target : Transform;
public var aggro = false;
public var beam : GameObject;
public var Player : GameObject;

private var characterController : CharacterController;
characterController = GetComponent(CharacterController);
//public var dmg : vp_PlayerDamageHandler;


function Start ()
{
	if (!target)
		target = GameObject.FindWithTag("Player").transform;
	
	beam = GameObject.Find("beam2");
	// Setup animations-----------------------------------------------------------------
	// set up properties for the animations
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
	// Rather than placing this in an update function, we can start the AI's behavior now and 
	//use coroutines to handle the changes
	yield WaitForSeconds(idleTime);
	
	while (true)	
	{
		// Idle around and wait for the player
		yield Idle();

		// Player has been located, prepare for the attack.
		if(aggro)
		yield Attack();
	}
}


function Idle ()
{
	// Walk around and pause in random directions unless the player is within range
	while (true)
	{
	animation.Play("walk");
		// Find a new direction to move
		if(Input.GetKeyDown(KeyCode.J))
		{
		beam.renderer.enabled = !beam.renderer.enabled;
		aggro = !aggro;
		}
		if(Time.time > timeToNewDirection)
		{
			yield WaitForSeconds(idleTime);
			
			if(Random.value > 0.5)
				transform.Rotate(Vector3(0,5,0), rotateSpeed);
			else
				transform.Rotate(Vector3(0,-5,0),rotateSpeed);
				
			timeToNewDirection = Time.time + directionTraveltime;
		}
		
		var walkForward = transform.TransformDirection(Vector3.forward);
		characterController.SimpleMove(walkForward * walkSpeed);
		
		distanceToPlayer  = transform.position - target.position;
			
		//We found the player!  Stop wasting time and go after him
		if (distanceToPlayer.magnitude < attackDistance)
			return;
		
		yield;
	}
} 

function Attack ()
{
	isAttacking = true;
	animation.Play("run");
	
	
	// We need to turn to face the player now that he's in range.
	var angle  = 0.0;
	var time  = 0.0;
	var direction : Vector3;
	while (angle > viewAngle || time < attackTurnTime)
	{
		time += Time.deltaTime;
		angle = Mathf.Abs(FacePlayer(target.position, attackRotateSpeed));
		move = Mathf.Clamp01((90 - angle) / 90);
		
		// depending on the angle, start moving
		animation["run"].weight = animation["run"].speed = move;
		direction = transform.TransformDirection(Vector3.forward * attackSpeed * move);
		characterController.SimpleMove(direction);
		
		yield;
	}
	
	
	// attack if can see player
	var lostSight = false;
	while (!lostSight)
	{
		angle = FacePlayer(target.position, attackRotateSpeed);
			
		// Check to ensure that the target is within the Bunny's eyesight
		if (Mathf.Abs(angle) > viewAngle)
			lostSight = true;
			
		// If  loses site of the player, he jumps out of here.
		if (lostSight)
			break;
		
		//Check to see if we're close enough to the player to bite 'em.
		var location = transform.TransformPoint(attackPosition) - target.position;
		if(Time.time > lastAttackTime + 1.0 && location.magnitude < attackRadius)
		{
			// deal damage
			animation.Play("push");
			
			target.SendMessage("Damage", 1.0f, SendMessageOptions.DontRequireReceiver);
			//dmg = Player.GetComponent(vp_DamageHandler);
			//maxHP = dmg.MaxHealth;
			//curHP = dmg.m_CurrentHealth;
			//hpBarSize = Screen.width / 3 / (maxHP/curHP);
		   // dmg.m_CurrentHealth -= .25f;
			
			lastAttackTime = Time.time;
		}

		if(location.magnitude > attackRadius)
			break;
			
		// Check to make sure our current direction didn't collide us with something
		if (characterController.velocity.magnitude < attackSpeed * 0.3)
			break;
		
		// yield for one frame
		yield;
	}

	isAttacking = false;
	
}

function FacePlayer(targetLocation : Vector3, rotateSpeed : float) : float
{
	// Find the relative place in the world where the player is located
	var relativeLocation = transform.InverseTransformPoint(targetLocation);
	var angle = Mathf.Atan2 (relativeLocation.x, relativeLocation.z) * Mathf.Rad2Deg;
	
	// Clamp it with the max rotation speed so he doesn't move too fast
	var maxRotation = rotateSpeed * Time.deltaTime;
	var clampedAngle = Mathf.Clamp(angle, -maxRotation, maxRotation);
	
	// Rotate
	transform.Rotate(0, clampedAngle, 0);
	// Return the current angle
	return angle;
}

//@script AddComponentMenu("Enemies/Bunny'sAIController")