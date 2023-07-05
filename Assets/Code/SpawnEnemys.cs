using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemys : MonoBehaviour
{
    public GameObject[] enemys;
    public GameObject[] powerups;

    [SerializeField]
    EnemyArrowSpawner enemyArrow;

    private Vector2 screenBounds;

    private void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnPowerUp());

    }

    private void InitEnemy(GameObject enemy)
    {

        int region = UnityEngine.Random.Range(0, 4);
        float x;
        float y;
        // divide spawn area into 4 parts
        if (region == 0)  // left
        {
            x = UnityEngine.Random.Range(-2f * screenBounds.x, -1.5f * screenBounds.x);
            y = UnityEngine.Random.Range(-screenBounds.y, screenBounds.y);

        }
        else if (region == 1)  // right
        {
            x = UnityEngine.Random.Range(1.5f * screenBounds.x, 2f * screenBounds.x);
            y = UnityEngine.Random.Range(-screenBounds.y, screenBounds.y);
        }
        else if (region == 2)  // bottom
        {
            x = UnityEngine.Random.Range(-screenBounds.x, screenBounds.x);
            y = UnityEngine.Random.Range(-1.5f * screenBounds.y, -2f * screenBounds.y);
        }
        else  // top
        {
            x = UnityEngine.Random.Range(-screenBounds.x, screenBounds.x);
            y = UnityEngine.Random.Range(1.5f * screenBounds.y, 2f * screenBounds.y);
        }
        Vector2 spawnVector = new Vector2(x, y);
        GameObject obj = Instantiate(enemy, spawnVector, Quaternion.identity) as GameObject;
        obj.transform.parent = gameObject.transform;

        if (enemy.CompareTag("Enemy")){
            // instantiate arrow
            enemyArrow.InstantiateArrow(region, obj);
        }

    }


    IEnumerator SpawnEnemy()
    {
        // wait 5 - 10 sec
        yield return new WaitForSeconds(UnityEngine.Random.Range(2f, 4f) * 80 * Time.fixedDeltaTime); //set random time to spawn

        int i = UnityEngine.Random.Range(0, enemys.Length);
        InitEnemy(enemys[i]);

        StartCoroutine(SpawnEnemy());
    }


    IEnumerator SpawnPowerUp()
    {
        // wait 15 - 30 sec
        yield return new WaitForSeconds(UnityEngine.Random.Range(15f, 30f) * 80 * Time.fixedDeltaTime); //set random time to spawn

        int i = UnityEngine.Random.Range(0, powerups.Length);
        InitEnemy(powerups[i]);

        StartCoroutine(SpawnPowerUp());
    }

}