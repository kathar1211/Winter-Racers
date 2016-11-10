﻿using UnityEngine;
using System.Collections;

public class BoostBar : MonoBehaviour {

    /* Questions to ask myself - What object should this be attached to? I'm thinking the player object
     * What should I be passing into this script?
     * What will trigger this script? The collision of the player and the cookies I think 
     * Should this have an update method then? Or just call methods from this script such as
     *      -set up
     *      - refill
     *      - changeSpeed
     * */

    // Take in an array of objects that will make up the boostbar
    public GameObject[] boostCircles;
    //I'm using the UI bar that's set up in the main branch instead
    public GameObject CookieMeter;
    RectTransform meter; 

	// Use this for initialization
	void Start () {

        meter = CookieMeter.GetComponent<RectTransform>();

        // All shapes will start filled in

        // I'm thinking of a gradient where it starts at green and goes to yellow then red as you fill it more and more

    }
	
	// Update is called once per frame
	void Update () {

        // Check if the player this script will be attached to has run into any cookies

        // Check how many objects are full and then add color fill to one that's not. 

        // Thinking two sizes of cookies: small gets you half a boost and normal gets you a full boost

        // Check if player has enough boost power to increase speed. 

        // Bar will drain right to left in a line so probably going to be UI elements
	
	}

    void OnCollisionEnter2D(Collision2D c)
    {
        //check for collision with cookies
        if (c.collider.gameObject.tag == "Cookie")
        {
            //on collision increase cookie meter
            //max width is 100
            //increas by 20 for now - 5 cookies to fill meter
            if (meter.rect.width <= 80)
            {
                meter.sizeDelta = new Vector2(meter.rect.width + 20, meter.rect.height);
            }
            else
            {
                meter.sizeDelta = new Vector2(100, meter.rect.height);
            }

            Destroy(c.collider.gameObject);

        }


    }

    //let the main sled script know if its time to boost
    public bool BoostReady()
    {
        if (meter.rect.width == 100)
        {
            return true;
        }
        return false;
    }

    //reset the boost meter after using it
    public void ResetMeter()
    {
        meter.sizeDelta = new Vector2(00, meter.rect.height);
    }

}