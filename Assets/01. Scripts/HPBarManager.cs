using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBarManager : MonoBehaviour
{
    [SerializeField] GameObject hpBarPrefab;
    Dictionary<Transform, GameObject> hpBars = new Dictionary<Transform, GameObject>();

    public void SetupHPBar(Transform playerTransform)
    {
        GameObject hpBarInstance = Instantiate(hpBarPrefab, playerTransform.position + new Vector3(0, 2, 0), Quaternion.identity);
        HPbar hpBarScript = hpBarInstance.GetComponent<HPbar>();
        hpBarScript.target = playerTransform;
        hpBarScript.offSet = new Vector3(0.3f, 1.7f, 0);

        hpBars.Add(playerTransform, hpBarInstance);
    }
    public void RemoveHPBar(Transform playerTransform)
    {
        if (hpBars.ContainsKey(playerTransform))
        {
            Destroy(hpBars[playerTransform]);
            hpBars.Remove(playerTransform);
        }
    }
    public HPbar GetHPBar(Transform playerTransform)
    {
        if (hpBars.ContainsKey(playerTransform))
            return hpBars[playerTransform].GetComponent<HPbar>();
        else
            return null;
    }
}
