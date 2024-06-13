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

    [SerializeField] GameObject dealer_border;
    [SerializeField] GameObject healer_border;
    [SerializeField] GameObject tanker_border;

    void Start()
    {
        dealer_border.SetActive(false); 
        healer_border.SetActive(false); 
        tanker_border.SetActive(false); 
    }

    public void OnCharacterClicked()
    {
        switch (playerType)
        {
            case PlayerType.dealer:
                dealer_border.SetActive(true);
                healer_border.SetActive(false);
                tanker_border.SetActive(false);

                if (!dealer_border)
                {
                    bool isActive1 = dealer_border.activeSelf;
                    dealer_border.SetActive(!isActive1);
                }
                break;

            case PlayerType.healer:
                dealer_border.SetActive(false);
                healer_border.SetActive(true);
                tanker_border.SetActive(false);
                
                if (!healer_border)
                {
                    bool isActive2 = healer_border.activeSelf;
                    healer_border.SetActive(!isActive2);
                }
                break;

            case PlayerType.tanker:
                dealer_border.SetActive(false);
                healer_border.SetActive(false);
                tanker_border.SetActive(true);

                if (!tanker_border)
                {
                    bool isActive3 = tanker_border.activeSelf;
                    tanker_border.SetActive(!isActive3);
                }
                break;
            
        }
    }
}
