using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Dealer : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletPos;
    void Start()
    {
        StartCoroutine(BulletSpawn());
    }
    void Update()
    {
        
    }
    IEnumerator BulletSpawn()
    {
        while (true)
        {
            float spawnTime = 5f;
            yield return new WaitForSeconds(spawnTime);

            GameObject spawnedBullet = Instantiate(bulletPrefab, bulletPos.position, bulletPos.rotation);
            spawnedBullet.transform.SetParent(bulletPos);
        }
    }
}
