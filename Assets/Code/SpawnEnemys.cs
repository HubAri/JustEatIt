using System.Collections;
using UnityEngine;

public class SpawnEnemys : MonoBehaviour
{
    public GameObject[] enemys;
    public GameObject[] powerups;
    public GameObject[] quickEnemies;

    [SerializeField]
    EnemyArrowSpawner enemyArrow;

    private Vector2 screenBounds;

    private void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        StartCoroutine(SpawnEnemy());
        StartCoroutine(SpawnPowerUp());
        StartCoroutine(SpawnSlownessPowerUp());
        StartCoroutine(SpawnQuickEnemy());

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

        if (enemy.CompareTag("Enemy") || enemy.CompareTag("QuickEnemy"))
        {
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

        InitEnemy(powerups[0]);

        StartCoroutine(SpawnPowerUp());
    }

    IEnumerator SpawnSlownessPowerUp()
    {
        // wait 10 - 20 sec
        yield return new WaitForSeconds(UnityEngine.Random.Range(10f, 15f) * 80 * Time.fixedDeltaTime); //set random time to spawn

        InitEnemy(powerups[1]);

        StartCoroutine(SpawnSlownessPowerUp());
    }
}