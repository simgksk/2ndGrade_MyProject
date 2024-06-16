using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum PlayerType
{
    dealer,
    healer,
    tanker
}
public class SelectPlayer : MonoBehaviour
{
    [SerializeField] PlayerType playerType;

    [Header("Border")]
    [SerializeField] GameObject dealer_border;
    [SerializeField] GameObject healer_border;
    [SerializeField] GameObject tanker_border;

    [Header("Effect")]
    [SerializeField] GameObject floatingImagePrefab;
    [SerializeField] GameObject dealer_NeedFeedImage;
    [SerializeField] GameObject healer_NeedFeedImage;
    [SerializeField] GameObject tanker_NeedFeedImage;
    [SerializeField] Transform dealerUI;
    [SerializeField] Transform healerUI;
    [SerializeField] Transform tankerUI;
    
    [Header("PlayerPrefab")]
    [SerializeField] GameObject dealerPrefab;
    [SerializeField] GameObject healerPrefab;
    [SerializeField] GameObject tankerPrefab;

    [Header("Layer Mask")]
    [SerializeField] LayerMask floorLayerMask;

    static SelectPlayer currentSelectedPlayer;

    int dealerFeed = 100;
    int healerFeed = 50;
    int tankerFeed = 150;

    float lastInstallationTime = -3f;
    bool isCooldown = false;

    void Start()
    {
        dealer_border.SetActive(false); 
        healer_border.SetActive(false); 
        tanker_border.SetActive(false);

        dealer_NeedFeedImage.SetActive(false);
        healer_NeedFeedImage.SetActive(false);
        tanker_NeedFeedImage.SetActive(false);
    }
    private void Update()
    {
        if (currentSelectedPlayer == this && Input.GetMouseButton(0) && !IsPointerOverUI())
        {
            PlayerInstallation();
        }
        UpdateFeedStatus();
    }

    private void PlayerInstallation()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= lastInstallationTime + 3f)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f, floorLayerMask))
            {
                FloorCell floorCell = hit.collider.GetComponent<FloorCell>();

                if (floorCell != null && !floorCell.IsOccupied())
                {
                    Vector3 cellCenter = floorCell.GetCenterPosition();
                    GameObject newObj = null;

                    switch (playerType)
                    {
                        case PlayerType.dealer:
                            if (GameManager.instance.currentFeed >= dealerFeed)
                            {
                                newObj = Instantiate(dealerPrefab);
                                newObj.transform.position = cellCenter;
                                GameManager.instance.SubFeedCnt(dealerFeed);
                                AddFloatingImage(dealerUI.transform);
                                OnCharacterClicked();
                            }
                            break;

                        case PlayerType.healer:
                            if (GameManager.instance.currentFeed >= healerFeed)
                            {
                                newObj = Instantiate(healerPrefab);
                                newObj.transform.position = cellCenter;
                                GameManager.instance.SubFeedCnt(healerFeed);
                                AddFloatingImage(healerUI.transform);
                                OnCharacterClicked();
                            }
                            break;

                        case PlayerType.tanker:
                            if (GameManager.instance.currentFeed >= tankerFeed)
                            {
                                newObj = Instantiate(tankerPrefab);
                                newObj.transform.position = cellCenter;
                                GameManager.instance.SubFeedCnt(tankerFeed);
                                AddFloatingImage(tankerUI.transform);
                                OnCharacterClicked();
                            }
                            break;
                    }

                    if (newObj != null)
                    {
                        floorCell.PlaceObj(newObj);
                        lastInstallationTime = Time.time;
                        StartCoroutine(DisableSelectionForCooldown());
                    }
                }

            }
        }
    }
    void UpdateFeedStatus()
    {
        /*dealer_border.SetActive(GameManager.instance.currentFeed >= dealerFeed);
        healer_border.SetActive(GameManager.instance.currentFeed >= healerFeed);
        tanker_border.SetActive(GameManager.instance.currentFeed >= tankerFeed);*/

        dealer_NeedFeedImage.SetActive(GameManager.instance.currentFeed < dealerFeed);
        healer_NeedFeedImage.SetActive(GameManager.instance.currentFeed < healerFeed);
        tanker_NeedFeedImage.SetActive(GameManager.instance.currentFeed < tankerFeed);

    }
    IEnumerator DisableSelectionForCooldown()
    {
        isCooldown = true;

        yield return new WaitForSeconds(3f);

        isCooldown = false;
    }
    void AddFloatingImage(Transform uiTransform)
    {
        GameObject floatingImage = Instantiate(floatingImagePrefab, uiTransform);
        RectTransform rectTransform = floatingImage.GetComponent<RectTransform>();

        rectTransform.anchoredPosition = Vector2.zero;

        StartCoroutine(ShrinkFloatingImage(rectTransform));
    }
    IEnumerator ShrinkFloatingImage(RectTransform rectTransform)
    {
        float duration = 3f;
        float elapsedTime = 0;
        float startHeight = rectTransform.rect.height;

        while (elapsedTime < duration)
        {
            float newY = Mathf.Lerp(startHeight, 0, elapsedTime / duration);
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, newY);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(rectTransform.gameObject);
    }
    public void OnCharacterClicked()
    {
        if (currentSelectedPlayer == this)
        {
            Deselect();
            currentSelectedPlayer = null;
        }
        else if(currentSelectedPlayer == null)
        {
            SelectedPlayer();
        }
        else
        {
            if (currentSelectedPlayer != null)
            {
                currentSelectedPlayer.Deselect();
            }
            SelectedPlayer();
        }
    }

    private void SelectedPlayer()
    {
        switch (playerType)
        {
            case PlayerType.dealer:
                dealer_border.SetActive(true);
                healer_border.SetActive(false);
                tanker_border.SetActive(false);
                break;

            case PlayerType.healer:
                dealer_border.SetActive(false);
                healer_border.SetActive(true);
                tanker_border.SetActive(false);
                break;

            case PlayerType.tanker:
                dealer_border.SetActive(false);
                healer_border.SetActive(false);
                tanker_border.SetActive(true);
                break;
        }
        currentSelectedPlayer = this;
    }

    public void Deselect()
    {
        if (currentSelectedPlayer != null)
        {
            switch (currentSelectedPlayer.playerType)
            {
                case PlayerType.dealer:
                    currentSelectedPlayer.dealer_border.SetActive(false);
                    break;
                case PlayerType.healer:
                    currentSelectedPlayer.healer_border.SetActive(false);
                    break;
                case PlayerType.tanker:
                    currentSelectedPlayer.tanker_border.SetActive(false);
                    break;
            }
        }
    }
    private bool IsPointerOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }
}
