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
    
    [Header("PlayerPrefab")]
    [SerializeField] GameObject dealerPrefab;
    [SerializeField] GameObject healerPrefab;
    [SerializeField] GameObject tankerPrefab;

    [Header("Layer Mask")]
    [SerializeField] LayerMask floorLayerMask;
    [SerializeField] Transform imageSelectionUI;

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
    }
    private void Update()
    {
        if (currentSelectedPlayer == this && Input.GetMouseButton(0) && !IsPointerOverUI())
        {
            PlayerInstallation();
        }
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
                            newObj = Instantiate(dealerPrefab);
                            newObj.transform.position = cellCenter;
                            GameManager.instance.SubFeedCnt(dealerFeed);
                            break;
                        case PlayerType.healer:
                            newObj = Instantiate(healerPrefab);
                            newObj.transform.position = cellCenter;
                            GameManager.instance.SubFeedCnt(healerFeed);
                            break;
                        case PlayerType.tanker:
                            newObj = Instantiate(tankerPrefab);
                            newObj.transform.position = cellCenter;
                            GameManager.instance.SubFeedCnt(tankerFeed);
                            break;
                    }

                    if (newObj != null)
                    {
                        floorCell.PlaceObj(newObj);
                        lastInstallationTime = Time.time;
                        StartCoroutine(DisableSelectionForCooldown());
                        AddFloatingImage(cellCenter);
                    }
                }

            }
        }
    }
    IEnumerator DisableSelectionForCooldown()
    {
        isCooldown = true;

        yield return new WaitForSeconds(3f);

        isCooldown = false;
    }
    void AddFloatingImage(Vector3 cellCenter)
    {
        Canvas canvas = FindObjectOfType<Canvas>();
        if (canvas != null)
        {
            GameObject floatingImage = Instantiate(floatingImagePrefab, imageSelectionUI);
            RectTransform rectTransform = floatingImage.GetComponent<RectTransform>();
            rectTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, cellCenter);

            rectTransform.sizeDelta = new Vector2(100f, 100f);

            StartCoroutine(ShrinkFloatingImage(rectTransform));
        }
    }
    IEnumerator ShrinkFloatingImage(RectTransform rectTransform)
    {
        float duration = 3f;
        float elapsedTime = 0;
        float startHeight = rectTransform.rect.height;
        Vector2 startSize = rectTransform.sizeDelta;

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
