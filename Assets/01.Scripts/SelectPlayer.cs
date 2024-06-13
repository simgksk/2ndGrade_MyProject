using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    static SelectPlayer currentSelectedPlayer;

    void Start()
    {
        dealer_border.SetActive(false); 
        healer_border.SetActive(false); 
        tanker_border.SetActive(false); 
    }
    private void Update()
    {
        if (currentSelectedPlayer == this && Input.GetMouseButton(0))
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

            if (Physics.Raycast(ray, out hit, 100f))
            {
                Vector3 hitPos = hit.point;
                hitPos.y = 0.3f;

                switch (playerType)
                {
                    case PlayerType.dealer:
                        GameObject _dealer = Instantiate(dealerPrefab);
                        _dealer.transform.position = hitPos;
                        break;
                    case PlayerType.healer:
                        GameObject _hearler = Instantiate(healerPrefab);
                        _hearler.transform.position = hitPos;
                        break;
                    case PlayerType.tanker:
                        GameObject _tanker = Instantiate(tankerPrefab);
                        _tanker.transform.position = hitPos;
                        break;
                }
            }
        }
    }

    public void OnCharacterClicked()
    {
        if (currentSelectedPlayer != null)
        {
            currentSelectedPlayer.Deselect();
        }

        currentSelectedPlayer = this;

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
    }

    public void Deselect()
    {
        switch (playerType)
        {
            case PlayerType.dealer:
                dealer_border.SetActive(false);
                break;
            case PlayerType.healer:
                healer_border.SetActive(false);
                break;
            case PlayerType.tanker:
                tanker_border.SetActive(false);
                break;
        }
    }
}
