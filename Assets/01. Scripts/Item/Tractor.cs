using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tractor : MonoBehaviour
{
    float tractorSpeed = 5f;
    bool tractorMove = false;
    void Start()
    {
        
    }

    void Update()
    {
        if (tractorMove)
        {
            transform.position += Vector3.right * tractorSpeed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Dino"))
        {
            tractorMove = true;
        }

        if (collision.gameObject.CompareTag("TractorLine"))
        {
            Destroy(gameObject);
        }
        
    }
    
}
