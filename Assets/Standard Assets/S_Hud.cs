using UnityEngine;
using System.Collections.Generic;

public class S_Hud : MonoBehaviour {
	public GameObject Player;
	public float maxHP,curHP,hpBarSize;
	GUIStyle style;
	public Texture2D red;
	//vp_DamageHandler dmg;

	// Use this for initialization
	void Start () {
		
		style = new GUIStyle();
		style.normal.background = red;
	}
	
	// Update is called once per frame
	void Update () {
		Player = GameObject.Find("SimplePlayer");
			//dmg = Player.GetComponent<vp_DamageHandler>();
			//maxHP = dmg.MaxHealth;
			//curHP = dmg.m_CurrentHealth;
			//hpBarSize = Screen.width / 3 / (maxHP/curHP);
		    //dmg.m_CurrentHealth -= .25f;
		if(curHP <= 0){
			//dmg.Die();
			}	
	}
	void OnGUI() {
		
		GUI.Box(new Rect((Screen.width/2) - (hpBarSize/2),10,hpBarSize ,25  ),"",style);		
	}
		
	
}
