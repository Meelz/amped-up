using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthBar : MonoBehaviour
{

    public Heartbeat_Controller manager;
    private float health;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.FindGameObjectWithTag("Player").GetComponent<Heartbeat_Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        health = manager.life;
        if (health >= 0 && health <= 100)
        {
            GetComponent<RectTransform>().sizeDelta = new Vector2(GetComponent<RectTransform>().sizeDelta.x, health);
        }
        
    }
}
