using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraBuffScript : MonoBehaviour
{
    public float damage = PlayerStats.playerStats.auraBuffDamage;
    public float damageInterval;
    public float checkInterval;
    public float auraRadius;

    private List<EnemyReceiveDamage> enemiesInRange = new List<EnemyReceiveDamage>();
    private Coroutine damageCoroutine;

    void Start()
    {
        damageCoroutine = StartCoroutine(ApplyDamageOverTime());
        StartCoroutine(CheckEnemiesInRange());
    }

    void OnDisable()
    {
        if (damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
            damageCoroutine = null;
        }
    }

    IEnumerator ApplyDamageOverTime()
    {
        while (true)
        {
            foreach (EnemyReceiveDamage enemy in enemiesInRange)
            {
                enemy.DealDamage(damage);
            }
            yield return new WaitForSeconds(damageInterval);
        }
    }

    IEnumerator CheckEnemiesInRange()
    {
        while (true)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, auraRadius);
            enemiesInRange.Clear();

            foreach (Collider2D collider in colliders)
            {
                if (collider.CompareTag("Enemy"))
                {
                    EnemyReceiveDamage enemy = collider.GetComponent<EnemyReceiveDamage>();
                    if (enemy != null)
                    {
                        enemiesInRange.Add(enemy);
                    }
                }
            }

            yield return new WaitForSeconds(checkInterval);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, auraRadius);
    }
}

