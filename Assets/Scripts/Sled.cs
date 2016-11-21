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
    public float time = 0.0f;

    public GameObject gSled;
	Rigidbody sled;

	private GameObject item;

	List<GameObject> items = new List<GameObject>();

	bool raceStart = false;

	public GameObject itemSprite;
	public GameObject ThrownSnowball;
	private GameObject s;
	//public Image snowballSprite;//this is for when we have multiple objects


	void Start(){
		//Instantiate (boostBar, boostBar.transform.position, boostBar.transform.rotation);
		itemSprite.SetActive (false);
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
		DriftControl();

		if (Input.GetButton(X) && items.Count == 0) {
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
        if (c.gameObject.tag == "Cookie" && cookieCount < 5)
        {
            eatedCookie = true;
            ++cookieCount;
            c.gameObject.SetActive(false);
            UpdateCookieMeter();
        }
		Debug.Log (c.gameObject.tag.ToString ());
		if (c.gameObject.tag == "Snowball" && items.Count < 1) {
			items.Add(c.gameObject);
			itemSprite.SetActive (true);
			//Debug.Log(itemSprite.GetComponent<Image> ().color.ToString());

			c.gameObject.SetActive(false);
			holdingItem = true;
			item = c.gameObject;
		}

		if (c.gameObject.tag == "ThrownSnowball" && c.gameObject.GetComponent<Snowball>().Thrower != null) {
			if(c.gameObject.GetComponent<Snowball>().Thrower != gSled.tag){
				sled.AddTorque(new Vector3(0, 0, -3 * x * (drag / Time.deltaTime)));
				sled.velocity = new Vector3(0, 0,0);
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
		//not doing this for this milestone probably

	}

	public void HitBySnowball(){
		sled.AddTorque(new Vector3(0, 0, -1 * x * (drag / Time.deltaTime)));

	}

	public void ThrowSnowball(){
		item.GetComponent<Snowball> ().StartPos = transform.position;
		item.GetComponent<Snowball> ().Dir = transform.up;
		item.GetComponent<Snowball> ().Thrower = gSled.name;
		Debug.Log ("sled name: " + gSled.name);
		item.GetComponent<Snowball> ().Thrown = true;
		Debug.Log (item.GetComponent<Snowball> ().Thrown);
		s = (GameObject)Instantiate(ThrownSnowball, transform.position, Quaternion.identity); 
		items.RemoveAt (0);
		itemSprite.SetActive (false);
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

