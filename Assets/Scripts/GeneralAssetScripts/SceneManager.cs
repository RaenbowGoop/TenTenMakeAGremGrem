using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class SceneManager : MonoBehaviour
{
    // Loads Scene
    public void LoadsScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    /* Change to Specified Scene (COROUTINE FOR MUSIC LATER)
    public void ChangeScene(string sceneName)
    {
        StartCoroutine(LoadScene(sceneName));
    }
    */
}