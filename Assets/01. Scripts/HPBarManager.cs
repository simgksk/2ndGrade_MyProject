using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBarManager : MonoBehaviour
{
    [SerializeField] GameObject chicken_HpBarPrefab;
    [SerializeField] GameObject dino_HpBarPrefab;
    Dictionary<Transform, GameObject> hpBars = new Dictionary<Transform, GameObject>();

    public HPbar SetupHPBar(Transform targetTransform, bool isEnemy = false)
    {
        GameObject hpBarPrefab = isEnemy ? dino_HpBarPrefab : chicken_HpBarPrefab;
        Vector3 offset = isEnemy ? new Vector3(0, 2, 0) : new Vector3(0.3f, 1.7f, 0);

        GameObject hpBarInstance = Instantiate(hpBarPrefab, targetTransform.position + offset, Quaternion.identity);
        HPbar hpBarScript = hpBarInstance.GetComponent<HPbar>();
        hpBarScript.target = targetTransform;
        hpBarScript.offSet = offset;

        hpBars.Add(targetTransform, hpBarInstance);

        return hpBarScript;
    }

    public void RemoveHPBar(Transform targetTransform)
    {
        if (hpBars.ContainsKey(targetTransform))
        {
            Destroy(hpBars[targetTransform]);
            hpBars.Remove(targetTransform);
        }
    }

    public HPbar GetHPBar(Transform targetTransform)
    {
        if (hpBars.ContainsKey(targetTransform))
            return hpBars[targetTransform].GetComponent<HPbar>();
        else
            return null;
    }
}

