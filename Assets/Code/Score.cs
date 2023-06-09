using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    private int scoreNum;

    private void Start()
    {
        scoreNum = 0;
        scoreText.text = "Score: " + scoreNum;
    }


    public void IncreaseScore(int num)
    {
        scoreNum += num;
        scoreText.text = "Score: " + scoreNum;
    }
}
