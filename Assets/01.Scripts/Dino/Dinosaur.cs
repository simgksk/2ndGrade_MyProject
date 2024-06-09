using System.Collections;
using UnityEngine;

public class Dinosaur : MonoBehaviour
{
    float moveSpeed = .5f;

    private void Awake()
    {
    }

    void Start()
    {
        StartCoroutine(DinoMove());
    }

    void Update()
    {
    }

    IEnumerator DinoMove()
    {
        while (true)
        {

            float moveDuration = 1f;
            float elapsedTime = 0f;

            while (elapsedTime < moveDuration)
            {
                transform.position += Vector3.left * moveSpeed * Time.deltaTime;
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            yield return new WaitForSeconds(.5f);
        }
    }
}
