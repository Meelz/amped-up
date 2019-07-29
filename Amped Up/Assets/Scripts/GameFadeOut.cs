using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameFadeOut : MonoBehaviour
{
    public void OnFadeComplete()
    {
        SceneManager.LoadScene(6);
    }
}
