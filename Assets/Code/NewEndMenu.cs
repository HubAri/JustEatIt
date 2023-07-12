using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewEndMenu : MonoBehaviour
{
    public GameObject menuEndObject;
    public Score score;
    public TextMeshProUGUI endText;
    public static bool endedGame = false;

    private int endScore;

    public void Freeze()
    {
        FindObjectOfType<AudioManager>().Pause("Background");
        endedGame = true;
        endScore = score.getScore();
        endText.text = $"You died!\r\n\nYour score is {endScore}\r\nYou can do better";
        menuEndObject.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Replay()
    {
        endedGame = false;
        Time.timeScale = 1.0f;
        var activeScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(activeScene.buildIndex);
    }

    public void ChangeToMenu()
    {
        endedGame = false;
        menuEndObject.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
