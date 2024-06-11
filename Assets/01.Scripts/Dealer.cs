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

            /*GameObject spawnedBullet = Instantiate(bulletPrefab, bulletPos.position, bulletPos.rotation);
            spawnedBullet.transform.SetParent(bulletPos);*/

            /*Debug.Log("Bullet Position: " + bulletPos.position);
            Debug.Log("Bullet Rotation: " + bulletPos.rotation.eulerAngles);*/

            // 총알을 bulletPos 위치에 생성합니다.
            Quaternion bulletRotation = Quaternion.Euler(0, 90, 0);
            GameObject spawnedBullet = Instantiate(bulletPrefab, bulletPos.position, bulletRotation);
            //Debug.Log("Spawned Bullet Position: " + spawnedBullet.transform.position);
        }
    }
}
