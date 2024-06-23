using System.Collections;
using UnityEngine;

public enum DinoType
{
    mofletitos,
    stegosaurus
}

public class Dinosaur : MonoBehaviour
{
    [SerializeField] DinoType dinoType;
    [SerializeField] Transform dinoTransform;
    float moveSpeed = .5f;
    Animator anim;
    Coroutine moveCoroutine;

    void Start()
    {
        anim = GetComponent<Animator>();
        moveCoroutine = StartCoroutine(DinoMove());
    }

    void Update()
    {
        RaycastHit hit;
        float distance = 1f;

        /*if (Physics.Raycast(dinoTransform.position, dinoTransform.forward, out hit, distance))
        {
            if (hit.collider.CompareTag("Dino"))
            {
            }
        }*/

        Debug.DrawRay(dinoTransform.position, dinoTransform.forward * distance, Color.red);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Tractor"))
        {
            if (moveCoroutine != null)
            {
                StopCoroutine(moveCoroutine);
            }

            switch (dinoType)
            {
                case DinoType.mofletitos:
                    anim.SetBool("isDie", true);
                    Destroy(gameObject, 1.5f);
                    break;
                case DinoType.stegosaurus:
                    anim.SetBool("isDie1", true);
                    Destroy(gameObject, 1.5f);
                    break;
                
            }
        }
        if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Barn"))
        {
            GameManager.instance.ShowGameOverPanel();
        }
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
