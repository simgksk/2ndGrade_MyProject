using System.Collections;
using UnityEngine;

public class Dealer : MonoBehaviour
{
    [Header("Bullet")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletPos;
    [SerializeField] Transform playerTrm;
    [SerializeField] LayerMask targetLine;

    HPBarManager hpBarManager;
    Transform bulletParent;
    Animator anim;
    bool isBulletSpawned;

    void Start()
    {
        hpBarManager = FindObjectOfType<HPBarManager>();
        hpBarManager.SetupHPBar(transform);

        anim = GetComponent<Animator>();
        bulletParent = new GameObject("Bullet").transform;
        isBulletSpawned = false;
    }
    private void OnDestroy()
    {
        hpBarManager.RemoveHPBar(transform);
    }

    void Update()
    {
        CheckRay();
    }

    private void CheckRay()
    {
        RaycastHit hit;
        float maxDistance = 20f;

        if (Physics.Raycast(playerTrm.position, playerTrm.forward, out hit, maxDistance, targetLine))
        {
            maxDistance = hit.distance;
            if (hit.collider.CompareTag("Dino") && !isBulletSpawned)
            {
                StartCoroutine(BulletSpawn());
            }
        }

        Debug.DrawRay(playerTrm.position, playerTrm.forward * maxDistance, Color.green);
    }

    IEnumerator BulletSpawn()
    {
        isBulletSpawned = true;

        anim.SetBool("isAttack", true); 
        yield return new WaitForSeconds(0.1f);

        Quaternion bulletRotation = Quaternion.LookRotation(playerTrm.forward);
        Vector3 spawnPosition = bulletPos.position;

        GameObject spawnedBullet = Instantiate(bulletPrefab, spawnPosition, bulletRotation);
        spawnedBullet.transform.SetParent(bulletParent);

        yield return new WaitForSeconds(1f); 

        anim.SetBool("isAttack", false);

        isBulletSpawned = false;
    }
}
