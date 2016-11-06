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

    public GameObject boostBar1;
    public GameObject boostBar2;
    public GameObject boostBar3;
    public GameObject boostBar4;


    // Use this for initialization
    void Start () {
		players.Add (player1);
		players.Add (player2);
		players.Add (player3);
		players.Add (player4);
		CreatePlayerList ();

        //assign boost bars
		Instantiate (boostBar1, transform.position, transform.rotation);
		Instantiate (boostBar2, transform.position, transform.rotation);
		//boostBar1.transform.SetParent (Canvas);
		//boostBar2.transform.SetParent (Canvas);
        player1.GetComponent<BoostBar>().CookieMeter = boostBar1;
        player2.GetComponent<BoostBar>().CookieMeter = boostBar2;
    }
	
	// Update is called once per frame
	void Update () {

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
