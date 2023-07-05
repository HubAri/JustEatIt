using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArrowMovement : MonoBehaviour
{
    private GameObject enemy;
    private int region;

    public void SetTargetEnemy(GameObject enemyTransform)
    {
        enemy = enemyTransform;
    }
    public void SetRegion(int r)
    {
        region = r;
    }


    void Update()
    {
        if (enemy != null)
        {
            if (region == 0 || region == 1) // enemy left right
            {
                // Only Move up down
                this.gameObject.transform.position = new Vector2(this.gameObject.transform.position.x, enemy.transform.position.y);
            }
            else  // enemy top bottom
            {
                // Only Move left right
                this.gameObject.transform.position = new Vector2(enemy.transform.position.x, this.gameObject.transform.position.y);
            }
            
        }
        
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject == enemy)
        {
            Destroy(this.gameObject);
        }
    }
}
