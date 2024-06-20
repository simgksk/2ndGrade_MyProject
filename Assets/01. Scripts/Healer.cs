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

            // ������ ������Ʈ�� ���� �ݿ��� �׸��� �̵��ϵ��� ��
            StartCoroutine(MoveInSemiCircle(spawnedFeed));
        }
    }

    IEnumerator MoveInSemiCircle(GameObject obj)
    {
        float duration = 2f; // �ݿ��� �׸��µ� �ɸ��� �ð�
        float elapsedTime = 0f;
        Vector3 startPos = obj.transform.position;
        Vector3 endPos = startPos + new Vector3(0, 2f, 2f); // ������ ��ġ�� ����

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            float height = Mathf.Sin(Mathf.PI * t); // �ݿ� ������ ���� sin �Լ�

            obj.transform.position = Vector3.Lerp(startPos, endPos, t) + Vector3.up * height;
            yield return null;
        }
    }
}
