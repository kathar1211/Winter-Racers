using UnityEngine;
using System.Collections;

public class Sled : MonoBehaviour {
	float x;

	float deadzone = 0.2f;
	float maxSpeed = 2f;
	float speed = 0.0f;
	float acceleration = 0.005f;
	float drag = 0.002f;

	Vector2 dir;

	public GameObject gSled;
	public Sled(){
	}
	
	// Update is called once per frame
	void Update () {
		x = Input.GetAxis ("Horizontal");

		if(Input.GetButton("A")) {
			//'Debug.Log("A");
			if(x !=0 ){
				Turn();
			}
			Move ();
		}
	}

	public void Turn(){
		Debug.Log ("TUrn");
		Vector2 tmp = new Vector2 (x, 0);
		Vector2 currentUp = transform.up;

		Debug.Log (currentUp);

		if (tmp.sqrMagnitude > deadzone) {
			dir = tmp.normalized;
		}
		gSled.GetComponent<Rigidbody2D> ().AddTorque (-1 * x);
		//transform.Rotate(new Vector3(0, 0,  * Time.deltaTime * 60));

	}

	public void Move(){
		Debug.Log ("Move");
		speed += acceleration * Time.deltaTime;
		if (gSled.GetComponent<Rigidbody2D> ().velocity.magnitude > maxSpeed) {

			gSled.GetComponent<Rigidbody2D> ().velocity.Normalize ();
			gSled.GetComponent<Rigidbody2D> ().velocity *= maxSpeed;
		}
		gSled.GetComponent<Rigidbody2D> ().AddForce (transform.up);
	}

	public void Drift(){

	}

}

