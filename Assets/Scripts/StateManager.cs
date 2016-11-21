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
    private int winningPlayer;
    private Sled tempSled;
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
    }

    void checkGameOver()
    {
        int finished = 0;
        if (finished != players.Length)
        {
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].GetComponent<Sled>().currLap == numLaps + 1)
                {
                    ////players[i] wins!
                    //winningPlayer = i;
                    finished += 1;
                    players[i].GetComponent<Sled>().time = Time.time;
                    //Disabling the script may allow the sled to stay, but prevent further input.
                    players[i].GetComponent<Sled>().enabled = false;
                }
            }
        }
        if (finished == players.Length)
        {
            currState = State.GameOver;
            for (int i = 0; i < players.Length; i++)
            {
                if (tempSled.time > players[i].GetComponent<Sled>().time)
                {
                    tempSled.time = players[i].GetComponent<Sled>().time;
                    winningPlayer = i;
                }
            }
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
                canvasTxt.text = "Player " + (winningPlayer + 1) + " Wins!\n" + (int)(tempSled.time / 60) +
                    ":" + ((tempSled.time % 60)).ToString("n2") + "\nPress BACK to Quit. Press START to try again!\n";
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
