using UnityEngine;
using System.Collections;

public class cheatCollision : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            bool cheatRef = other.gameObject.GetComponent<Sled>().isCheating;
            if (cheatRef == true)
            {
                other.gameObject.GetComponent<Sled>().isCheating = false;
            }
            else
            {
                other.gameObject.GetComponent<Sled>().isCheating = true;
            }
        }
    }
}
