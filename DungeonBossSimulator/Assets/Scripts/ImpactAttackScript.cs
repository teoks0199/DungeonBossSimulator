using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactAttackScript : MonoBehaviour
{
    private float despawnDelay = 0.5f; // Delay before despawning the impact attack
    public float damage = PlayerStats.playerStats.impactAttackDamage;
    public Animator animator;

    public void InitializeDespawn()
    {
        // PlayerStats.playerStats.impactAttackCoolDownImage();
        StartCoroutine(PlayImpactAnimationAndDespawn());
    }

    IEnumerator PlayImpactAnimationAndDespawn()
    {
        if (animator != null)
        {
            animator.SetTrigger("Impact");
        }
        //PlayerStats.playerStats.impactAttackCoolDownImage();
        yield return new WaitForSeconds(despawnDelay);
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




