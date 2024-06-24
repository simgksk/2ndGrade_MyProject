using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Healer : MonoBehaviour
{
    [SerializeField] GameObject feedPrefab;
    [SerializeField] Transform feedPos;

    Transform feedParent;
    HPBarManager hpBarManager;

    void Start()
    {
        hpBarManager = FindObjectOfType<HPBarManager>();
        hpBarManager.SetupHPBar(transform);

        feedParent = new GameObject("HealerFeed").transform;
        StartCoroutine(FeedSpwan());
    }
    private void OnDestroy()
    {
        hpBarManager.RemoveHPBar(transform);
    }
    IEnumerator FeedSpwan()
    {
        while (true)
        {
            float spawnTime = 5f;
            yield return new WaitForSeconds(spawnTime);

            Quaternion feedRotation = Quaternion.Euler(0, 90, 0);
            Vector3 spawnPosition = feedPos.TransformPoint(Vector3.zero);
            GameObject spawnedFeed = Instantiate(feedPrefab, spawnPosition, feedRotation);
            spawnedFeed.transform.SetParent(feedParent);

            StartCoroutine(MoveInSemiCircle(spawnedFeed));
        }
    }

    IEnumerator MoveInSemiCircle(GameObject obj)
    {
        float duration = 2f;
        float elapsedTime = 0f;
        float rdPosX = Random.Range(-1.5f, 1.5f);
        Vector3 startPos = feedPos.position;
        Vector3 endPos = startPos + new Vector3(rdPosX, -1f, 0); 

        while (elapsedTime < duration)
        {
            if (obj == null)
            {
                yield break;
            }

            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            float height = Mathf.Sin(Mathf.PI * t);

            obj.transform.position = Vector3.Lerp(startPos, endPos, t) + Vector3.up * height;
            yield return null;
        }
    }
}
