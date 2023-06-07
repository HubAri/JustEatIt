using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SnakeTail : MonoBehaviour
{

    public Transform SnakeTailGfx;
    public float circleDiameter;
    public int snakeLength;

    private List<Transform> snakeTails = new List<Transform>();
    private List<Vector2> positions = new List<Vector2>();

    // Start is called before the first frame update
    void Start()
    {
        positions.Add(SnakeTailGfx.position); // Position of SnakeTail 0 (behind the head)
        AddTail();
        AddTail();
        AddTail();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = ((Vector2)SnakeTailGfx.position - positions[0]).magnitude;

        if(distance > circleDiameter)
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
}
