using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Supporter : MonoBehaviour
{
    [SerializeField] GameObject feedPrefab;
    [SerializeField] Transform feedPos;

    Transform feedParent;
    HPBarManager hpBarManager;
    HPbar hpBar;

    float damage = 2f;
    float spawnTime = 5f;

    void Start()
    {
        hpBarManager = FindObjectOfType<HPBarManager>();
        if (hpBarManager != null)
        {
            hpBar = hpBarManager.SetupHPBar(transform);
        }
        else
        {
            Debug.LogError("HPBarManager를 찾을 수 없습니다.");
        }

        feedParent = new GameObject("HealerFeed").transform;
        StartCoroutine(FeedSpwan());
    }

    private void OnDestroy()
    {
        if (hpBarManager != null)
        {
            hpBarManager.RemoveHPBar(transform);
        }
    }

    IEnumerator FeedSpwan()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);

            Quaternion feedRotation = Quaternion.Euler(0, 90, 0);
            Vector3 spawnPosition = feedPos.TransformPoint(Vector3.zero);
            GameObject spawnedFeed = Instantiate(feedPrefab, spawnPosition, feedRotation);
            spawnedFeed.transform.SetParent(feedParent);

            StartCoroutine(MoveInSemiCircle(spawnedFeed));
        }
    }

    IEnumerator MoveInSemiCircle(GameObject obj)
    {
        float duration = 2f;
        float elapsedTime = 0f;
        float rdPosX = Random.Range(-1.5f, 1.5f);
        Vector3 startPos = feedPos.position;
        Vector3 endPos = startPos + new Vector3(rdPosX, -1f, 0);

        while (elapsedTime < duration)
        {
            if (obj == null)
            {
                yield break;
            }

            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            float height = Mathf.Sin(Mathf.PI * t);

            obj.transform.position = Vector3.Lerp(startPos, endPos, t) + Vector3.up * height;
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Dino"))
        {
            if (hpBar != null)
            {
                hpBar.Damage(damage);
                if (hpBar.currentHP <= 0)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                Debug.LogError("hpBar가 초기화되지 않았습니다.");
            }
        }
    }
}
