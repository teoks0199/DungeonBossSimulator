using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPriest : MonoBehaviour
{
    public float healAmount;
    public float healCooldown;
    public float healRange;
    public LayerMask enemyLayerMask;

    private bool isHealing = false;
    private EnemyReceiveDamage[] enemies;

    public bool isDead = false;

    private void Start()
    {
        StartCoroutine(HealAllEnemiesRoutine());
    }
    
    void Update() {
        if (isDead)
        {
            GetComponent<BoxCollider2D> ().enabled = false;
            StopCoroutine(HealAllEnemiesRoutine());
        }  
    }


    private IEnumerator HealAllEnemiesRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(healCooldown);
            HealAllEnemiesWithinRange();
        }
    }

    private void HealAllEnemiesWithinRange()
{
    Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, healRange, enemyLayerMask);
    Debug.Log("Number of colliders detected: " + colliders.Length);

    foreach (Collider2D collider in colliders)
    {
  
        EnemyReceiveDamage enemy = collider.GetComponent<EnemyReceiveDamage>();
        if (enemy != null && !enemy.isDestroyed)
        {                
            enemy.HealCharacter(healAmount);
        }

    }
}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, healRange);
    }
}