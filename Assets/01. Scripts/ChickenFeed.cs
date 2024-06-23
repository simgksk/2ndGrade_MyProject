using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenFeed : MonoBehaviour
{
    float fallSpeed = 1f;
    bool isFloor = false;
    int feedCnt = 25;
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
        if (GameManager.instance != null)
        {
            GameManager.instance.AddFeedCnt(feedCnt);
            Destroy(gameObject);
        }
    }
}
