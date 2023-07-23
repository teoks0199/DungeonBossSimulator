using UnityEngine;

public class FriendlyMeleeUnit : MonoBehaviour
{
    private GameObject enemy;
    public float speed;
    private SpriteRenderer _renderer;
    private Rigidbody2D rb;

    public float knockbackForce;
    private float damage;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        damage = PlayerStats.playerStats.meleeMinionDamage;
        //speed = PlayerStats.playerStats.meleeMinionSpeed;
        FindNearestEnemy();

        if (enemy != null)
        {
            Vector2 targetPosition = enemy.transform.position;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }

        if (transform.position.x < enemy.transform.position.x)
        {
            _renderer.flipX = false;
        }
        else if (transform.position.x > enemy.transform.position.x)
        {
            _renderer.flipX = true;
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

    void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.gameObject.CompareTag("Enemy"))
        { 
            EnemyReceiveDamage enemyDamageReceiver = collision.gameObject.GetComponent<EnemyReceiveDamage>();
            enemyDamageReceiver.DealDamage(damage);
            Vector2 difference = transform.position - collision.transform.position;
            Debug.Log("Test");
            rb.AddForce(difference.normalized * knockbackForce, ForceMode2D.Impulse);                       
        }
      
    }


    
    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (collision.CompareTag("Enemy"))
    //     {
    //         EnemyReceiveDamage enemyDamageReceiver = collision.GetComponent<EnemyReceiveDamage>();
    //         if (enemyDamageReceiver != null)
    //         {
    //             enemyDamageReceiver.DealDamage(damage);
    //         }
    //     }

    //     if (collision.CompareTag("Minion")) {
    //         Vector2 difference = transform.position - collision.transform.position;
    //         rb.AddForce(difference.normalized * knockbackForce, ForceMode2D.Impulse); 
    //     }
    // }
}

