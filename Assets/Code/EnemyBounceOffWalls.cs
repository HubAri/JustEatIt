using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBounceOffWalls : MonoBehaviour
{
    private Vector2 screenBounds;
    private float objectRadius;
    private SpriteRenderer sr;
    private CircleCollider2D cc;

    // Use this for initialization
    private void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        objectRadius = transform.GetComponent<SpriteRenderer>().bounds.extents.y; //extents = size of height / 2
        sr = GetComponent<SpriteRenderer>();
        cc = GetComponent<CircleCollider2D>();
    }

    private void LateUpdate()
    {
        Bounds bounds1 = new Bounds(Vector3.zero, 2 * screenBounds);
        Bounds bounds2 = new Bounds(Vector3.zero, 2 * screenBounds + new Vector2(0.2f, 0.2f));
        Bounds screenBound = new Bounds(Vector3.zero, new Vector3(Screen.width, Screen.height, 0));

        if (cc.bounds.Intersects(screenBound))
        {
            
        }
    }

}
