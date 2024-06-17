using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Dealer : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletPos;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(BulletSpawn());
    }
    
    IEnumerator BulletSpawn()
    {
        while (true)
        {
            anim.SetBool("isAttack", true);
            float spawnTime = 5f;
            yield return new WaitForSeconds(spawnTime);

            Quaternion bulletRotation = Quaternion.Euler(0, 90, 0);
            GameObject spawnedBullet = Instantiate(bulletPrefab, bulletPos.position, bulletRotation);
            spawnedBullet.transform.SetParent(bulletPos);
        }
    }

}
