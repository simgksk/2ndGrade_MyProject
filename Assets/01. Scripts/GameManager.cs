using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [Header("Feed")]
    [SerializeField] TextMeshProUGUI feedCntTxt;
    int feedCnt = 100;
    public int currentFeed = 0;

    [Header("Panel")]
    [SerializeField] CanvasGroup gameOverPanelCanvasGroup;
    float fadeDuration = 1f;

    [Header("Timer")]
    [SerializeField] TextMeshProUGUI timeTxt;
    float elapsedTime;

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
        gameOverPanelCanvasGroup.alpha = 0;
        gameOverPanelCanvasGroup.gameObject.SetActive(false);
        UpdateFeedText();
    }
    private void Update()
    {
        elapsedTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timeTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    private void UpdateFeedText()
    {
        feedCntTxt.text = feedCnt.ToString();
        currentFeed = feedCnt;
    }
    public void AddFeedCnt(int feed)
    {
        feedCnt += feed;
        UpdateFeedText();
    }
    public void SubFeedCnt(int feed)
    {
        feedCnt -= feed;
        UpdateFeedText();
    }
    public void ShowGameOverPanel()
    {
        gameOverPanelCanvasGroup.gameObject.SetActive(true);
        StartCoroutine(FadeIn());
    }
    IEnumerator FadeIn()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            gameOverPanelCanvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        gameOverPanelCanvasGroup.alpha = 1f;
    }
}
