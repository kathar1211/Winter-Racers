using UnityEngine;
using System.Collections;

public class ItemManager : MonoBehaviour {

    public GameObject[] cookies;

    private float respawnTimer = 0;
	private float itemTimer = 0.0f;


	public GameObject[] snowballs;

    private float[] respawnTimers;
    private float[] snowballTimers;

    // Starts by finding all items with the cookie tag, this way if we have other levels we don't have to manually add them to an array
    void Start()
    {
        cookies = GameObject.FindGameObjectsWithTag("Cookie");

		snowballs = GameObject.FindGameObjectsWithTag ("Snowball");

        respawnTimers = new float[cookies.Length];
        for (int i = 0; i < respawnTimers.Length; i++)
        {
            respawnTimers[i] = 0;
        }

        snowballTimers = new float[snowballs.Length];
        for (int i = 0; i < snowballTimers.Length; i++)
        {
            snowballTimers[i] = 0;
        }

    }

    // Update constantly checks the state of the cookie to see if it becomes unactive
    void Update()
    {

        /*foreach (GameObject item in cookies)
       {
            setCookieState(item);
       }

		foreach (GameObject item in snowballs)
		{
			setCookieState(item);
		}*/
		

        for (int i = 0; i < cookies.Length; i++)
        {
            respawnTimers[i] = SetItemState(cookies[i], respawnTimers[i], 10.0f);
        }

        for (int i = 0; i < snowballs.Length; i++)
        {
            snowballTimers[i] = SetItemState(snowballs[i], snowballTimers[i], 20.0f);
        }
    }

    // Once a cookie is not active, the timer starts for it and then it respawns once it reaches that time. 
    /*public float setCookieState(GameObject item, float respawnTimer)
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
    }*/

	public float SetItemState(GameObject item, float itemTimer, float maxTime){
		if (item.activeSelf == false)
		{
			itemTimer += 0.05f;
			if (itemTimer >= maxTime)
			{
				itemTimer = 0;
				item.SetActive(true);
			}
		}
        return itemTimer;
	}
}
