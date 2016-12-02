using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class StateManager : MonoBehaviour {

    enum State
    {
        Menu,
        Instruction,
        Game,
        GameOver 
    }

    private State currState;
    private State prevState;
    private GameObject canvas;
    private int instructIndex;
    private GameObject[] players;
    private int[] playerOrder;
    private Sled tempSled;
    private int numFinished;
    private Text[] playerLaps;
    public Text canvasTxt;  //Menu txt
    public Image canvasImg; //Menu img
    public Sprite[] menuBackgrounds;
    public int numLaps;

	private bool gameStart = false;
	public bool GameStart{
		get{ return gameStart;}
	}


    //To retrieve players once the game starts
    void getPlayers()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        playerOrder = new int[players.Length];
        playerLaps = new Text[players.Length];
        playerLaps[0] = GameObject.Find("P1cookieTxt").GetComponent<Text>();
        if(players.Length > 1)
        {
            playerLaps[1] = GameObject.Find("P2cookieTxt").GetComponent<Text>();
        }
}

    void updateLap()
    {
        for(int i = 0; i < players.Length; i++)
        {
            playerLaps[i].text = "P" + (i+1) + " Cookiometer        " + (players[i].GetComponent<Sled>().currLap) + "/" + numLaps;
        }
    }

    void checkGameOver()
    {
        if (numFinished != players.Length)
        {
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].GetComponent<Sled>().currLap == (numLaps + 1) && !players[i].GetComponent<Sled>().finished)
                {
                    ////players[i] wins!
                    //winningPlayer = i;
                    numFinished += 1;
                    players[i].GetComponent<Sled>().time = Time.timeSinceLevelLoad;
                    //Disabling the script may allow the sled to stay, but prevent further input.
                    players[i].GetComponent<Sled>().enabled = false;
                    players[i].GetComponent<Sled>().finished = true;
                }
            }
        }
        else //(numFinished == players.Length)
        {
            for (int j = 0; j < players.Length; j++)
            {
                if (tempSled.time > players[j].GetComponent<Sled>().time)
                {
                    tempSled.time = players[j].GetComponent<Sled>().time;
                    playerOrder[0] = j;
                }
                else
                {
                    playerOrder[1] = j;
                }
            }
            currState = State.GameOver;
        }
    }

    void setScreen(State s)
    {
        if (prevState == s && s != State.Instruction)
        {
            return;
        }
        prevState = s;

        switch (s)
        {
            case State.Menu:
                canvasImg.enabled = true;
                canvasImg.sprite = menuBackgrounds[0];
                canvasImg.color = Color.white;
                canvasTxt.fontSize = 36;
                canvasTxt.color = Color.black;
                canvasTxt.text = "";
                break;

            case State.Instruction:
                canvasImg.sprite = menuBackgrounds[instructIndex];
                canvasImg.color = Color.white;
                canvasTxt.fontSize = 22;
                canvasTxt.text = "";
                break;

            case State.Game:
                //Get Players
                getPlayers();
                //Hide Canvas
                canvasImg.color = Color.clear;
				gameStart = true;
                //Show UI

                //Continue Game
                Time.timeScale = 1f;
                break;

            case State.GameOver:
                //End game processes
                setScreen(currState);
                //Pause Game
                Time.timeScale = 0f;
                //Display Canvas
                canvas.SetActive(true);
                canvasImg.color = Color.clear;
                canvasTxt.alignment = TextAnchor.LowerCenter;
                canvasTxt.fontSize = 18;
                if (players.Length > 1)
                {
                    canvasTxt.text = "Player " + (playerOrder[0] + 1) + " Wins!\nTime: " + (int)(players[playerOrder[0]].GetComponent<Sled>().time / 60) +
                        ":" + ((tempSled.time % 60)).ToString("n2") + "\n\nPlayer " + (playerOrder[1] + 1) + "\nTime: " + (int)(players[playerOrder[1]].GetComponent<Sled>().time / 60) +
                        ":" + ((tempSled.time % 60)).ToString("n2") + "\n\nPress BACK to Quit. Press START to try again!\n";
                }
                else
                {
                    canvasTxt.text = "Player " + (playerOrder[0] + 1) + "\nTime: " + (int)(players[playerOrder[0]].GetComponent<Sled>().time / 60) +
                        ":" + ((tempSled.time % 60)).ToString("n2") + "\n\n\nPress BACK to Quit. Press START to try again!\n";
                }
                break;
        }
    }

    // Use this for initialization
    void Start ()
    {
        //Set Variables
        currState = State.Menu;
        prevState = State.GameOver;
        canvas = GameObject.Find("Canvas");
        instructIndex = 1;

        //Prep
        canvas.SetActive(true);
        Time.timeScale = 0;
        tempSled = new Sled();
        tempSled.time = float.MaxValue;
        numFinished = 0;
    }

    // Update is called once per frame
    void Update ()
    {
	    switch (currState)
        {
            case State.Menu:
                setScreen(currState);
                if(Input.anyKeyDown)
                {
                    //Continue!
                    currState = State.Instruction;
                }
                break;

            case State.Instruction:
                    setScreen(currState);
                    //Check Input
                    if (Input.GetButtonDown("Submit"))
                    {
                        //Continue!
                        if (instructIndex < 2)
                        {
                            instructIndex++;
                        }
                        else
                        { 
                            currState = State.Game;
                        }
                    }
                break;

            case State.Game:
                setScreen(currState);
                //Check GameOver
                checkGameOver();
                updateLap();
                break;

            case State.GameOver:
                setScreen(currState);
                //Check Input
                if (Input.GetButtonDown("Quit"))
                {
                    //Quit the Game!
                    Application.Quit();
                }
                else if (Input.GetButtonDown("Start"))
                {
                    //Restart the Game!
                    Application.LoadLevel("WinterRacers");
                }
                break;
        }
	}
}
