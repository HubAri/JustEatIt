using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SnakeMove : MonoBehaviour
{
    public float speed = 3f;
    public float rotaionSpeed = 200f;

    private Vector3 mousePosition;
    private float velX = 0f;

    private bool mouseControl = true;

    private void Update()
    {
        velX = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        if (!mouseControl)
        {
            transform.Translate(Vector2.up * speed * Time.fixedDeltaTime, Space.Self);

            transform.Rotate(Vector3.forward * -velX * rotaionSpeed * Time.fixedDeltaTime);
        }
        else
        {
            // Move Head to Cursor
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;
            Vector2 tmpDir = mousePosition - transform.position;

            if (Mathf.Sqrt(Mathf.Pow(tmpDir.x, 2) + Mathf.Pow(tmpDir.y, 2)) > 0.2f)
            {
                // faster if cursor far away
                //transform.position = Vector2.Lerp(transform.position, mousePosition, speed / 4 * Time.deltaTime);

                // always same speed
                transform.position = Vector2.Lerp(transform.position, mousePosition, speed * 1f / tmpDir.magnitude * Time.deltaTime);

                /* also possible:
                transform.position = Vector3.MoveTowards(transform.position, mousePosition, speed * Time.deltaTime);
                */

                // Rotate Head
                float leftright = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
                float updown = transform.position.y - Camera.main.ScreenToWorldPoint(Input.mousePosition).y;

                float angle = (180 + 360 + Mathf.Atan2(leftright, updown) * Mathf.Rad2Deg) % 360;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            }


        }

    }
}
