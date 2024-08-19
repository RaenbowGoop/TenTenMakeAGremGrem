using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public class SceneManager : MonoBehaviour
{
    public Animator sceneTransition;
    float transitionTime = 1f;

    // Loads Scene
    private void LoadScene(string sceneName) {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    //Loading Scene with transition
    private IEnumerator LoadSceneWithTransition(string sceneName) {
        //Play animation
        sceneTransition.SetTrigger("Start");

        //wait for transition to finish
        yield return new WaitForSeconds(transitionTime);

        //Load Scene
        LoadScene(sceneName);
    }

    // Change to Specified Scene
    public void ChangeScene(string sceneName) {
        StartCoroutine(LoadSceneWithTransition(sceneName));
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}