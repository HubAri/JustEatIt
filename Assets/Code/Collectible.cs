using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public AnimationCurve smoothStopCurve;

    private Score score;
    private SnakeTail snakeTail;
    private SpawnCollectibles spawnCollectibles;


    private float pushForce = 10f;
    private float smoothStopDuration = 1f;

    private Rigidbody2D rb;
    private Coroutine pushCoroutine;

    private void Start()
    {
        snakeTail = GameObject.Find("Snake").GetComponent<SnakeTail>();
        score = GameObject.Find("GameObject").GetComponent<Score>();
        spawnCollectibles = GameObject.Find("Collectibles").GetComponent<SpawnCollectibles>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("PowerUp"))
        {
            if (pushCoroutine != null)
                StopCoroutine(pushCoroutine);

            // Calculate the direction away from the collision point
            Vector2 awayDirection = (Vector2)transform.position - (Vector2)collision.transform.position;
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
                score.IncreaseScore(snakeTail.snakeLength * 10);
                snakeTail.AddTail();
                spawnCollectibles.SpawnNeut();
            }
            else if (gameObject.CompareTag("posCol"))
            {
                score.IncreaseScore(snakeTail.snakeLength * 50);
            }
            else if (gameObject.CompareTag("negCol"))
            {
                score.IncreaseScore(-250);
            }

            Destroy(this.gameObject, 0.02f);
        }
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
}
