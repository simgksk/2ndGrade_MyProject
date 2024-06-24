using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPbar : MonoBehaviour
{
    [SerializeField] Transform hpBar;
    [SerializeField] Transform hpBarBackground;
    public Transform target;
    public Vector3 offSet;

    float maxHP = 100f;
    public float currentHP;

    private void Start()
    {
        currentHP = maxHP;
        UpdateHPBar();
    }
    private void Update()
    {
        transform.position = target.position + offSet;
        transform.LookAt(transform.position + Camera.main.transform.rotation * Vector3.forward, 
            Camera.main.transform.rotation * Vector3.up);
    }
    public void Damage(float damage)
    {
        currentHP -= damage;
        if (currentHP > maxHP)
        {
            currentHP = maxHP;
        }
        UpdateHPBar();
    }
    private void UpdateHPBar()
    {
        float hpRatio = currentHP / maxHP;
        float newScaleX = hpRatio; 

        float leftEdgeX = hpBarBackground.position.x + hpBarBackground.localScale.x * -0.5f;

        hpBar.position = new Vector3(leftEdgeX - newScaleX * -0.5f, hpBar.position.y, hpBar.position.z);

        hpBar.localScale = new Vector3(newScaleX, 1, 1);
    }
}
