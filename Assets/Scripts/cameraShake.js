private var isHit : boolean = false;        // If camera has been hit or not

private var hitTime : float = 0;            // Used for timer reference point

private var tiltHitTime : float = 0;        // Used for timer reference point...not reset each cycle

private var originalPosition : float = 0;   // Used for X axis position reference point

private var originalTilt : float = 0;       // Used for z axis rotation reference point

var startingWobbleDistance : float = .8;    // Total distance the object travels over a wobble cycle, in meters

private var wobbleDistance : float = 0;     // Wobble distance for a given cycle

var distanceDecrease : float = .5;          // The percentage of wobble distance for each cycle compared to the previous one

var wobbleSpeed : float = 55;               // Increase to make wobbling faster

var numberOfWobbles : int = 3;              // How many times the object wobbles, total

private var wobbleNumber : int = 0;         // Keeps track of current wobble number for the entire sequence

var tiltAmplifier : float = 1.8;            // Value to multiply the camera tilt by

 

function Update () {

    if (Input.GetKey ("space")) {
  

        isHit = true;

        hitTime = Time.time;                            // Get the current time so we can have a reference point later

        tiltHitTime = Time.time;

        originalPosition = transform.localPosition.x;   // Also get current position & rotation for a reference point

        originalTilt = transform.eulerAngles.z;

        wobbleNumber = numberOfWobbles;

        wobbleDistance = startingWobbleDistance;

    }

    if (isHit) {

        // Make timers always start at 0 

        var timer : float = (Time.time - hitTime) * wobbleSpeed;

        var tiltTimer : float = (Time.time - tiltHitTime) * wobbleSpeed;

        transform.localPosition.x = originalPosition + Mathf.Sin(timer) * wobbleDistance;

        // Make camera tilt only do 1/2 sine wave cycle over entire animation

        transform.eulerAngles.z = originalTilt + Mathf.Sin( tiltTimer / (numberOfWobbles * 2) ) * tiltAmplifier;

        // See if we've gone through an entire sine wave cycle, reset distance timer if so and do less distance next cycle

        if (timer > Mathf.PI * 2) {

            hitTime = Time.time;

            wobbleDistance *= distanceDecrease;

            wobbleNumber--;

            // If we've done all the wobbles, then stop it with the wobbling already,

            // and make sure we go back to the exact position we started in

            if (!wobbleNumber) {

                isHit = false;

                transform.localPosition.x = originalPosition;

                transform.eulerAngles.z = originalTilt;

            }

        }

    }

}