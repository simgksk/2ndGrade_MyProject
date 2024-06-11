using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float bulletSpeed = 3f;
    void Start()
    {
        
    }

    void Update()
    {
        transform.position += Vector3.right * bulletSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Dino"))
        {
            Destroy(gameObject);
        }
        if(other.gameObject.CompareTag("TractorLine"))
        {
            Destroy(gameObject);
        }
    }
}
