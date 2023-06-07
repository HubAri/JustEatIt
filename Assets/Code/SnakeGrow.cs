using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SnakeGrow : MonoBehaviour
{

    public SnakeTail snakeTail;
    public SpawnCollectibles spawnCollectibles;
    public TextMeshProUGUI scoreText;

    private int scoreNum;

    private void Start()
    {
        scoreNum = 0;
        scoreText.text = "Score: " + scoreNum;
    }
    private void OnTriggerEnter2D(Collider2D Collectable)
    {
        if (Collectable.tag == "normalCol")
        {
            Destroy(Collectable.gameObject, 0.02f);
            scoreNum += snakeTail.snakeLength * 10;
            snakeTail.AddTail();
            scoreText.text = "Score: " + scoreNum;
            spawnCollectibles.SpawnNeut();
        }
        else if (Collectable.tag == "posCol")
        {
            Destroy(Collectable.gameObject, 0.02f);
            scoreNum += snakeTail.snakeLength * 50;
            scoreText.text = "Score: " + scoreNum;
        }
        else if (Collectable.tag == "negCol")
        {
            Destroy(Collectable.gameObject, 0.02f);
            scoreNum -= 250;
            scoreText.text = "Score: " + scoreNum;
        }
    }
}
