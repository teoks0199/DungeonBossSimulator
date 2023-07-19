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


}

