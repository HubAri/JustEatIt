using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemys : MonoBehaviour
{
    public GameObject[] enemys;
    public GameObject[] powerups;
    public GameObject[] quickEnemies;

    private Vector2 screenBounds;

    private void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnPowerUp());
        StartCoroutine(SpawnQuickEnemy());

    }

    private void InitEnemy(GameObject enemy)
    {

        int r = UnityEngine.Random.Range(0, 4);
        float x;
        float y;
        // divide spawn area into 4 parts
        if (r == 0)
        {
            x = UnityEngine.Random.Range(-2f * screenBounds.x, -1.5f * screenBounds.x);
            y = UnityEngine.Random.Range(-screenBounds.y, screenBounds.y);

        }
        else if (r == 1)
        {
            x = UnityEngine.Random.Range(1.5f * screenBounds.x, 2f * screenBounds.x);
            y = UnityEngine.Random.Range(-screenBounds.y, screenBounds.y);
        }
        else if (r == 2)
        {
            x = UnityEngine.Random.Range(-screenBounds.x, screenBounds.x);
            y = UnityEngine.Random.Range(-1.5f * screenBounds.y, -2f * screenBounds.y);
        }
        else
        {
            x = UnityEngine.Random.Range(-screenBounds.x, screenBounds.x);
            y = UnityEngine.Random.Range(1.5f * screenBounds.y, 2f * screenBounds.y);
        }
        Vector2 spawnVector = new Vector2(x, y);
        GameObject obj = Instantiate(enemy, spawnVector, Quaternion.identity) as GameObject;
        obj.transform.parent = gameObject.transform;

    }


    IEnumerator SpawnEnemy()
    {
        // wait 5 - 10 sec
        yield return new WaitForSeconds(UnityEngine.Random.Range(5f, 10f) * 80 * Time.fixedDeltaTime); //set random time to spawn

        int i = UnityEngine.Random.Range(0, enemys.Length);
        InitEnemy(enemys[i]);

        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnQuickEnemy()
    {
        // wait 5 - 10 sec
        yield return new WaitForSeconds(UnityEngine.Random.Range(5f, 10f) * 80 * Time.fixedDeltaTime); //set random time to spawn

        int i = UnityEngine.Random.Range(0, quickEnemies.Length);
        InitEnemy(quickEnemies[i]);

        StartCoroutine(SpawnQuickEnemy());
    }



    IEnumerator SpawnPowerUp()
    {
        // wait 10 - 20 sec
        yield return new WaitForSeconds(UnityEngine.Random.Range(10f, 20f) * 80 * Time.fixedDeltaTime); //set random time to spawn

        int i = UnityEngine.Random.Range(0, powerups.Length);
        InitEnemy(powerups[i]);

        StartCoroutine(SpawnPowerUp());
    }

}