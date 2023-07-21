using UnityEngine;

public class SnakeMove : MonoBehaviour
{
    public float maxSpeed = 8f;
    public float minSpeed = 2f;
    public float maxDistance = 10f;
    public float minDistance = 1f;

    public float rotaionSpeed = 200f;

    public Animator headanimator;
    private SnakeTail snakeTail;

    [HideInInspector]
    public float speed;

    private float speedKeyboard = 3f; // if controlled via keyboard
    private float velX = 0f;

    private Vector3 mousePosition;
    private Vector2 screenBounds;

    private bool mouseControl = true;

    private void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        snakeTail = GameObject.Find("Snake").GetComponent<SnakeTail>();
    }

    private void Update()
    {
        if (snakeTail.powerUpActivated)
        {
            headanimator.SetBool("PowerUpActive", true);
        }
        else {
            headanimator.SetBool("PowerUpActive", false);
        }
    }

    private void FixedUpdate()
    {
        if (!mouseControl)
        {
            velX = Input.GetAxisRaw("Horizontal"); // pressed A -> -1, D -> 1, else 0
            transform.Translate(Vector2.up * speedKeyboard * Time.fixedDeltaTime, Space.Self);
            transform.Rotate(Vector3.forward * -velX * rotaionSpeed * Time.fixedDeltaTime);
        }
        else
        {
            // Move Head to Cursor
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0f;
            Vector2 tmpDir = mousePosition - transform.position;

            float xInScreen = Mathf.Min(Mathf.Abs(mousePosition.x), screenBounds.x);
            float yInScreen = Mathf.Min(Mathf.Abs(mousePosition.y), screenBounds.y);

            if (mousePosition.x < 0)
                mousePosition.x = -xInScreen;
            else
                mousePosition.x = xInScreen;

            if (mousePosition.y < 0)
                mousePosition.y = -yInScreen;
            else
                mousePosition.y = yInScreen;

            // Only move when there is at least a small distance to Cursor
            if (Mathf.Sqrt(Mathf.Pow(tmpDir.x, 2) + Mathf.Pow(tmpDir.y, 2)) > 0.2f)
            {

                // Calculate the distance between the player and the cursor
                float distance = Vector3.Distance(transform.position, mousePosition);

                // Calculate the speed based on the distance
                speed = Mathf.Lerp(minSpeed, maxSpeed, Mathf.InverseLerp(minDistance, maxDistance, distance));

                transform.position = Vector3.MoveTowards(transform.position, mousePosition, speed * Time.deltaTime);



                // Rotate Head
                float leftright = Camera.main.ScreenToWorldPoint(Input.mousePosition).x - transform.position.x;
                float updown = transform.position.y - Camera.main.ScreenToWorldPoint(Input.mousePosition).y;

                float angle = (180 + 360 + Mathf.Atan2(leftright, updown) * Mathf.Rad2Deg) % 360;
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            }


        }

    }


}
