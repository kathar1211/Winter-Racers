using UnityEngine;
using System.Collections;

public class finishCollision : MonoBehaviour {

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            bool cheatRef = other.gameObject.GetComponent<Sled>().isCheating;
            if (!cheatRef)
            {
                Debug.Log("Soo... ye?");
                other.gameObject.GetComponent<Sled>().currLap++;
                other.gameObject.GetComponent<Sled>().isCheating = true;
            }
        }
    }
}
