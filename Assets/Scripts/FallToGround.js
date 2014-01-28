
private var onGround:boolean = false;

function Start(){
	ignoreCollision("Point");
	
}

function OnCollisionEnter(collision : Collision){
		if(collision.gameObject.tag=="Ground"){
			Destroy (rigidbody);
			onGround=true;
		}
}

function isOnGround(){
	return onGround;
}


function ignoreCollision(tag : String) { 
    var objects = GameObject.FindGameObjectsWithTag(tag); 
    for (o in objects) { 
        if (o.GetComponent("Collider") && o != gameObject)
        Physics.IgnoreCollision(collider, o.collider); 
    } 
} 
