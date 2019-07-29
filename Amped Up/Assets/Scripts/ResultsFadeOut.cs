using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultsFadeOut : MonoBehaviour
{
    public void OnFadeComplete()
    {
        SceneManager.LoadScene(3);
    }
}
