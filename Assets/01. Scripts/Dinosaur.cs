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
    [SerializeField] LayerMask chicken;

    float moveSpeed = 0.5f;
    Animator anim;
    Coroutine moveCoroutine;
    Coroutine attackCoroutine;

    HPBarManager hpBarManager;
    HPbar hpBar;

    float damage = 5f;
    float attackTime = 3f;
    float stopTime = 3f;

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
                    moveCoroutine = null;
                }
                if (attackCoroutine == null)
                {
                    attackCoroutine = StartCoroutine(AttackPlayer());
                }
            }
        }
        else
        {
            if (moveCoroutine == null)
            {
                moveCoroutine = StartCoroutine(DinoMove());
            }
            if (attackCoroutine != null)
            {
                StopCoroutine(attackCoroutine);
                attackCoroutine = null;
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
                moveCoroutine = null;
            }

            switch (dinoType)
            {
                case DinoType.mofletitos:
                    anim.SetBool("isDie", true);
                    break;
                case DinoType.stegosaurus:
                    anim.SetBool("isDie1", true);
                    break;
            }
            Destroy(gameObject, 1.5f);
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
                switch (dinoType)
                {
                    case DinoType.mofletitos:
                        anim.SetBool("isStop", false);
                        break;
                    case DinoType.stegosaurus:
                        anim.SetBool("isStop1", false);
                        break;
                }

                transform.position += Vector3.left * moveSpeed * Time.deltaTime;
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            switch (dinoType)
            {
                case DinoType.mofletitos:
                    anim.SetBool("isStop", true);
                    break;
                case DinoType.stegosaurus:
                    anim.SetBool("isStop1", true);
                    break;
            }

            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator AttackPlayer()
    {
        while (true)
        {
            switch (dinoType)
            {
                case DinoType.mofletitos:
                    anim.SetBool("isAttack", true);
                    break;
                case DinoType.stegosaurus:
                    //anim.SetBool("isAttack", true);
                    break;
            }

            yield return new WaitForSeconds(attackTime);

            switch (dinoType)
            {
                case DinoType.mofletitos:
                    anim.SetBool("isAttack", false);
                    anim.SetBool("isStop", true);
                    break;
                case DinoType.stegosaurus:
                    //anim.SetBool("isAttack", false);
                    anim.SetBool("isStop1", true);
                    break;
            }

            yield return new WaitForSeconds(stopTime);

            switch (dinoType)
            {
                case DinoType.mofletitos:
                    anim.SetBool("isStop", false);
                    break;
                case DinoType.stegosaurus:
                    anim.SetBool("isStop1", false);
                    break;
            }
        }
    }
}
