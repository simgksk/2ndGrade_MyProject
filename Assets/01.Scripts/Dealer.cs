using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Dealer : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletPos;
    [SerializeField] GameObject healthBarPrefab;
    GameObject healthBar;
    HealthBar healthBarScript;
    float maxHealth = 100f;
    float currentHealth;

    void Start()
    {
        /*currentHealth = maxHealth;
        healthBar = Instantiate(healthBarPrefab, transform.position + Vector3.up * 2, Quaternion.identity, transform);
        healthBarScript = healthBar.GetComponent<HealthBar>();
        healthBarScript.SetMaxHealth(maxHealth);*/

        StartCoroutine(BulletSpawn());
    }
    void Update()
    {
        //healthBar.transform.position = transform.position + Vector3.up * 2;
    }
    IEnumerator BulletSpawn()
    {
        while (true)
        {
            float spawnTime = 5f;
            yield return new WaitForSeconds(spawnTime);

            Quaternion bulletRotation = Quaternion.Euler(0, 90, 0);
            GameObject spawnedBullet = Instantiate(bulletPrefab, bulletPos.position, bulletRotation);
            spawnedBullet.transform.SetParent(bulletPos);
        }
    }

}
