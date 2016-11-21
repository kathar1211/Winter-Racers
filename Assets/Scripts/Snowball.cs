using UnityEngine;
using System.Collections;

public class Snowball : MonoBehaviour {

	public float range;
	public float force;

	private string thrower;

	private Vector3 startPos;
	private Vector2 pos;
	private Vector2 dir;
	private float distance;

	public GameObject g_snowball;
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

	public Vector2 Dir{
		set{ dir = value;}
	}
	
	// Use this for initialization
	void Start () {
		snowball = g_snowball.GetComponent<Rigidbody> ();

	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (transform.position);
		if (g_snowball.tag == "ThrownSnowball") {
			Threw ();
			///Debug.Log (thrown);
			pos = transform.position;
		}
	}

	public void ThrowingPlayer(){

	}

	public void Threw(){
		//snowball.AddForce (dir * Time.deltaTime);
		//Debug.Log (dir);
		//distance = Vector2.Distance (startPos, pos);
		//if (distance > range) {
		//	thrown = false;
		//	Debug.Log ("Distance: " + distance);
		//	Destroy(g_snowball);
		//}
	}


}
