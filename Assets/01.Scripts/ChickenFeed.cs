using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenFeed : MonoBehaviour
{
    float fallSpeed = 1f;
    bool isFloor = false;
    void Start()
    {
        
    }

    void Update()
    {
        if(!isFloor)
        {
            transform.position += Vector3.down * fallSpeed * Time.deltaTime;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Floor"))
        {
            isFloor = true;
        }
    }
    private void OnMouseDown()
    {
        Destroy(gameObject);
    }
}
