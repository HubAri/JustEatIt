using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCollectibles : MonoBehaviour
{
    public GameObject neutral;
    public GameObject positive;
    public GameObject negative;

    private List<GameObject> posList = new();
    private List<GameObject> negList = new();
    private Vector2 screenBounds;
    private float x;
    private float y;


    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

        SpawnNeut();
        SpawnPos();
        SpawnNeg();
    }

    void InitCollectible(GameObject collectible)
    {
        x = UnityEngine.Random.Range(-screenBounds.x + collectible.transform.GetComponent<SpriteRenderer>().bounds.extents.x, screenBounds.x - collectible.transform.GetComponent<SpriteRenderer>().bounds.extents.x);
        y = UnityEngine.Random.Range(-screenBounds.y + collectible.transform.GetComponent<SpriteRenderer>().bounds.extents.y, screenBounds.y - collectible.transform.GetComponent<SpriteRenderer>().bounds.extents.y);
        GameObject obj = Instantiate(collectible, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
        obj.transform.parent = gameObject.transform;

        if (collectible.name == positive.name)
        {
            posList.Insert(0, obj);   
        }
        else if (collectible.name == negative.name)
        {
            negList.Insert(0, obj);
        }
    }

    public void SpawnPos()
    {
        StartCoroutine(SpawnPosColl());
    }
    public void SpawnNeut()
    {
        InitCollectible(neutral);
    }
    public void SpawnNeg()
    {
        StartCoroutine(SpawnNegColl());
    }
    IEnumerator SpawnPosColl()
    {
        // wait 8 - 20 sec
        yield return new WaitForSeconds(UnityEngine.Random.Range(8f, 20f) * 80 * Time.fixedDeltaTime); //set random time to spawn
        InitCollectible(positive);
        yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 2f) * 80 * Time.fixedDeltaTime); //set time to destroy

        try
        {
            Destroy(posList[0]);
        }
        catch (NullReferenceException) { }

        posList.RemoveAt(posList.Count - 1);
        StartCoroutine(SpawnPosColl());
    }


    IEnumerator SpawnNegColl()
    {
        yield return new WaitForSeconds(UnityEngine.Random.Range(5f, 15f) * 80 * Time.fixedDeltaTime); //set random time to spawn
        if (negList.Count == 0)
            InitCollectible(negative);
        yield return new WaitForSeconds(UnityEngine.Random.Range(5f, 10f) * 80 * Time.fixedDeltaTime); //set time to destroy

        try
        {
            Destroy(negList[0]);
        }
        catch (NullReferenceException) { }

        negList.RemoveAt(negList.Count - 1);
        StartCoroutine(SpawnNegColl());
    }

}
