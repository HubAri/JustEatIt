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
        PositionScoreText();

        scoreNum = 0;
        scoreText.text = "Score: " + scoreNum;
    }


    public void IncreaseScore(int num)
    {
        scoreNum += num;
        scoreText.text = "Score: " + scoreNum;
    }

    private void PositionScoreText()
    {
        RectTransform rectTransform = scoreText.GetComponent<RectTransform>();

        rectTransform.anchorMin = new Vector2(1f, 1f);
        rectTransform.anchorMax = new Vector2(1f, 1f);
        rectTransform.pivot = new Vector2(1f, 1f);

        float offsetX = 30f;  // horizontal offset
        float offsetY = 10f;  // vertical offset

        rectTransform.anchoredPosition = new Vector2(-offsetX, -offsetY);
    }
}
