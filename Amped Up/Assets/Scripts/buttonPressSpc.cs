using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonPressSpc : MonoBehaviour
{

    SpriteRenderer m_spriteRenderer;
    // Use this for initialization
    void Start()
    {
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_spriteRenderer.enabled = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            m_spriteRenderer.enabled = false;
        }
    }
}
