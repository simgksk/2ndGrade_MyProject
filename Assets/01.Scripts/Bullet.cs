using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float bulletSpeed = 5f;
    void Start()
    {
        
    }

    void Update()
    {
        transform.position += Vector3.right * bulletSpeed * Time.deltaTime;
    }
}
