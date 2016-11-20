﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Sled : MonoBehaviour {

	public GameObject boostBar;
	public Vector2 startPos;

	public string RTB;
	public string LTB;
	public string Horizontal;
	public string A;

	private int cookieCount;
	private bool eatedCookie;
	private bool holdingItem = false;

	float x;

	float deadzone = 0.2f;
	float maxSpeed = 2.0f;
	float speed = 0.0f;
	float acceleration = 0.005f;
	float drag = .02f;

	Vector2 dir;

    float boostTimer = 5.0f;
    bool usingBoost = false;

    public bool isCheating = false;
    public int currLap = 0;

    public GameObject gSled;
	Rigidbody sled;

	List<GameObject> items = new List<GameObject>();

	bool raceStart = false;

	void Start(){
		//Instantiate (boostBar, boostBar.transform.position, boostBar.transform.rotation);
	}

	// Update is called once per frame
	void Update () {
		if (sled == null) {
			sled = gSled.GetComponent<Rigidbody>();
		}

		if (raceStart)
			GetInput ();

		if (holdingItem) {
			DisplayItemHeld();
		}

	}//updoot

	public void GetInput(){
		x = Input.GetAxis (Horizontal);
		if(Input.GetAxis(RTB) != 0) {
			
			if(x !=0 ){
				Turn();
			}
			
			Boost ();			
			Move ();
		}
		
		//backing up
		if (Input.GetAxis(LTB)!= 0)
		{
			if (x != 0)
			{
				Turn();
			}           
			MoveBackwards();
		}
		DriftControl();
	}

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Cookie" && cookieCount < 5)
        {
            eatedCookie = true;
            ++cookieCount;
            c.gameObject.SetActive(false);
            UpdateCookieMeter();
        }

		if (c.gameObject.tag == "Snowball" && items.Count < 2) {
			items.Add(c.gameObject);
			c.gameObject.SetActive(false);
			holdingItem = true;
		}

    }

	public void UpdateCookieMeter(){
		boostBar.GetComponent<BoostBar>().MeterWidth = cookieCount * 20;
	}

	public void Turn(){
		Vector2 tmp = new Vector2 (x, 0);
		Vector2 currentUp = transform.up;
		if (tmp.sqrMagnitude > deadzone) {
			dir = tmp.normalized;
		}
        //x is backwards to how we want it for some reason. 
        //sled.AddTorque(-1 * x * (drag / Time.deltaTime));
        sled.AddTorque(new Vector3(0, 0, -1 * x * (drag / Time.deltaTime)));
		LimitVelocity();
	}

	public void Move(){
		sled.AddForce(transform.up);
		LimitVelocity();
	}

    public void MoveBackwards()
    {
        sled.AddForce(transform.up * -1);
        LimitVelocity();
    }

    public void LimitVelocity(){
		if (sled.velocity.magnitude > maxSpeed || sled.velocity.magnitude < -1 * maxSpeed) {
			sled.velocity = sled.velocity.normalized;
			sled.velocity *= maxSpeed;
		}
	}

	public void DriftControl(){
		sled.AddForce (-1 * Vector3.Dot (sled.velocity, transform.right) * transform.right, ForceMode.Impulse);
	}

    public void Boost()
    {
        if (boostBar != null && boostBar.GetComponent<BoostBar>().BoostReady())
        {
            boostTimer = 3.0f;
            if (Input.GetButton(A))
            {
                usingBoost = true;
                boostBar.GetComponent<BoostBar>().ResetMeter();
                cookieCount = 0;
                Debug.Log("Boostin'");
            }
        }

        if (usingBoost)
        {
            if (boostTimer > 0)
            {
                //increase maxspeed and acceleration while boost active
                maxSpeed = 79999.0f;
                acceleration = 6.0f;
				sled.AddForce (transform.up*acceleration);
                //Debug.Log(boostTimer);
            }
            else
            {
                //end boost, reset original values
                usingBoost = false;
                maxSpeed = 2.0f;
                acceleration = 0.005f;
                Debug.Log("End Boost");
                
            }
            boostTimer -= Time.deltaTime;
        }
    }

    public void CheatCheck()
    {

    }

	public void DisplayItemHeld(){

	}

	public void DisplayItemRange(int range){

	}

	public void ThrowSnowball(){

	}

	//Properties for cookie eating accessed by GameManager
	public int CookieCount{
		get{ return cookieCount;}
		set{ cookieCount = value;}
	}

	public bool EatedCookie{
		get { return eatedCookie;}
		set { eatedCookie = value;}
	}

	public bool RaceStart{
		get { return raceStart;}
		set { raceStart = value;}
	}
}

