using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    //public float damage = PlayerStats.playerStats.projectileDamage;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player" && collision.tag != "Minion")
        {
            if (collision.GetComponent<EnemyReceiveDamage>() != null) 
            {
                collision.GetComponent<EnemyReceiveDamage>().DealDamage(PlayerStats.playerStats.projectileDamage);
            }
            if (collision.tag != "Loot" && collision.tag != "Aura")
            {
                Destroy(gameObject);
            }
            
        }
    }
}
