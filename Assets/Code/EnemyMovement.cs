using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float enemySpeed = 1f;
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
                    if (Mathf.Abs(transform.position.x) >= Mathf.Abs(screenBounds.x - radius)) // collision left or right
                    {
                        // calculate new direction
                        direction = Vector3.Reflect(direction.normalized, new Vector3(1, 0, 0));
                    }
                    else if (Mathf.Abs(transform.position.y) >= Mathf.Abs(screenBounds.y - radius)) // collision top or bottom
                    {
                        // calculate new direction
                        direction = Vector3.Reflect(direction.normalized, new Vector3(0, 1, 0));
                    }
                }
            }
        }

        // move Enemy
        transform.position += enemySpeed * Time.deltaTime * direction;

        // rotate Enemy
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle-90);
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
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Enemy bounces off Enemy
            Vector2 C = collision.contacts[0].point;
            Vector2 N = (C - (Vector2) collision.transform.position).normalized;
            Vector2 R = (Vector2) direction - 2 * (Vector2.Dot(direction, N)) * N;
            direction = R;
        }
    }

}
