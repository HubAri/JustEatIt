using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArrowSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject arrowObject;

    private Vector2 screenBounds;
    private float objectHeight;

    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        objectHeight = arrowObject.GetComponent<SpriteRenderer>().bounds.extents.y; //extents = size of height / 2

        
    }

    public void InstantiateArrow(int region, GameObject enemy)
    {
        GameObject arrowObj = null;

        if (region == 0)  // left
        {
            Vector2 spawnPosition = new Vector2(-screenBounds.x + objectHeight, enemy.transform.position.y);
            arrowObj = Instantiate(arrowObject, spawnPosition, Quaternion.identity);
            arrowObj.transform.Rotate(0f, 0f, 90f);

        }
        else if (region == 1)  // right
        {
            Vector2 spawnPosition = new Vector2(screenBounds.x - objectHeight, enemy.transform.position.y);
            arrowObj = Instantiate(arrowObject, spawnPosition, Quaternion.identity);
            arrowObj.transform.Rotate(0f, 0f, -90f);

        }
        else if (region == 2)  // bottom
        {
            Vector2 spawnPosition = new Vector2(enemy.transform.position.x, -screenBounds.y+objectHeight);
            arrowObj = Instantiate(arrowObject, spawnPosition, Quaternion.identity);
            arrowObj.transform.Rotate(0f, 0f, 180f);

        }
        else  // top
        {
            Vector2 spawnPosition = new Vector2(enemy.transform.position.x, screenBounds.y - objectHeight);
            arrowObj = Instantiate(arrowObject, spawnPosition, Quaternion.identity);

        }

        EnemyArrowMovement arrowMovement = arrowObj.GetComponent<EnemyArrowMovement>();
        arrowMovement.SetRegion(region);
        arrowMovement.SetTargetEnemy(enemy);
    }

}
