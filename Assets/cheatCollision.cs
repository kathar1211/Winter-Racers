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
                cheatRef = false;
            }
            else
            {
                cheatRef = true;
            }
        }
    }
}
