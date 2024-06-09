using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DinoSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] dinoPrefabs;
    void Start()
    {
        StartCoroutine(SpawnDino());
    }
    void Update()
    {
        
    }

    IEnumerator SpawnDino()
    {
        while (true)
        {
            float rdSpawnTime = Random.Range(2f, 5f);
            yield return new WaitForSeconds(rdSpawnTime);

            int rdIndex = Random.Range(0, dinoPrefabs.Length);
            GameObject selectedPrefab = dinoPrefabs[rdIndex];

            Transform childTransform = transform.GetChild(Random.Range(0, transform.childCount));
            Instantiate(selectedPrefab, childTransform.position, Quaternion.identity, transform);
        }
    }
    
}
