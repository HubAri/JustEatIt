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

        float offsetXPercentage = 0.02f;  // 2% offset
        float offsetX = Screen.height * offsetXPercentage;
        float offsetYPercentage = 0.02f;  // 2% offset
        float offsetY = Screen.height * offsetYPercentage;

        rectTransform.anchoredPosition = new Vector2(-offsetX, -offsetY);

    }


    private void SetSizeScore()
    {
        RectTransform rectTransform = scoreText.GetComponent<RectTransform>();

        float heightPercentage = 0.05f;  // 5% height
        float scoreHeight = Screen.height * heightPercentage;


        float widthPercentage = 0.15f;  // 15% width
        float scoreWidtht = Screen.width * widthPercentage;

        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, scoreHeight);
        rectTransform.sizeDelta = new Vector2(scoreWidtht, rectTransform.sizeDelta.y);
    }
}
