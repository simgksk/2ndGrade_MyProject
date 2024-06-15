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
    [SerializeField] Floor floor;

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
                Vector2Int gridPos = GetGridPosition(hitPos);

                if (IsWithinFloor(gridPos))
                {
                    Vector3 cellCentorPos = floor.GetCellCenterPosition(gridPos);

                    switch (playerType)
                    {
                        case PlayerType.dealer:
                            GameObject _dealer = Instantiate(dealerPrefab);
                            _dealer.transform.position = cellCentorPos;
                            GameManager.instance.SubFeedCnt(dealerFeed);
                            break;
                        case PlayerType.healer:
                            GameObject _hearler = Instantiate(healerPrefab);
                            _hearler.transform.position = cellCentorPos;
                            GameManager.instance.SubFeedCnt(healerFeed);
                            break;
                        case PlayerType.tanker:
                            GameObject _tanker = Instantiate(tankerPrefab);
                            _tanker.transform.position = cellCentorPos;
                            GameManager.instance.SubFeedCnt(tankerFeed);
                            break;
                    }
                }

            }
        }
    }

    private Vector2Int GetGridPosition(Vector3 hitPos)
    {
        Vector3 localPos = hitPos - floor.transform.position;

        int x = Mathf.FloorToInt(localPos.x / floor.cellSize.x);
        int z = Mathf.FloorToInt(localPos.z / floor.cellSize.y);

        return new Vector2Int(x, z);
    }

    bool IsWithinFloor(Vector2Int gridPos)
    {
        return gridPos.x >= 0 && gridPos.x < floor.floorSize.x && gridPos.y >= 0 && gridPos.y < floor.floorSize.y;
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
