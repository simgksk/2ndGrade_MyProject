using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dinosaur : MonoBehaviour
{
    float moveSpeed = .5f;
    bool isMoving = true;
    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        StartCoroutine(DinoMove());
    }

    void Update()
    {
    }

    IEnumerator DinoMove()
    {
        while (true)
        {
            if (isMoving)
            {
                float moveDuration = 1f;
                float elapsedTime = 0f;

                while (elapsedTime < moveDuration)
                {
                    transform.position += Vector3.left * moveSpeed * Time.deltaTime;
                    elapsedTime += Time.deltaTime;
                    anim.SetBool("isWalking", true);
                    yield return null; 
                }
            }
            isMoving = !isMoving;
            anim.SetBool("isWalking", false);
            yield return new WaitForSeconds(.5f);
        }
    }
}
