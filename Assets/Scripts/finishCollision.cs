using UnityEngine;
using System.Collections;

public class finishCollision : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            bool cheatRef = other.gameObject.GetComponent<Sled>().isCheating;
            if (!cheatRef)
            {
                other.gameObject.GetComponent<Sled>().currLap++;
                cheatRef = true;
            }
        }
    }
}
