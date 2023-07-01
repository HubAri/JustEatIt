using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SnakeTail : MonoBehaviour
{

    public Transform SnakeHeadGfx;
    public Transform SnakeTailGfx;
    private float circleDiameter = 0.32f;

    [HideInInspector]
    public int snakeLength;

    public SnakeMove snakeMove;
    public bool powerUpActivated = false;


    [SerializeField]
    private Sprite Head, HeadSpikes, Body, BodySpikes;

    private Dictionary<int, Transform> snakeTails = new();
    private List<Vector2> positions = new();

    // Start is called before the first frame update
    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("Background");
        positions.Add(SnakeTailGfx.position); // Position of SnakeTail 0 (behind the head)
        AddTail();
        AddTail();
        AddTail();
    }

    // Update is called once per frame
    private void Update()
    {
        float distance = ((Vector2)SnakeTailGfx.position - positions[0]).magnitude;

        // between 0.1 and 0.4 based on speed
        circleDiameter = SetcircleDiameterBasedOnSpeed(snakeMove.speed, snakeMove.maxSpeed, snakeMove.minSpeed);


        if (distance > circleDiameter)
        {
            Vector2 direction = ((Vector2)SnakeTailGfx.position - positions[0]).normalized; // direction to SnakeTail 0

            positions.Insert(0, positions[0] + direction * circleDiameter);
            positions.RemoveAt(positions.Count - 1);

            distance -= circleDiameter;
        }

        for (int i = 0; i < snakeTails.Count; i++)
        {
            var item = snakeTails.ElementAt(i);
            var itemValue = item.Value;

            itemValue.position = (Vector3)Vector2.Lerp(positions[i + 1], positions[i], distance / circleDiameter) + new Vector3(0f, 0f, i * 0.1f); // Move all Tails to pos of previous and offset z-axis
        }

    }

    public void AddTail()
    {
        Transform tail = Instantiate(SnakeTailGfx, positions[positions.Count - 1], transform.rotation, transform); // Create Tail
        snakeTails.Add(tail.gameObject.GetInstanceID(), tail);
        positions.Add(tail.position);
        snakeLength = snakeTails.Count + 1;
    }

    private float SetcircleDiameterBasedOnSpeed(float speed, float maxSpeed, float minSpeed)
    {
        float normalizedSpeed = Mathf.InverseLerp(minSpeed, maxSpeed, speed); // percentage of speed in given range
        float x = Mathf.Lerp(0.15f, 0.4f, normalizedSpeed); // interpolate normalizedSpeed between 0.1 and 0.4
        return x;
    }

    public void DestroyBodyParts(int instanceID)
    {
        try
        {
            GameObject snakeTail = snakeTails[instanceID].gameObject;

            Destroy(snakeTail);
            snakeTails.Remove(instanceID);
            snakeLength = snakeTails.Count + 1;
        }
        catch (KeyNotFoundException) { }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (!powerUpActivated)
            {

                // Game over
                Debug.Log("Game over");
                GameObject snake = gameObject;
                Destroy(snake);
                Time.timeScale = 0;
                FindObjectOfType<AudioManager>().Stop("Background");
                FindObjectOfType<AudioManager>().Play("End");
            }
            else
            {
                Destroy(collision.gameObject);

            }
        }
        else if (collision.gameObject.CompareTag("PowerUp"))
        {
            Destroy(collision.gameObject);

            StartCoroutine(WaitPowerUp());


        }
        else if (collision.gameObject.CompareTag("StationaryEnemy"))
        {
            Debug.Log("Game over");
            GameObject snake = gameObject;
            Destroy(snake);
            Time.timeScale = 0;
            FindObjectOfType<AudioManager>().Stop("Background");
            FindObjectOfType<AudioManager>().Play("End");
        }


    }



    IEnumerator WaitPowerUp()
    {
        powerUpActivated = true;

        // Change Head
        if (SnakeHeadGfx.gameObject.GetComponent<SpriteRenderer>() != null)
            SnakeHeadGfx.gameObject.GetComponent<SpriteRenderer>().sprite = HeadSpikes;
        // Change Bodys
        GameObject[] bodyParts = GameObject.FindGameObjectsWithTag("Body");
        FindObjectOfType<AudioManager>().Stop("Background");
        FindObjectOfType<AudioManager>().Play("Spikes");
        foreach (GameObject body in bodyParts)
            body.GetComponent<SpriteRenderer>().sprite = BodySpikes;
        

        // wait 10 sec
        yield return new WaitForSeconds(10 * 80 * Time.fixedDeltaTime);

        // Change Head
        if (SnakeHeadGfx.gameObject.GetComponent<SpriteRenderer>() != null)
            SnakeHeadGfx.gameObject.GetComponent<SpriteRenderer>().sprite = Head;
        // Change Bodys
        GameObject[] newBodyParts = GameObject.FindGameObjectsWithTag("Body");
        FindObjectOfType<AudioManager>().Play("Background");
        foreach (GameObject body in newBodyParts)
            body.GetComponent<SpriteRenderer>().sprite = Body;

        powerUpActivated = false;
    }

}
