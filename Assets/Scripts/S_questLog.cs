using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class S_questLog : MonoBehaviour
{	
		public string assHole;
		public bool quest1Started = false, quest2Started = false, questOpened = false, questClicked = false, questComplete = false, quest2Complete = false, newQuest = false, go = false, ok = false;
		float qHeight = 90, qWidth = 100, qXpos = 10, qYpos = 10;
		public GameObject player, enemy;
		public 	vp_FPInput input;
		string page = "Page 1", questInfo = string.Empty;
		public vp_SimpleInventory inv;
		public objectiveList olist;
		public GameObject[] weapons;
		public float questNumber = 0;
		public float timer = 3, timer1 = 3;
		public List<Quest> questList = new List<Quest> ();
		public Quest test;
		public Dictionary<string,Quest> questLog = new Dictionary<string, Quest>();

		

		// Use this for initialization
		void Start ()
		{
				player = GameObject.Find ("player 1");
				input = player.GetComponent<vp_FPInput> ();
				inv = player.GetComponent<vp_SimpleInventory> ();
				quest1Started = true;
				makeQuest ("Find a weapon!", "Arm yourself", questNumber);

			
		
		}
	
		// Update is called once per frame
		void Update ()
	{		questNumber = questList.Count;
				if (newQuest) {
		
						timer -= Time.deltaTime;
						if (timer >= 0) {
								newQuest = true;
						} else {
								
								timer = 3;
				newQuest = false;
						}
				}
				if (questComplete) {
						timer1 -= Time.deltaTime;
						if (timer1 >= 0) {
								questComplete = true;
						} else {								
								timer1 = 3;								
								questComplete = false;					
			
						}
				}
				
						if (findQuest("Find a weapon!").questComplete == false) {
								if (inv.HaveItem ("2Revolver")) {
										completeQuest ("Find a weapon!");
										makeQuest ("Look for clues", "Search the shack for clues as to where \n your friends are", questNumber);
				ok = true;
										
								


								}
						
				}


				enemy = GameObject.Find ("ActionHeroine");
				if (!enemy) {
						//quest2Complete = true;
				} else {
						quest2Complete = false;
				}

		}

		public void OnGUI ()
		{
				if (questOpened) {
						GUI.Box (new Rect (qXpos, qYpos, qWidth + 300, qHeight + 410), "");
						GUI.Box (new Rect (qXpos, qYpos, qWidth + 300, 60), "");
						GUI.Box (new Rect (qXpos, qYpos, qWidth + 300, 30), "Quest Log");
						GUI.Label (new Rect (qXpos + 5, qYpos + 70, qWidth, 20), "Quests");
						//GUI.Label (new Rect (qXpos + 361, qYpos + 70, qWidth, 20), "Status");
						GUI.Label (new Rect (qXpos + 185, 45, qWidth, 20), page);
						if (GUI.Button (new Rect (385, 15, 20, 20), "X")) {

								questOpened = false;
								input.ForceCursor = false;
						}

 

//Pages 1 till 2

						if (GUI.Button (new Rect (qXpos + 5, qYpos + 35, 60, 20), "Previous")) {
								if (page == "Page 1") {
								}

								if (page == "Page 2") {
										page = "Page 1";
								}
						}

						if (GUI.Button (new Rect (qXpos + 335, qYpos + 35, 60, 20), "Next")) {
								if (page == "Page 2") {
								}

								if (page == "Page 1") {
										page = "Page 2";
								}
						}
				}
//Quest Log Open Button
				//if (GUI.Button (new Rect (5, 700, 70, 40), "Quest Log")) {
				if (Input.GetKeyDown (KeyCode.Tab)) {						
						input.ForceCursor = true;
						questOpened = true;
				}

				
			
				//----------------QUESTS----------------
				if (questClicked == true) {
//Window
						GUI.Box (new Rect (qXpos, qYpos, qWidth + 300, qHeight + 410), "");
						GUI.Box (new Rect (qXpos, qYpos, qWidth + 300, 60), "");
						GUI.Box (new Rect (qXpos, qYpos, qWidth + 300, 500), questInfo);
//Close Button
						if (GUI.Button (new Rect (385, 15, 20, 20), "X")) {
								questClicked = false;
								questOpened = true;
						}
				}
				if (page == "Page 1") {	

						if (questOpened == true) {
								foreach (KeyValuePair<string, Quest> q in questLog) {
										if (q.Value.questComplete == false) {
												if (GUI.Button (new Rect (qXpos + 2.5f, qYpos + (4 * (17 + (q.Value.questNumber * 5.5f))), 395f, 20f), q.Value.questName)) {
														questOpened = false;
														questClicked = true;
														questInfo = q.Value.questDetail;
												}
										}
								}
						}
				}
				if (questComplete) {
						GUI.Label (new Rect (10, 10, 150, 50), "Quest Complete!");
				}
				if (newQuest) {


						GUI.Label (new Rect (10, 25, 150, 50), "New quest acquired");

				}
				

		}

		public void makeQuest (string qname, string detail, float Number)
		{
			
				
						Quest q = new Quest (qname, detail,Number);						
						//questList.Add (q);
						questLog.Add(qname,q);

						newQuest = true;
						q.pickedUp = true;
						
		}

		public Quest findQuest (string qname)
		{
		Quest temp = null;
		if(questLog.TryGetValue(qname, out temp)){
			return questLog[qname];
		}
		else
		{
			print("quest does not exist");
			return null;
		}
		return null;
		}

		public void completeQuest (string questName)
		{		
		Quest q = findQuest(questName);
		if(q.questComplete == false){	
			questComplete = true;
			q.questComplete = true;
				
						
				}
				
		}
}

