using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class songSelectFadeOut : MonoBehaviour
{
    public void OnFadeComplete()
    {
        SceneManager.LoadScene(5);
    }
    public void OnEscapeComplete()
    {
        SceneManager.LoadScene(2);
    }
}
