﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class StateManager : MonoBehaviour {

    enum State
    {
        Menu,
        Instruction,
        Game,
        Pause,
        GameOver 
    }

    private State currState;
    private State prevState;
    private GameObject canvas;
    private Color white;
    private int instructIndex;
    public Text canvasTxt;
    public Image canvasImg;
    public Sprite[] menuBackgrounds;

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
                canvasImg.sprite = menuBackgrounds[0];
                canvasImg.color = white;
                canvasTxt.fontSize = 36;
                canvasTxt.color = Color.black;
                canvasTxt.text = "";
                break;

            case State.Instruction:
                canvasImg.sprite = menuBackgrounds[instructIndex];
                canvasImg.color = white;
                canvasTxt.fontSize = 22;
                canvasTxt.text = "";
                break;

            case State.Game:
                //Hide Canvas
                canvasImg.color = Color.clear;
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
                canvasImg.color = white;
                //if ()
                //{
                //    canvasImg.sprite = menuBackgrounds[4];
                //}
                canvasTxt.alignment = TextAnchor.LowerCenter;
                canvasTxt.fontSize = 18;
                canvasTxt.text = "Press BACK to Quit. Press START to try again!\n";
                break;
        }
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
                {
                    setScreen(currState);
                    //Check Input
                    if (Input.GetButton("7"))
                    {
                        //Continue!
                        if (instructIndex < 3)
                        {
                            instructIndex++;
                        }
                        else
                        { 
                            currState = State.Game;
                        }
                    }
                }
                break;

            case State.Game:
                setScreen(currState);
                //Check Input
                if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
                {
                    //Pause the Game!
                    currState = State.Pause;
                }
                //Check GameOver
                //if ()
                //{
                //    //Did they lose or win?
                //    currState = State.GameOver;
                //}
                break;

            case State.Pause:
                setScreen(currState);
                //Check Input
                if (Input.GetKeyDown(KeyCode.P))
                {
                    //Unpause the Game!
                    currState = State.Game;
                }
                else if(Input.GetKeyDown(KeyCode.Escape))
                {
                    //Quit the Game!
                    Application.Quit();
                }
                break;

            case State.GameOver:
                setScreen(currState);
                //Check Input
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    //Quit the Game!
                    Application.Quit();
                }
                else if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
                {
                    //Restart the Game!
                    Application.LoadLevel("WinterRacers");
                }
                break;
        }
	}
}
