using System.Collections;
using UnityEngine;

public enum DinoType
{
    mofletitos,
    stegosaurus
}

public class Dinosaur : MonoBehaviour
{
    float moveSpeed = .5f;
    Animator anim;
    Coroutine moveCoroutine;
    [SerializeField] DinoType dinoType;
    [SerializeField] Transform dinoTransform;
    [SerializeField] LayerMask chicken;

    HPBarManager hpBarManager;
    HPbar hpBar;

    float damage = 10f;

    void Start()
    {
        hpBarManager = FindObjectOfType<HPBarManager>();
        if (hpBarManager != null)
        {
            hpBar = hpBarManager.SetupHPBar(transform, true);
        }

        anim = GetComponent<Animator>();
        moveCoroutine = StartCoroutine(DinoMove());
    }

    void Update()
    {
        RaycastHit hit;
        float distance = 0.7f;

        if (Physics.Raycast(dinoTransform.position, dinoTransform.forward, out hit, distance, chicken))
        {
            if (hit.collider.CompareTag("Dealer") || hit.collider.CompareTag("Healer") || hit.collider.CompareTag("Tanker"))
            {
                if (moveCoroutine != null)
                {
                    StopCoroutine(moveCoroutine);
                }
            }
        }

        Debug.DrawRay(dinoTransform.position, dinoTransform.forward * distance, Color.red);
    }
    private void OnDestroy()
    {
        if (hpBarManager != null)
        {
            hpBarManager.RemoveHPBar(transform);
        }
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
            if (hpBar != null)
            {
                hpBar.Damage(damage);

                if (hpBar.currentHP <= 0)
                {
                    Destroy(gameObject);
                }
            }

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
