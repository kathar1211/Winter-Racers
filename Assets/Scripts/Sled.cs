using UnityEngine;
using System.Collections;

public class Sled : MonoBehaviour {

	public Vector2 startPos;

	public string RTB;
	public string LTB;
	public string Horizontal;
	public string A;

	float x;

	float deadzone = 0.2f;
	float maxSpeed = 2.0f;
	float speed = 0.0f;
	float acceleration = 0.005f;
	float drag = .02f;

	Vector2 dir;

    float boostTimer = 5.0f;
    bool usingBoost = false;

    public GameObject gSled;
	Rigidbody2D sled;

	// Update is called once per frame
	void Update () {
		if (sled == null) {
			sled = gSled.GetComponent<Rigidbody2D>();
		}

		x = Input.GetAxis (Horizontal);
		if(Input.GetAxis(RTB) != 0) {
			Debug.Log("HELLO");
			if(x !=0 ){
				Turn();
			}
			if(Input.GetButton(A)){
			   Boost ();
			}
			Move ();
		}
		DriftControl ();
	}

	public void Turn(){
		Vector2 tmp = new Vector2 (x, 0);
		Vector2 currentUp = transform.up;
		if (tmp.sqrMagnitude > deadzone) {
			dir = tmp.normalized;
		}
		//x is backwards to how we want it for some reason. 
		sled.AddTorque (-1 * x * (drag / Time.deltaTime));

		LimitVelocity ();
	}

	public void Move(){
		//speed += acceleration * Time.deltaTime;
		sled.AddForce (transform.up);
		LimitVelocity ();
	}

	public void LimitVelocity(){
		if (sled.velocity.magnitude > maxSpeed || sled.velocity.magnitude < -1 * maxSpeed) {
			sled.velocity = sled.velocity.normalized;
			Debug.Log (sled.velocity);
			sled.velocity *= maxSpeed;
		}
	}

	public void DriftControl(){
		sled.AddForce (-1 * Vector3.Dot (sled.velocity, transform.right) * transform.right, ForceMode2D.Impulse);
	}

    public void Boost()
    {
        if (GetComponent<BoostBar>().BoostReady() == true)
        {
            boostTimer = 5.0f;
            if (Input.GetButton(A))
            {
                usingBoost = true;
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

}

