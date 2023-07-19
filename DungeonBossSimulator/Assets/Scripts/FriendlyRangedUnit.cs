/*using System.Collections;
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
    public float speed = 0.1F;

    private Rigidbody2D rb;

    public float knockbackForce;

    public void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        FindNearestEnemy();
        StartCoroutine(ShootEnemy());
    }

    void Update()
    {
        animator.SetBool("Attack", false);
        FindNearestEnemy();
        
        if (enemy != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, enemy.transform.position, speed * Time.deltaTime);
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
        Debug.Log("shooting");
        yield return new WaitForSeconds(0.1f);
        Debug.Log("1");
        
     

        if (enemy != null)
        {
            //animator.SetBool("Attack", true);
            Debug.Log("2");
            yield return new WaitForSeconds(1f);
            Debug.Log("3");
            animator.SetBool("Attack", true);
            yield return new WaitForSeconds(0.2f);
            GameObject projectileInstance = Instantiate(projectile, transform.position, Quaternion.identity);
            Debug.Log("4");
            Vector2 direction = (enemy.transform.position - transform.position).normalized;
            Debug.Log("5");
            if (direction.x > 0)
            {
                _renderer.flipX = false;
            }
            else if (direction.x < 0)
            {
                _renderer.flipX = true;
            }
            Debug.Log("6");
            //yield return new WaitForSeconds(0.1f);
            projectileInstance.GetComponent<Rigidbody2D>().velocity = direction * projectileForce;
            Debug.Log("7"); 
            
            StartCoroutine(ShootEnemy());
        }
        //
        //StartCoroutine(ShootEnemy());
        
    }


//     IEnumerator ShootEnemy()
// {
//     while (true)
//     {
//         yield return new WaitForSeconds(1f);

//         if (enemy != null)
//         {
//             animator.SetBool("Attack", true);
//             yield return new WaitForSeconds(1f);

//             GameObject projectileInstance = Instantiate(projectile, transform.position, Quaternion.identity);

//             Vector2 direction = (enemy.transform.position - transform.position).normalized;

//             if (direction.x > 0)
//             {
//                 _renderer.flipX = false;
//             }
//             else if (direction.x < 0)
//             {
//                 _renderer.flipX = true;
//             }

//             projectileInstance.GetComponent<Rigidbody2D>().velocity = direction * projectileForce;
//         }

//         yield return null;
//     }
// }


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


        void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.gameObject.CompareTag("Enemy"))
        { 
            // EnemyReceiveDamage enemyDamageReceiver = collision.gameObject.GetComponent<EnemyReceiveDamage>();
            // enemyDamageReceiver.DealDamage(damage);
            Vector2 difference = transform.position - collision.transform.position;
            Debug.Log("Test");
            rb.AddForce(difference.normalized * knockbackForce, ForceMode2D.Impulse);                       
        }
      
    }


}*/

using UnityEngine;

public class FriendlyRangedUnit : MonoBehaviour
{
    //public Transform firePoint;        // The position from where the projectile will be fired
    public GameObject projectilePrefab; // The prefab of the projectile to be fired
    public float fireRate = 0.01f;        // The rate at which the AI unit fires projectiles (in shots per second)
    //public float shootingRange = 10f;  // The maximum distance at which the AI unit can shoot enemies

    private float timeSinceLastShot = 0f;
    private SpriteRenderer _renderer;
    private Animator animator;

    public void Start()
    {
        fireRate = 0.5f; 
        _renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool("Attack", false);
        // Find the closest enemy within shooting range
        GameObject closestEnemy = FindClosestEnemy();

        if (closestEnemy != null)
        {
            // Rotate the AI unit to face the enemy
            /*Vector3 directionToEnemy = closestEnemy.transform.position - transform.position;
            float targetAngle = Mathf.Atan2(directionToEnemy.y, directionToEnemy.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, targetAngle));*/

            // Check if it's time to fire
            timeSinceLastShot += Time.deltaTime;
            if (timeSinceLastShot >= 1f / fireRate)
            {
                // Fire the projectile
                FireProjectile(closestEnemy.transform.position);
                timeSinceLastShot = 0f;
            }
        }
        if (closestEnemy != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, closestEnemy.transform.position, 0.2F * Time.deltaTime);
            Vector2 direction = (closestEnemy.transform.position - transform.position).normalized;
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

    private GameObject FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closestEnemy = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject enemy in enemies)
        {
            Vector3 directionToEnemy = enemy.transform.position - currentPosition;
            float distanceSqr = directionToEnemy.sqrMagnitude;

            if (distanceSqr < closestDistanceSqr) //&& distanceSqr <= shootingRange * shootingRange)
            {
                closestDistanceSqr = distanceSqr;
                closestEnemy = enemy;
            }
        }

        return closestEnemy;
    }

    private void FireProjectile(Vector3 pos)
    {
        // Instantiate and fire the projectile
        //GameObject newProjectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
        // Add any additional logic for the projectile (e.g., setting velocity, damage, etc.)

        animator.SetBool("Attack", true);
        //yield return new WaitForSeconds(1f);

        GameObject newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        Vector2 direction = (pos - transform.position).normalized;

        if (direction.x > 0)
        {
            _renderer.flipX = false;
        }
        else if (direction.x < 0)
        {
            _renderer.flipX = true;
        }

        newProjectile.GetComponent<Rigidbody2D>().velocity = direction * 1F;

        // Destroy the projectile after a few seconds if it hasn't hit anything
        //Destroy(newProjectile, 5f);
    }
}

