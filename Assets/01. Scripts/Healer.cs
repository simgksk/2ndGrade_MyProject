using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healer : MonoBehaviour
{
    [SerializeField] GameObject feedPrefab;
    [SerializeField] Transform feedPos;

    Transform feedParent;

    void Start()
    {
        StartCoroutine(FeedSpwan());
        feedParent = new GameObject("HealerFeed").transform;
    }

    IEnumerator FeedSpwan()
    {
        while (true)
        {
            float spawnTime = 5f;
            yield return new WaitForSeconds(spawnTime);

            Quaternion feedRotation = Quaternion.Euler(0, 90, 0);
            GameObject spawnedFeed = Instantiate(feedPrefab, feedPos.position, feedRotation);
            spawnedFeed.transform.SetParent(feedParent);

            // 생성된 오브젝트가 위로 반원을 그리며 이동하도록 함
            StartCoroutine(MoveInSemiCircle(spawnedFeed));
        }
    }

    IEnumerator MoveInSemiCircle(GameObject obj)
    {
        float duration = 2f; // 반원을 그리는데 걸리는 시간
        float elapsedTime = 0f;
        Vector3 startPos = obj.transform.position;
        Vector3 endPos = startPos + new Vector3(0, 2f, 2f); // 적당한 위치로 설정

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            float height = Mathf.Sin(Mathf.PI * t); // 반원 궤적을 위한 sin 함수

            obj.transform.position = Vector3.Lerp(startPos, endPos, t) + Vector3.up * height;
            yield return null;
        }
    }
}
