using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tanker : MonoBehaviour
{
    float damage = 1f;
    HPbar hpBar;
    HPBarManager hpBarManager;

    void Start()
    {
        hpBarManager = FindObjectOfType<HPBarManager>();
        if (hpBarManager != null)
        {
            hpBar = hpBarManager.SetupHPBar(transform);
        }
    }
    private void OnDestroy()
    {
        hpBarManager.RemoveHPBar(transform);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Dino"))
        {
            hpBar.Damage(damage);
            if (hpBar.currentHP <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

}
