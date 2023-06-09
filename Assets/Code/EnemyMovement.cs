using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMovement : MonoBehaviour
{
    public KeepInScreen keepInScreen;

    private float rotaionSpeed = 200f;

    private float enemySpeed = 1f;
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;
    private Vector2 destinationForEnemy;
    private bool wasIn = false;
    private float radius;
    private Vector3 previousPosition;

    private bool clockwise = true;

    private void Awake()
    {
        keepInScreen.enabled = false;
    }

    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x; //extents = size of width / 2
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y; //extents = size of height / 2
        radius = GetComponent<CircleCollider2D>().radius;
        // get a pos in view where Enemy has to go
        GetDestForEnemy();
    }

    void Update()
    {

        if (Mathf.Abs(transform.position.x) > Mathf.Abs(screenBounds.x + objectWidth) || Mathf.Abs(transform.position.y) > Mathf.Abs(screenBounds.y + objectHeight))
        { // Enemy completly outside of view -> rotate to view
            
            float leftright = destinationForEnemy.x - transform.position.x;
            float updown = transform.position.y - destinationForEnemy.y;
            float angle = (180 + 360 + Mathf.Atan2(leftright, updown) * Mathf.Rad2Deg) % 360;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            Debug.Log("draussen");

            // Enemy goes straight
            transform.position += enemySpeed * Time.deltaTime * transform.up;
        }
        else
        {
            // Enemy completly in view
            if (Mathf.Abs(transform.position.x) < Mathf.Abs(screenBounds.x - radius) && Mathf.Abs(transform.position.y) < Mathf.Abs(screenBounds.y - radius))
            {
                keepInScreen.enabled = false; // wieder auf true
                previousPosition = transform.position;
                Debug.Log("DRIN");
                wasIn = true;

                // Enemy goes straight
                transform.position += enemySpeed * Time.deltaTime * transform.up;
            }
            else
            {
                if (wasIn)
                {
                    // Berechne den Einfallswinkel
                    Vector2 incomingDirection = transform.position - previousPosition;
                    incomingDirection.Normalize();

                    float angle = -Mathf.Atan2(incomingDirection.x, incomingDirection.y) * Mathf.Rad2Deg;
                    
                    float tmpAngle = angle + 360;
                    tmpAngle %= 360;

                    Debug.Log($"Winkel: {angle}");

                    if (Mathf.Abs(transform.position.x) >= Mathf.Abs(screenBounds.x - radius)) // collision left or right
                    {
                        Debug.Log("left right");
                        Debug.Log($"Winkel positiv: {tmpAngle}");
                        if ((90 < tmpAngle && tmpAngle < 180) || (270 < tmpAngle && tmpAngle < 360))
                            clockwise = false;
                        else
                            clockwise = true;
                    }
                    if (Mathf.Abs(transform.position.y) >= Mathf.Abs(screenBounds.y - radius)) // collision top or bottom
                    {
                        Debug.Log("top bottom");
                        Debug.Log($"Winkel positiv: {tmpAngle}");
                        if ((0 < tmpAngle && tmpAngle < 90) || (180 < tmpAngle && tmpAngle < 270))
                            clockwise = false;
                        else
                            clockwise = true;
                    }

                    float newAngle = 180f - 2 * angle;
                    Debug.Log(clockwise);
                    Debug.Log($"newAngle: {newAngle}");

                    if (clockwise)
                    {
                        newAngle = angle + newAngle;
                    }
                    else
                    {
                        newAngle = angle - newAngle;
                    }
                    Debug.Log($"gesetzt: {newAngle}");

                    // Rotiere den Gegner um den Ausfallswinkel
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, newAngle));
                }
            }


        }


    }

    private void GetDestForEnemy()
    {
        float x = UnityEngine.Random.Range(-screenBounds.x + objectWidth, screenBounds.x - objectWidth);
        float y = UnityEngine.Random.Range(-screenBounds.y + objectHeight, screenBounds.y - objectHeight);

        destinationForEnemy = new Vector2(x, y);
    }

    private IEnumerator WallRotate()
    {
        Quaternion startRotation = transform.rotation;
        Quaternion targetRotation = startRotation * Quaternion.Euler(0f, 0f, 180f); // Rotate by 180 degrees

        float elapsedTime = 0f;
        float totalRotationTime = 1f; // Total time for the rotation in seconds

        while (elapsedTime < totalRotationTime)
        {
            float t = elapsedTime / totalRotationTime;
            transform.rotation = Quaternion.Slerp(startRotation, targetRotation, t);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation; // Set the final rotation
    }

}
