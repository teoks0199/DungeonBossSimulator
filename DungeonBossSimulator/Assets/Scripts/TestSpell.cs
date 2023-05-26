using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpell : MonoBehaviour
{
    public GameObject projectile;
    public float minDamage;
    public float maxDamage;
    public float projectileForce;
    public GameObject attackPoint; // Changed the type to GameObject
    public Vector2 attackSize = new Vector2(1f, 1f); // Size of the attack rectangle
    public Animator animator;
    public LayerMask enemyLayers;
    public float meleeAttackCooldown = 1.0f; // Cooldown time for melee attack in seconds

    private SpriteRenderer spriteRenderer;
    private bool canAttack = true; // Flag to determine if melee attack can be performed

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }

        if (Input.GetMouseButtonDown(1))
        {
            MeleeAttack();
        }
    }

    void Start()
    {
        attackPoint.SetActive(false); // Disable the attack point hit box initially
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Attack()
    {
        GameObject spell = Instantiate(projectile, transform.position, Quaternion.identity);
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 myPos = transform.position;
        Vector2 direction = (mousePos - myPos).normalized;
        spell.GetComponent<Rigidbody2D>().velocity = direction * projectileForce;
        // Random damage
        spell.GetComponent<TestProjectile>().damage = Random.Range(minDamage, maxDamage);
    }

    public void MeleeAttack()
    {
        if (canAttack)
        {
            animator.SetTrigger("Attack");
            StartCoroutine(MeleeAttackCooldown());
        }
    }

    private IEnumerator MeleeAttackCooldown()
    {
        canAttack = false; // Set the flag to prevent repeated attacks
        yield return new WaitForSeconds(meleeAttackCooldown);
        canAttack = true; // Reset the flag to allow the next attack
    }

    public void EnableAttackPoint()
    {
        attackPoint.SetActive(true);
        Vector2 attackPointPosition = attackPoint.transform.localPosition;
        if (!IsFacingRight())
        {
            attackPointPosition = new Vector2(-attackPointPosition.x, attackPointPosition.y);
        }
        attackPoint.transform.localPosition = attackPointPosition;

        Vector2 size = new Vector2(attackSize.x * Mathf.Abs(transform.localScale.x), attackSize.y);
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackPoint.transform.position, size, 0f, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Hit");
            enemy.GetComponent<EnemyReceiveDamage>().DealDamage(20);
        }
    }

    public void DisableAttackPoint()
    {
        attackPoint.SetActive(false);
    }

    private bool IsFacingRight()
    {
        return spriteRenderer.flipX == false;
    }
}






