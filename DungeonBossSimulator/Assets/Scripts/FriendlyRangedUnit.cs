using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyRangedUnit : MonoBehaviour
{
    public GameObject projectile;
    public GameObject enemy;
    public float damage;
    public float projectileForce;
    private SpriteRenderer _renderer;
    private Animator animator;

    public void Start()
    {
        StartCoroutine(ShootEnemy());
        _renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        animator.SetBool("Attack", false);
        FindNearestEnemy();
        if (enemy != null)
        {
            Vector2 direction = (enemy.transform.position - transform.position).normalized;
            if (direction.x > 0)
            {
                _renderer.flipX = false;
            }
            else if (direction.x < 0)
            {
                _renderer.flipX = true;
            }
        }
    }

    IEnumerator ShootEnemy()
    {
        yield return null; // Optional initial delay before shooting

       
        if (enemy != null)
        {
            animator.SetBool("Attack", true);
            yield return new WaitForSeconds(0.45f);
            GameObject projectileInstance = Instantiate(projectile, transform.position, Quaternion.identity);

            Vector2 direction = (enemy.transform.position - transform.position).normalized;
            if (direction.x > 0)
            {
                _renderer.flipX = false;
            }
            else if (direction.x < 0)
            {
                _renderer.flipX = true;
            }

            projectileInstance.GetComponent<Rigidbody2D>().velocity = direction * projectileForce;
            projectileInstance.GetComponent<TestEnemyProjectile>().damage = damage; 

            
        }

        yield return null; // Optional delay between shots
        StartCoroutine(ShootEnemy());
           
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
}
