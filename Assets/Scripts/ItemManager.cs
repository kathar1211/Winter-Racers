﻿using UnityEngine;
using System.Collections;

public class ItemManager : MonoBehaviour {

    public GameObject[] cookies;
    private float respawnTimer = 0;
	private float itemTimer = 0.0f;


	public GameObject[] snowballs;

    // Starts by finding all items with the cookie tag, this way if we have other levels we don't have to manually add them to an array
    void Start()
    {
        cookies = GameObject.FindGameObjectsWithTag("Cookie");
		snowballs = GameObject.FindGameObjectsWithTag ("Snowball");
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
		
    }

    // Once a cookie is not active, the timer starts for it and then it respawns once it reaches that time. 
    public void setCookieState(GameObject item)
    {
        //Debug.Log(respawnTimer);
        if (item.activeSelf == false)
        {
            respawnTimer += 0.05f;
            if (respawnTimer >= 10.0f)
            {
                respawnTimer = 0;
                item.SetActive(true);
            }
        }
    }

	public void SetSnowballState(GameObject item){
		if (item.activeSelf == false)
		{
			itemTimer += 0.05f;
			if (itemTimer >= 10.0f)
			{
				itemTimer = 0;
				item.SetActive(true);
			}
		}
	}
}
