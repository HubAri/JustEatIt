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
        SetSizeScore();
        PositionScore();

        scoreNum = 0;
        scoreText.text = "Score: " + scoreNum;
    }


    public void IncreaseScore(int num)
    {
        scoreNum += num;
        scoreText.text = "Score: " + scoreNum;
    }






    private void PositionScore()
    {
        RectTransform rectTransform = scoreText.GetComponent<RectTransform>();

        rectTransform.anchorMin = new Vector2(1f, 1f);
        rectTransform.anchorMax = new Vector2(1f, 1f);
        rectTransform.pivot = new Vector2(1f, 1f);

        float offset = 30f;

        rectTransform.anchoredPosition = new Vector2(-offset, -offset);

    }


    private void SetSizeScore()
    {
        RectTransform rectTransform = scoreText.GetComponent<RectTransform>();

        float scoreHeight = 60f;
        float scoreWidtht = 550f;

        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, scoreHeight);
        rectTransform.sizeDelta = new Vector2(scoreWidtht, rectTransform.sizeDelta.y);
    }
}
