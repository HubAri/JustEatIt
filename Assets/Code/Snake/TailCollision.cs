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
        if (collision.CompareTag("Enemy"))
        {
            if (!snakeTail.powerUpActivated)
            {
                // Destroy body parts
                snakeTail.DestroyBodyParts(gameObject.GetInstanceID());
            }
        }
    }
}
