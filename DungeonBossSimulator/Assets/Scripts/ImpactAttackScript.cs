using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactAttackScript : MonoBehaviour
{
    public float despawnDelay = 0.5f; // Delay before despawning the impact attack
    public float damage;
    public Animator animator;

    public void InitializeDespawn()
    {
        StartCoroutine(PlayImpactAnimationAndDespawn());
    }

    IEnumerator PlayImpactAnimationAndDespawn()
    {
        // Play the impact animation
        if (animator != null)
        {
            animator.SetTrigger("Impact");
        }

        // Wait for the impact animation to finish
        yield return new WaitForSeconds(despawnDelay);

        // Destroy the impact attack object
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyReceiveDamage enemy = collision.GetComponent<EnemyReceiveDamage>();
            if (enemy != null)
            {
                enemy.DealDamage(damage);
            }
        }
    }
}




