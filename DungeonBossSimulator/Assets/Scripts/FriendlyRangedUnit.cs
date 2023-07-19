
using UnityEngine;

public class FriendlyRangedUnit : MonoBehaviour
{
    //public Transform firePoint;        // The position from where the projectile will be fired
    public GameObject projectilePrefab; // The prefab of the projectile to be fired
    public float fireRate;        // The rate at which the AI unit fires projectiles (in shots per second)
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
        animator.SetBool("Attack", true);

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

