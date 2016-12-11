using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Sled : MonoBehaviour {

	public GameObject boostBar;
	public Vector2 startPos;

	public string RTB;
	public string LTB;
	public string Horizontal;
	public string A;
	public string X;

	private int cookieCount;
	private bool eatedCookie;
	private bool holdingItem = false;

	public bool HoldingItem{
		get{ return holdingItem;}
		set{ holdingItem = value;}
	}

	float x;

	float deadzone = 0.2f;
	float maxSpeed = 2.0f;
	float speed = 0.0f;
	float acceleration = 0.005f;
	float drag = .02f;

	Vector2 dir;

    float boostTimer = 5.0f;
    bool usingBoost = false;

	private bool hit;
	private float hitTimer = 0.0f;

    public bool isCheating = false;
    public int currLap = 0;
    public float time = 0.0f;
    public bool finished = false;

    public GameObject gSled;
	Rigidbody sled;

	private GameObject item;

	List<GameObject> items = new List<GameObject>();

	bool raceStart = false;

	//public Image snowballSprite;
	public GameObject gThrownSnowball;
	private GameObject s;
	//public Image snowballSprite;//this is for when we have multiple objects


	void Start(){

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

		if (hit) {
			if(hitTimer >= 2.0f){
				hitTimer = 0.0f;
				hit = false;
				sled.drag = 1;
				sled.angularDrag = 1;
			}
			else{
				hitTimer += Time.deltaTime;
				sled.AddTorque(new Vector3(0, 0, -18 * x));
				sled.drag = 0;
				sled.angularDrag = 0;
				sled.velocity = new Vector3(0, 0,0);
			}
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
		DriftControl();

		if (Input.GetButton(X) && items.Count > 0) {
			//Debug.Log("throw balls for what");
			//snowballSprite.GetComponent<Image>().enabled = false;
			ThrowSnowball();
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
	}

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Cookie" && cookieCount < 5 && !usingBoost)
        {
            eatedCookie = true;
            ++cookieCount;
            c.gameObject.SetActive(false);
            UpdateCookieMeter();
        }
		if (c.gameObject.tag == "Snowball" && items.Count < 1) {
			items.Add(c.gameObject);
			//snowballSprite.GetComponent<Image>().enabled = true;
			//rangeDebug.Log ("Distance: " + distance);Debug.Log("ayyy lmao");
			Debug.Log ("hit a snowball.");
			c.gameObject.SetActive(false);
			holdingItem = true;
			item = c.gameObject;

		}

		if (c.gameObject.tag == "ThrownSnowball" ) {
			if(c.gameObject.GetComponent<Snowball>().throwName != gSled.name){
				hit = true;

				Debug.Log ("Player: " + gSled.name);
				Debug.Log ("Snowball: " + c.gameObject.GetComponent<Snowball>().Thrower);
				Destroy(c.gameObject);
			}
			holdingItem = false;

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
                //Debug.Log("Boostin'");
            }
        }

        if (usingBoost)
        {
            if (boostTimer > 0)
            {
                //increase maxspeed and acceleration while boost active
                maxSpeed = 8.0f;
                acceleration = 5.0f;
				sled.AddForce (transform.up*acceleration);
                //Debug.Log(boostTimer);
            }
            else
            {
                //end boost, reset original values
                usingBoost = false;
                maxSpeed = 2.0f;
                acceleration = 0.005f;
                //Debug.Log("End Boost");
                
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
		//not doing this for this milestone probably

	}

	public void HitBySnowball(){
		sled.AddTorque(new Vector3(0, 0, -1 * x * (drag / Time.deltaTime)));

	}

	public void ThrowSnowball(){
		s = (GameObject)Instantiate(gThrownSnowball, transform.position, gSled.transform.rotation); 
		s.GetComponent<Snowball>().SetTheFuckingDirection (transform.up, gSled.name);
		//s.GetComponent<Snowball> ().Dir = transform.up;
		//s.GetComponent<Snowball> ().Thrower = gSled.name;;
		//s.GetComponent<Snowball> ().Thrown = true;
		
		items.RemoveAt (0);
		Debug.Log (items.Count);
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

