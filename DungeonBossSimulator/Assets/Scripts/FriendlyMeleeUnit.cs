using UnityEngine;

public class FriendlyMeleeUnit : MonoBehaviour
{
    private GameObject enemy;
    public float speed;
    private SpriteRenderer _renderer;
    private Rigidbody2D rb;

    public float knockbackForce;
    public float damage = 3;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        FindNearestEnemy();

        if (enemy != null)
        {
            Vector2 targetPosition = enemy.transform.position;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            // Flip sprite based on enemy position
            _renderer.flipX = transform.position.x < targetPosition.x;
        }
    }

    private void FindNearestEnemy()
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyReceiveDamage enemyDamageReceiver = collision.GetComponent<EnemyReceiveDamage>();
            if (enemyDamageReceiver != null)
            {
                enemyDamageReceiver.DealDamage(damage);
            }

            Vector2 knockbackDirection = (collision.transform.position - transform.position).normalized;
            rb.AddForce(-knockbackDirection * knockbackForce, ForceMode2D.Impulse);
        }
    }
}

