using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonPressA : MonoBehaviour {

    SpriteRenderer m_spriteRenderer;
	// Use this for initialization
	void Start () {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.D))
        {
            m_spriteRenderer.enabled = true;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            m_spriteRenderer.enabled = false;
        }
    }
}
