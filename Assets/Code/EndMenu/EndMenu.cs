using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    public void ReplayGame()
    {
        Debug.Log("Replay Game pressed");
        Time.timeScale = 1.0f;
        var activeScene = SceneManager.GetActiveScene();   
        SceneManager.LoadScene(activeScene.buildIndex);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game pressed");
        Application.Quit();
    }
}
