using UnityEngine;
using System.Collections;

public class ItemManager : MonoBehaviour {

    public GameObject[] cookies;
    private float[] respawnTimers;

    // Starts by finding all items with the cookie tag, this way if we have other levels we don't have to manually add them to an array
    void Start()
    {
        cookies = GameObject.FindGameObjectsWithTag("Cookie");
        respawnTimers = new float[cookies.Length];
        for (int i = 0; i < respawnTimers.Length; i++)
        {
            respawnTimers[i] = 0;
        }
    }

    // Update constantly checks the state of the cookie to see if it becomes unactive
    void Update()
    {
        for (int i = 0; i < cookies.Length; i++)
        {
            respawnTimers[i] = setCookieState(cookies[i], respawnTimers[i]);
        }
    }

    // Once a cookie is not active, the timer starts for it and then it respawns once it reaches that time. 
    public float setCookieState(GameObject item, float respawnTimer)
    {
        if (item.activeSelf == false)
        {
            respawnTimer += 0.05f;
            if (respawnTimer >= 10.0f)
            {
                respawnTimer = 0;
                item.SetActive(true);
            }
        }
        return respawnTimer;
    }
}
