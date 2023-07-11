using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyMeleeUnit : MonoBehaviour
{
    public GameObject enemy;
    public float speed;
    private SpriteRenderer _renderer;
    private Rigidbody2D rb;

    // New variable for knockback force
    public float knockbackForce;
    public float damage = 3;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        FindNearestEnemy();

        if (enemy != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, enemy.transform.position, speed * Time.deltaTime);

            if (transform.position.x < enemy.transform.position.x)
            {
                _renderer.flipX = false;
            }
            else if (transform.position.x > enemy.transform.position.x)
            {
                _renderer.flipX = true;
            }
        }
    }

    void FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float closestDistance = Mathf.Infinity;
        GameObject closestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector2.Distance(transform.position, enemy.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }

        enemy = closestEnemy;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            //Debug.Log("hitting");
            // Deal damage to the enemy
            EnemyReceiveDamage enem = collision.GetComponent<EnemyReceiveDamage>();
            if (enem != null)
            {
                enem.DealDamage(damage);
            }

            Vector2 difference = transform.position - collision.transform.position;
            rb.AddForce(difference.normalized * knockbackForce, ForceMode2D.Impulse);
        }
    }
}
