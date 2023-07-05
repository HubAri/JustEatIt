using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public AnimationCurve smoothStopCurve;

    [SerializeField]
    private ScorePopup ScorePopup;

    private Score score;
    private SnakeTail snakeTail;
    private SpawnCollectibles spawnCollectibles;


    private float pushForce = 12f;
    private float smoothStopDuration = 1f;

    private Vector2 screenBounds;
    private float radius;
    private Vector2 awayDirection;
    private Rigidbody2D rb;
    private Coroutine pushCoroutine;

    private void Start()
    {
        snakeTail = GameObject.Find("Snake").GetComponent<SnakeTail>();
        score = GameObject.Find("GameObject").GetComponent<Score>();
        spawnCollectibles = GameObject.Find("Collectibles").GetComponent<SpawnCollectibles>();
        rb = GetComponent<Rigidbody2D>();

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        radius = this.gameObject.GetComponent<CircleCollider2D>().radius;
    }

    private void FixedUpdate()
    {
        CheckWallCollision();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("PowerUp") || collision.gameObject.CompareTag("QuickEnemy"))
        {
            HandleOverlap(collision);

            if (pushCoroutine != null)
                StopCoroutine(pushCoroutine);

            // Calculate the direction away from the collision point
            awayDirection = (Vector2)transform.position - (Vector2)collision.transform.position;
            awayDirection.Normalize();

            // Calculate the push velocity
            Vector2 pushVelocity = awayDirection * pushForce;

            // Apply the push velocity to the rigidbody
            rb.velocity = pushVelocity;

            // Start the push coroutine
            pushCoroutine = StartCoroutine(StopPush());
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            if (gameObject.CompareTag("normalCol"))
            {
                int amount = snakeTail.snakeLength * 10;
                score.IncreaseScore(amount);
                snakeTail.AddTail();
                ScorePopup.Create(this.gameObject.transform.position, amount, false);
                spawnCollectibles.SpawnNeut();
                FindObjectOfType<AudioManager>().Play("Eat");
            }
            else if (gameObject.CompareTag("posCol"))
            {
                int amount = snakeTail.snakeLength * 50;
                score.IncreaseScore(amount);
                ScorePopup.Create(this.gameObject.transform.position, amount, false);
                FindObjectOfType<AudioManager>().Play("Joy");
            }
            else if (gameObject.CompareTag("negCol"))
            {
                int amount = -250;
                score.IncreaseScore(amount);
                ScorePopup.Create(this.gameObject.transform.position, amount, true);
                FindObjectOfType<AudioManager>().Play("Disgust");
            }

            Destroy(this.gameObject, 0.02f);
        }
    }

    private void CheckWallCollision()
    {
        if (Mathf.Abs(transform.position.x) + radius >= screenBounds.x || Mathf.Abs(transform.position.y) + radius >= screenBounds.y)
        {
            if (pushCoroutine != null)
                StopCoroutine(pushCoroutine);

            // Calculate the direction away from the collision point
            float overShootHR = (transform.position.x + radius) - screenBounds.x;
            float overShootHL = -screenBounds.x - (transform.position.x - radius);
            float overShootVB = (transform.position.y + radius) - screenBounds.y;
            float overShootVT = -screenBounds.y - (transform.position.y - radius);

            if (overShootHR > 0)
            {
                // right
                transform.position -= new Vector3(overShootHR, 0, 0);
                awayDirection = Vector3.Reflect(awayDirection.normalized, new Vector3(1, 0, 0));
            }
            if (overShootHL > 0)
            {
                // left
                transform.position += new Vector3(overShootHL, 0, 0);
                awayDirection = Vector3.Reflect(awayDirection.normalized, new Vector3(1, 0, 0));
            }
            if (overShootVB > 0)
            {
                // top
                transform.position -= new Vector3(0, overShootVB, 0);
                awayDirection = Vector3.Reflect(awayDirection.normalized, new Vector3(0, 1, 0));
            }
            if (overShootVT > 0)
            {
                // bottom
                transform.position += new Vector3(0, overShootVT, 0);
                awayDirection = Vector3.Reflect(awayDirection.normalized, new Vector3(0, 1, 0));
            }

            awayDirection.Normalize();

            // Lower push velocity
            Vector2 pushVelocity = awayDirection;

            // Apply the push velocity to the rigidbody
            rb.velocity = pushVelocity * 3f;

            // Start the push coroutine
            pushCoroutine = StartCoroutine(StopWallBounce());
        }
    }
    private IEnumerator StopWallBounce()
    {
        float elapsedTime = 0f;
        float normalizedTime = 0f;
        Vector2 initialVelocity = rb.velocity;

        yield return new WaitForSeconds(0.2f);

        while (normalizedTime < 0.2f)
        {
            // slow down
            rb.velocity = initialVelocity.normalized * 0.95f;

            elapsedTime += Time.deltaTime;
            normalizedTime = elapsedTime / smoothStopDuration;
            yield return null;
        }
        // Reset the velocity to zero
        rb.velocity = Vector2.zero;
    }

    private IEnumerator StopPush()
    {
        float elapsedTime = 0f;
        float normalizedTime = 0f;
        Vector2 initialVelocity = rb.velocity;

        while (normalizedTime < 1f)
        {
            // Calculate the current velocity based on the animation curve
            float currentVelocity = Mathf.Lerp(pushForce, 0f, smoothStopCurve.Evaluate(normalizedTime));

            // Update the velocity of the rigidbody
            rb.velocity = initialVelocity.normalized * currentVelocity;

            // Update the elapsed time and normalized time
            elapsedTime += Time.deltaTime;
            normalizedTime = elapsedTime / smoothStopDuration;

            yield return null;
        }

        // Reset the velocity to zero
        rb.velocity = Vector2.zero;
    }

    private void HandleOverlap(Collider2D collision)
    {
        Collider2D collider = collision.gameObject.GetComponent<Collider2D>();
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
    }


}
