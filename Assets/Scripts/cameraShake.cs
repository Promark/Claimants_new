using UnityEngine;
using System.Collections;
using URnd=UnityEngine.Random;

public class cameraShake : MonoBehaviour
{
   private Vector3 originPosition;
   private Quaternion originRotation;
   public float shake_decay;
   public float shake_intensity;
 
   
     
 
   void Update ()
	{
		if (Input.GetKey(KeyCode.Q))
            Shake ();
        
		
      if (shake_intensity > 0)
	  {
         transform.position = originPosition + URnd.insideUnitSphere * shake_intensity;
         transform.rotation = new Quaternion(
         originRotation.x + URnd.Range (-shake_intensity,shake_intensity) * .2f,
         originRotation.y + URnd.Range (-shake_intensity,shake_intensity) * .2f,
         originRotation.z + URnd.Range (-shake_intensity,shake_intensity) * .2f,
         originRotation.w + URnd.Range (-shake_intensity,shake_intensity) * .2f);
         shake_intensity -= shake_decay;
      }
   }
 
   void Shake()
	{
      originPosition = transform.position;
      originRotation = transform.rotation;
      shake_intensity = .3f;
   shake_decay = 0.002f;
   }
}
