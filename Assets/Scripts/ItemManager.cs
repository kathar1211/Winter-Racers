using UnityEngine;
using System.Collections;

public class ItemManager : MonoBehaviour {

    public GameObject[] cookies;
    private float respawnTimer = 0;

    void Start()
    {
        cookies = GameObject.FindGameObjectsWithTag("Cookie");
    }

    void Update()
    {
        foreach (GameObject item in cookies)
        {
            setCookieState(item);
        }
    }

    public void setCookieState(GameObject item)
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
    }
}
