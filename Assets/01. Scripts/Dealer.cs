using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Dealer : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletPos;

    Transform bulletParent;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        bulletParent = new GameObject("Bullet").transform;
        StartCoroutine(BulletSpawn());
    }
    
    IEnumerator BulletSpawn()
    {
        while (true)
        {
            anim.SetBool("isAttack", false);

            float spawnTime = 5f;
            yield return new WaitForSeconds(spawnTime);

            Quaternion bulletRotation = Quaternion.Euler(0, 90, 0);
            GameObject spawnedBullet = Instantiate(bulletPrefab, bulletPos.position, bulletRotation);
            spawnedBullet.transform.SetParent(bulletParent);

            anim.SetBool("isAttack", true);

            yield return new WaitForSeconds(.5f);
        }
    }

}
