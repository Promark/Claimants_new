using UnityEngine;
using System.Collections;

public class mapCollider : MonoBehaviour
{
		S_questLog log;
		bool entered = false, first = true;
		// Use this for initialization
		void Start ()
		{

		}
	
		// Update is called once per frame
		void Update ()
		{
				if (entered) {
						log = GameObject.Find ("QuestLog").GetComponent<S_questLog> ();
						{
								if (log.questList [0].questComplete == true) {
										if (Input.GetKeyDown (KeyCode.E)) {
												log.makeQuest ("Go to the town", "You found on the map that there is a town nearby. Make your way there to see if your friends are there.", log.questNumber);
										}
								}
						}
				}
	
		}

		public void OnTriggerEnter (Collider other)
		{
				entered = true;
		}

}
