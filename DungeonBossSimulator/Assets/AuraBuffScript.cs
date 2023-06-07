using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraBuffScript : MonoBehaviour
{
    public float damage;
 
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
