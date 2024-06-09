using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tractor : MonoBehaviour
{
    float tractorSpeed = 5f;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("TractorLine"))
        {
            Destroy(gameObject);
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Dino"))
        {
            transform.position += Vector3.right * tractorSpeed * Time.deltaTime;
        }
    }
}
