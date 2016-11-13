using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	List<GameObject> players = new List<GameObject>();
	List<GameObject> boostBars = new List<GameObject> ();

	public GameObject player1;
	public GameObject player2;
	public GameObject player3;
	public GameObject player4;
	int c;//number of controllers

    public GameObject boostBar1;
    public GameObject boostBar2;
    //public GameObject boostBar3;
    //public GameObject boostBar4;


    // Use this for initialization
    void Start () {
		players.Add (player1);
		players.Add (player2);
		players.Add (player3);
		players.Add (player4);
		CreatePlayerList ();

		//boostBars.Add (boostBar1);
		//boostBars.Add (boostBar2);
		//boostBars.Add (boostBar3);
		//boostBars.Add (boostBar4);
		//CreatePlayerCanvas ();

       // player1.GetComponent<BoostBar>().CookieMeter = boostBar1;
       // player2.GetComponent<BoostBar>().CookieMeter = boostBar2;
    }
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < c; ++i) {
			if(players[i].GetComponent<Sled>().EatedCookie){
				//UpdateCookieMeter(i);
			}
		}
	}


	public void CreatePlayerList(){
		c = Input.GetJoystickNames ().Length;
		if (c > 0) {
			Debug.Log("Detected " + c + " controllers");
			for (int i = 0; i < c; ++i){
				Instantiate(players[i], players[i].transform.position, transform.rotation);
				Debug.Log ("should have made a player");
			}
		}
	}
/*
	public void CreatePlayerCanvas(){
		for (int i = 0; i < c; ++i) {
			Instantiate(boostBars[i], boostBars[i].transform.position, transform.rotation);
		}
	}

	//i know this is gross I'm so sorry
	public void UpdateCookieMeter(int i){
			boostBars[i].GetComponent<BoostBar>().MeterWidth = players[i].GetComponent<Sled>().CookieCount * 20;	
	}
*/


}
