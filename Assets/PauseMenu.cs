using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{  
    public GameObject menuPauseObject; // drag and drop pause menu into editor
    public static bool pausedGame = false;

    // Update is called once per frame
    //check for Escape Input to open Ingame-Menu
    void Update()
    {
       if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausedGame) //select method according to pausedGame bool 
            {
                ResumeGame();
            }else
            {
                Freeze();
            }
        }
    }

    //disable menu and continue with normal timeScale
    public void ResumeGame()
    {
        pausedGame = false;
        menuPauseObject.SetActive(false);
        Time.timeScale = 1f;
    }

    //enable menu and freeze screen / pause
    void Freeze()
    {
        pausedGame = true;
        menuPauseObject.SetActive(true);
        Time.timeScale = 0f;
    }

    // end application
    public void QuitGame()
    {
        Application.Quit();
    }

    //load menu scene and put parameters back to default
    public void ChangeToMenu()
    {
        pausedGame = false;
        menuPauseObject.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
