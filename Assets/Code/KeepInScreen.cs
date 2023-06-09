using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepInScreen : MonoBehaviour
{
    public GameObject obj;

    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    private void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        try
        {
            objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x; //extents = size of width / 2
            objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y; //extents = size of height / 2
        }
        catch (MissingComponentException)
        {
            objectWidth = obj.transform.GetComponent<SpriteRenderer>().bounds.extents.x;
            objectHeight = obj.transform.GetComponent<SpriteRenderer>().bounds.extents.y;
        }

    }

    private void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + objectHeight, screenBounds.y - objectHeight);
        transform.position = viewPos;
    }
}