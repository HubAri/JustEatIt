using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SnakeTail : MonoBehaviour
{

    public Transform SnakeTailGfx;
    private float circleDiameter = 0.32f;

    [HideInInspector]
    public int snakeLength;

    public SnakeMove snakeMove;

    private List<Transform> snakeTails = new();
    private List<Vector2> positions = new();

    // Start is called before the first frame update
    private void Start()
    {
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
        circleDiameter = setcircleDiameterBasedOnSpeed(snakeMove.speed, snakeMove.maxSpeed, snakeMove.minSpeed);


        if (distance > circleDiameter)
        {
            Vector2 direction = ((Vector2)SnakeTailGfx.position - positions[0]).normalized; // direction to SnakeTail 0

            positions.Insert(0, positions[0] + direction * circleDiameter);
            positions.RemoveAt(positions.Count - 1);

            distance -= circleDiameter;
        }
        
        for (int i = 0; i < snakeTails.Count; i++)
        {
            snakeTails[i].position = (Vector3)Vector2.Lerp(positions[i+1], positions[i], distance / circleDiameter) + new Vector3(0f, 0f, i * 0.1f); // Move all Tails to pos of previous and offset z-axis
        }
    }

    public void AddTail()
    {
        Transform tail = Instantiate(SnakeTailGfx, positions[positions.Count-1], transform.rotation, transform); // Create Tail
        snakeTails.Add(tail);
        positions.Add(tail.position);
        snakeLength = snakeTails.Count+1;
    }

    private float setcircleDiameterBasedOnSpeed(float speed, float maxSpeed, float minSpeed)
    {
        float normalizedSpeed = Mathf.InverseLerp(minSpeed, maxSpeed, speed); // percentage of speed in given range
        float x = Mathf.Lerp(0.15f, 0.4f, normalizedSpeed); // interpolate normalizedSpeed between 0.1 and 0.4
        return x;
    }
}
