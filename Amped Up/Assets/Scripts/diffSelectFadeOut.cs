using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class diffSelectFadeOut : MonoBehaviour
{
    public void OnFadeComplete()
    {
        SceneManager.LoadScene(4);
    }
}
