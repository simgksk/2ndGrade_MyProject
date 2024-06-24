using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FeedSpawner : MonoBehaviour
{
    [SerializeField] GameObject feedPrefab;
    void Start()
    {
        StartCoroutine(FeedSpawn());
    }

    IEnumerator FeedSpawn()
    {
        while (true)
        {
            float rdSpawnTime = Random.Range(7f, 10f);
            yield return new WaitForSeconds(rdSpawnTime);

            Transform childTransform = transform.GetChild(Random.Range(0, transform.childCount));

            if (childTransform.childCount == 0)
            {
                GameObject spawnedFeed = Instantiate(feedPrefab, childTransform.position, transform.rotation);
                spawnedFeed.transform.SetParent(childTransform);
            }
        }
    }

}
