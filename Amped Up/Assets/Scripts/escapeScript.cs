using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class escapeScript : MonoBehaviour
{
    // Start is called before the first frame update
    private float startTime = 0f;
    private float timer = 0.0f;
    public float holdTime = 1.0f;
    public Animator animator;

    Text m_text;

    private bool held = false;

    void Start()
    {
        m_text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            m_text.enabled = true;
            startTime = Time.time;
            timer = startTime;
        }

        if (Input.GetKey(KeyCode.Escape) && held == false)
        {
            timer += Time.deltaTime;

            if (timer > (startTime + holdTime))
            {
                held = true;
                ButtonHeld();
            }
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            m_text.enabled = false;
            held = false;
        }

        
    }

    void ButtonHeld()
    {
        //go back to song selection
        animator.SetTrigger("FadeOut");
        Game_Data.hasPassed = false;
        Debug.Log("held for " + holdTime + " seconds");
    }
}
