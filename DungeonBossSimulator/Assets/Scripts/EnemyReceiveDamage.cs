using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health;
    public float maxHealth;

    public GameObject lootDrop;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    public void DealDamage(float damage) 
    {
        health -= damage;
        CheckDeath();
    }

    private void CheckOverheal() 
    {
        if (health > maxHealth) {
            health = maxHealth;
        }
    }

    private void CheckDeath() 
    {
        if (health <= 0) {
            Destroy(gameObject);
            Instantiate(lootDrop, transform.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
