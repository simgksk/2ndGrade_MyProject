using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Dealer : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletPos;

    [SerializeField] Transform playerTrm;
    [SerializeField] GameObject target;

    Transform bulletParent;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        bulletParent = new GameObject("Bullet").transform;
        StartCoroutine(BulletSpawn());
    }

    void Update()
    {
        RaycastHit hit;

        Debug.DrawRay(playerTrm.position, (target.transform.position - playerTrm.position).normalized * 100f, Color.green);

        if (Physics.Raycast(playerTrm.position, (target.transform.position - playerTrm.position).normalized, out hit))
        {
            Debug.Log("Hit object: " + hit.collider.name);
        }

    }

    IEnumerator BulletSpawn()
    {
        while (true)
        {
            anim.SetBool("isAttack", false);

            float spawnTime = 5f;
            yield return new WaitForSeconds(spawnTime);

            Quaternion bulletRotation = Quaternion.Euler(0, 90, 0);
            Vector3 spawnPosition = bulletPos.TransformPoint(Vector3.zero);
            GameObject spawnedBullet = Instantiate(bulletPrefab, spawnPosition, bulletRotation);
            spawnedBullet.transform.SetParent(bulletParent);

            anim.SetBool("isAttack", true);

            yield return new WaitForSeconds(.5f);
        }
    }

}
