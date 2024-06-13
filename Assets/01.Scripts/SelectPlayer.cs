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
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f, floorLayerMask))
            {
                Vector3 hitPos = hit.point;
                hitPos.y = 0.3f;

                switch (playerType)
                {
                    case PlayerType.dealer:
                        GameObject _dealer = Instantiate(dealerPrefab);
                        _dealer.transform.position = hitPos;
                        GameManager.instance.SubFeedCnt(dealerFeed);
                        break;
                    case PlayerType.healer:
                        GameObject _hearler = Instantiate(healerPrefab);
                        _hearler.transform.position = hitPos;
                        GameManager.instance.SubFeedCnt(healerFeed);
                        break;
                    case PlayerType.tanker:
                        GameObject _tanker = Instantiate(tankerPrefab);
                        _tanker.transform.position = hitPos;
                        GameManager.instance.SubFeedCnt(tankerFeed);
                        break;
                }
            }
        }
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
