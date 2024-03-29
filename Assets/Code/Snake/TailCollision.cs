using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailCollision : MonoBehaviour
{
    private Transform parentTransform;
    private SnakeTail snakeTail;

    private void Start()
    {
        parentTransform = transform.parent;
        snakeTail = parentTransform.GetComponent<SnakeTail>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("StationaryEnemy") || collision.CompareTag("QuickEnemy"))
        {
            if (!snakeTail.powerUpActivated)
            {
                // Destroy body parts
                FindObjectOfType<AudioManager>().Play("Ow");
                snakeTail.DestroyBodyParts(gameObject.GetInstanceID());
            }
        }
    }
}
