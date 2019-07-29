using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sfxHandler : MonoBehaviour
{

    private AudioSource audiosource;
    public GameObject BGMholder;
    private AudioSource BGM;
    public AudioClip clip1;
    public AudioClip clip2;
    int sceneLoaded;
    // Start is called before the first frame update
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        sceneLoaded = SceneManager.GetActiveScene().buildIndex;
        BGM = BGMholder.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sceneLoaded != 2 || sceneLoaded != 6)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                audiosource.clip = clip1;
                audiosource.Play();
            }
        }
        if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            audiosource.clip = clip2;
            audiosource.Play();
            BGM.mute = !BGM.mute;
        }
    }
}
