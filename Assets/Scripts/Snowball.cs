using UnityEngine;
using System.Collections;

public class Snowball : MonoBehaviour {

	private float range;
	private string thrower;

	private Vector3 startPos;
	private Vector2 pos;
	public Vector3 dir;
	private float distance;
	public Vector3 testVector;
	public string throwName;
	
	//public GameObject g_snowball;
	Rigidbody snowball;

	private bool thrown = false;

	public bool Thrown{
		get{ return thrown;}
		set{ thrown = value;}
	}

	public string Thrower{
		get{ return thrower;}
		set{ thrower = value;}
	}

	public Vector3 StartPos{
		set{ startPos = value;}
		get{ return startPos;}
	}

	public Vector2 Pos{
		set{ pos = value;}
	}

	public Vector3 Dir{
		set{ dir = value;}
		get{ return dir;}
	}
	
	// Use this for initialization
	void Start () {
		snowball = this.gameObject.GetComponent<Rigidbody> ();
		startPos = transform.position;
		dir = new Vector3 (0.0f, 0.0f, 0.0f);
		range = 5;
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (this.gameObject.tag);
		if (this.tag == "ThrownSnowball") {
			Threw ();
			//Debug.Log ("walk into the sled like whaddup");
			//pos = transform.position;
		}
	}

	public void ThrowingPlayer(){

	}

	public void Threw(){
		snowball.AddForce (transform.up * 7);

		distance = Vector2.Distance (startPos, transform.position);

		Debug.Log ("Distance: " + distance);
		Debug.Log ("Range: " + range);
		Debug.Log ("dir: " + dir);
		Debug.Log ("startpos: " + startPos);
		Debug.Log (transform.position);

		Debug.Log ("thrower name: " + thrower);


		if (distance > range) {
			thrown = false;
			Debug.Log ("DIE DIE DIE DIE");
			Destroy(this.gameObject);
		}
	}

	public void SetTheFuckingDirection(Vector3 fuckingDirection, string name){
		testVector = fuckingDirection;
		throwName = name;
		Debug.Log (fuckingDirection);
		float x = fuckingDirection.x;
		Debug.Log ("x " + x);
		dir.x = fuckingDirection.x;
		dir.y = fuckingDirection.y;
		dir.z = fuckingDirection.z;
		Debug.Log("set teh fukkin direction");
		Debug.Log ("Dir set: " + dir);
	}


}
