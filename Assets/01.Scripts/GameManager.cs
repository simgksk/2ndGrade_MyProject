using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField] TextMeshProUGUI feedCntTxt;
    int feedCnt = 100;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start()
    {
        UpdateFeedText();
    }

    private void UpdateFeedText()
    {
        feedCntTxt.text = feedCnt.ToString();
    }
    public void AddFeedCnt(int feed)
    {
        feedCnt += feed;
        UpdateFeedText();
    }
}
