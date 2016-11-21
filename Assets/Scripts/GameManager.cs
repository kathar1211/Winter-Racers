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
    public GameObject boostBar3;
    public GameObject boostBar4;

	public GameObject countdown;

	public Sprite one;
	public Sprite two;
	public Sprite three;
	public Sprite go;

	private float countdownTimer = 3.0f;
	private bool counted = false;

    // Use this for initialization
    void Start ()
    {
        players.Add (player1);
		players.Add (player2);
		players.Add (player3);
		players.Add (player4);

		boostBars.Add (boostBar1);
		boostBars.Add (boostBar2);
		boostBars.Add (boostBar3);
		boostBars.Add (boostBar4);

        CreatePlayerList();

    }
	
	// Update is called once per frame
	void Update () {
		if (GetComponent<StateManager> ().GameStart && counted == false) {
		
			ItsTheFinalCountdown ();
		}
	}

	public void ItsTheFinalCountdown(){

		if (countdownTimer <= 3.0f && countdownTimer >= -1.0f) {
			countdownTimer -= 0.05f;

		} 
		 if (countdownTimer >= 2.0f) {
			countdown.GetComponent<SpriteRenderer>().sprite = three;
			
		} 
		else if (countdownTimer >= 1.0f) {
			countdown.GetComponent<SpriteRenderer>().sprite = two;
		} 
		else if (countdownTimer >= 0.0f) {
			countdown.GetComponent<SpriteRenderer>().sprite = one;
		} 
		else {
			countdown.GetComponent<SpriteRenderer>().sprite = go;
			for (int i = 0; i < c; ++i) {
				players[i].GetComponent<Sled>().RaceStart = true;		
			}
		}

		 if (countdownTimer <= -1.0f) {
			countdown.SetActive(false);
			counted = true;
		}
	}

	public void CreatePlayerList(){
		c = Input.GetJoystickNames ().Length;
		if (c > 0) {
			Debug.Log("Detected " + c + " controllers");
			for (int i = 0; i < c; ++i){
                players[i] = (GameObject)Instantiate(players[i], players[i].transform.position, players[i].transform.rotation);
                Debug.Log("should have made a player");
               
                //assign boostbar and make it visible
                players[i].GetComponent<Sled>().boostBar = boostBars[i];                
                boostBars[i].GetComponent<BoostBar>().sled = players[i];
                boostBars[i].GetComponent<BoostBar>().Activate();
            }
		}
	}

}
