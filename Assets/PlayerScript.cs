using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {

	// Use this for initialization
	public AudioClip a1;
	public AudioClip a2;
	public bool entered = false,first = true,ok = false, entered2 = false, letgo = true, mapTrigger = false;
	public S_questLog log;
	public Vector3 worldLocation;
	public enum state
	{
		World,
		Cabin
	};
	public state GameState = new state();
	void Awake(){
		DontDestroyOnLoad(gameObject);
		switch (GameState){
		case state.Cabin:
		
			break;
		}
	}
	void Start () {
		GameState = state.World;


	}

	
	// Update is called once per frame
	void Update () {
		StartCoroutine("GetLog");
		switch (GameState){
		case state.Cabin:
		ok = log.ok;
			break;
		}
	
	}
	void OnTriggerEnter(Collider other){
		switch (GameState){
		case state.Cabin:
		if (other.tag == "Map"){
			mapTrigger = true;
			audio.clip = a2;
			}
			if(other.tag == "Cabin"){
				entered2 = true;
			}
			break;
		case state.World:
		if(other.tag == "Cabin"){
				entered = true;

					
			}			
			break;
		}

	}
	void OnGUI(){
		//if(log.questList[0].questComplete == true){
		{
			//here the ok variable is to see if the player has completed the first quest
			switch (GameState){
			case state.Cabin:
			if (mapTrigger == true && first == true && ok == true){
			GUI.Label(new Rect(10,10,200,50),"Press 'E' to intereact.");
			if(Input.GetKeyDown(KeyCode.E)){
					log.questComplete = true;
					log.completeQuest("Look for clues");
					log.makeQuest("Go to the town","You found on the map that there is a town nearby.\n Make your way there to see if your friends are there.",log.questNumber);

				audio.Play();
					first = false;

					}
			}
				if(entered2 == true){

					GUI.Label(new Rect(10,10,100,50),"Press E to exit");
				
				if(Input.GetKeyDown(KeyCode.E)){

						GameState = state.World;
						setWorldPos();
						letgo = false;
						Application.LoadLevel("level_Zero");


				}
				}
				letgo = true;
				break;
			case state.World:
				if(entered == true)
				{
					GUI.Label(new Rect(10,10, 100,50),"Press E to enter");
					getWorldPos();
					if(Input.GetKeyDown(KeyCode.K))
					{
						letgo = false;
						GameState = state.Cabin;
						getCabinPos();
						audio.clip = a1;
						audio.Play();
						Application.LoadLevel("Shack");

					}

				}
				break;
				}
		
		}
	}
	void OnTriggerExit(Collider other)
	{
		switch(GameState){
		case state.Cabin:
		if(other.tag == "Map"){
			mapTrigger = false;
			}
			if (other.tag == "Cabin"){
				entered2 = false;
			
		}
			break;
		}
	}
	private IEnumerator GetLog(){
		log = GameObject.Find("QuestLog").GetComponent<S_questLog>();
		yield return new WaitForSeconds(5);
	}
	void getCabinPos(){
		Vector3 temp;
		PlayerPrefs.SetFloat("CabinX",259.51f);
		PlayerPrefs.SetFloat("CabinY",76.928f);
		PlayerPrefs.SetFloat("CabinZ",277.97f);
		temp = new Vector3(PlayerPrefs.GetFloat("CabinX"),PlayerPrefs.GetFloat("CabinY"),PlayerPrefs.GetFloat("CabinZ"));
		gameObject.transform.position = temp;
	}
	void getWorldPos(){
		Vector3 temp;
		PlayerPrefs.SetFloat("WorldX",gameObject.transform.position.x);
		PlayerPrefs.SetFloat("WorldY",gameObject.transform.position.y);
		PlayerPrefs.SetFloat("WorldZ",gameObject.transform.position.z);
		worldLocation = new Vector3 (PlayerPrefs.GetFloat("WorldX"),PlayerPrefs.GetFloat("WorldY"),PlayerPrefs.GetFloat("WorldZ"));

	}
	void setWorldPos(){


		gameObject.transform.position = worldLocation;
	}

}
