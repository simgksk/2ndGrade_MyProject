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
    public int currentFeed = 0;

    [SerializeField] CanvasGroup gameOverPanelCanvasGroup;
    float fadeDuration = 1f;

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
