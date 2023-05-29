using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyProjectile : MonoBehaviour
{
    public float damage;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Enemy")
        {
            if(collision.tag == "Player")
            {
                PlayerMovement.animator.SetTrigger("Hit");
                PlayerStats.playerStats.DealDamage(damage);                    
            }
            if (collision.tag != "Loot")
            {
                Destroy(gameObject);
            }
        }
    }
}
