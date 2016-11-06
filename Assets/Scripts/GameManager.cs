using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	List<GameObject> players = new List<GameObject>();
	public GameObject player1;
	public GameObject player2;
	public GameObject player3;
	public GameObject player4;
	int c;//number of controllers
	

	// Use this for initialization
	void Start () {
		players.Add (player1);
		players.Add (player2);
		players.Add (player3);
		players.Add (player4);
		CreatePlayerList ();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButton("A")){
			Debug.Log ("A");
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

}
