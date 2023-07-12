using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static Unity.Burst.Intrinsics.X86;

public class EnemyMovement : MonoBehaviour
{
    private float enemySpeed = 1.5f;
    private float quickEnemySpeed = 3f;
    private float rotationSpeed = 5f;

    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    private Vector2 destinationForEnemy;
    private bool wasIn = false;
    private float radius;
    private Vector3 direction;


    private void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x; //extents = size of width / 2
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y; //extents = size of height / 2
        radius = GetComponent<CircleCollider2D>().radius;

        SetSpawnDirectionForEnemy();

    }

    private void Update()
    {

        if (Mathf.Abs(transform.position.x) > Mathf.Abs(screenBounds.x + objectWidth) || Mathf.Abs(transform.position.y) > Mathf.Abs(screenBounds.y + objectHeight))
        { // Enemy completly outside of view -> rotate to view
            SetSpawnDirectionForEnemy();
        }
        else
        {
            // Enemy completly in view
            if (Mathf.Abs(transform.position.x) < Mathf.Abs(screenBounds.x - radius) && Mathf.Abs(transform.position.y) < Mathf.Abs(screenBounds.y - radius))
            {
                wasIn = true;
            }
            else
            {
                // was inside, touches border
                if (wasIn)
                {

                    float overShootHR = (transform.position.x + radius) - screenBounds.x;
                    float overShootHL = -screenBounds.x - (transform.position.x - radius);
                    float overShootVB = (transform.position.y + radius) - screenBounds.y;
                    float overShootVT = -screenBounds.y - (transform.position.y - radius);

                    if (overShootHR > 0)
                    {
                        // right
                        transform.position -= new Vector3(overShootHR, 0, 0);
                        direction = Vector3.Reflect(direction.normalized, new Vector3(1, 0, 0));
                    }
                    if (overShootHL > 0)
                    {
                        // left
                        transform.position += new Vector3(overShootHL, 0, 0);
                        direction = Vector3.Reflect(direction.normalized, new Vector3(1, 0, 0));
                    }
                    if (overShootVB > 0)
                    {
                        // top
                        transform.position -= new Vector3(0, overShootVB, 0);
                        direction = Vector3.Reflect(direction.normalized, new Vector3(0, 1, 0));
                    }
                    if (overShootVT > 0)
                    {
                        // bottom
                        transform.position += new Vector3(0, overShootVT, 0);
                        direction = Vector3.Reflect(direction.normalized, new Vector3(0, 1, 0));
                    }

                }
            }
        }

        // move Enemy
        transform.position += enemySpeed * Time.deltaTime * direction;
        // move Spider
        if (gameObject.CompareTag("QuickEnemy"))
            transform.position += quickEnemySpeed * Time.deltaTime * direction;


        // rotate Enemy     
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle - 90);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        
    }

    private void SetSpawnDirectionForEnemy()
    {
        float x = UnityEngine.Random.Range(-screenBounds.x + objectWidth, screenBounds.x - objectWidth);
        float y = UnityEngine.Random.Range(-screenBounds.y + objectHeight, screenBounds.y - objectHeight);

        destinationForEnemy = new Vector2(x, y);
        direction = ((Vector3) destinationForEnemy - transform.position).normalized;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("PowerUp") || collision.gameObject.CompareTag("SlownessPowerUp") || collision.gameObject.CompareTag("QuickEnemy"))
        {
            Collider2D collider = collision.collider;
            float otherRadius = 0f;

            if (collider is CircleCollider2D circleCollider)
                otherRadius = circleCollider.radius;

            float dx = transform.position.x - collision.transform.position.x;
            float dy = transform.position.y - collision.transform.position.y;
            float dist = Mathf.Sqrt(dx * dx + dy * dy);
            float overlap = radius + otherRadius - dist;

            float angle = Mathf.Atan2(dy, dx);
            float cosa = Mathf.Cos(angle);
            float sina = Mathf.Sin(angle);

            float overlapX = overlap * cosa;
            float overlapY = overlap * sina;
            float posXNew = transform.position.x + overlapX / 2;
            float posYNew = transform.position.y + overlapY / 2;
            float otherPosXNew = collision.transform.position.x - overlapX / 2;
            float otherPosYNew = collision.transform.position.y - overlapY / 2;
            transform.position = new Vector3(posXNew, posYNew, 0);
            collision.transform.position = new Vector3(otherPosXNew, otherPosYNew, 0);



            // Enemy bounces off Enemy
            Vector2 C = collision.contacts[0].point;
            Vector2 N = (C - (Vector2)collision.transform.position).normalized;
            Vector2 R = (Vector2)direction - 2 * (Vector2.Dot(direction, N)) * N;
            direction = R;


        }
    }

}
