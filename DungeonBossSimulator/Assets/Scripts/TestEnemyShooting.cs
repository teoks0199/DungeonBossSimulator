using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyShooting : MonoBehaviour
{
//     public GameObject projectile;
//     public GameObject player;
//     public float minDamage;
//     public float maxDamage;
//     public float projectileForce;
//     public float cooldown;
//     private SpriteRenderer _renderer;
//     private Animator animator;
//     public bool isDead = false;

//     public void Start()
//     {
//         StartCoroutine(ShootPlayer());
//         //player = PlayerStats.playerStats.playerModel;
//         _renderer = GetComponent<SpriteRenderer>();
//         animator = GetComponent<Animator>();
//     }

//     void Update() {
//         if (!isDead)
//         {
//             player = PlayerStats.playerStats.playerModel;
//             animator.SetBool("Attack", false);
//         } 
//         else
//         {
//             GetComponent<BoxCollider2D> ().enabled = false;
//         }  
//     }

//     IEnumerator ShootPlayer() {
//             yield return new WaitForSeconds(cooldown);
//             if (player!=null) {
//                 animator.SetBool("Attack", true);
//                 yield return new WaitForSeconds(0.45f);
//                 GameObject spell = Instantiate(projectile, transform.position, Quaternion.identity);
//                 //Debug.Log(player.transform.position);
//                 Vector2 myPos = transform.position;
//                 Vector2 targetPos = player.transform.position;
//                 Vector2 direction = (targetPos - myPos).normalized;
//                 if (direction.x > 0) {
//                         _renderer.flipX = false; }
//                 else if (direction.x < 0) {
//                         _renderer.flipX = true; 
//                     }

//                 spell.GetComponent<Rigidbody2D>().velocity = direction * projectileForce;
//                 spell.GetComponent<TestEnemyProjectile>().damage = Random.Range(minDamage, maxDamage);
//                 // animator.SetBool("Attack", true);
//                 StartCoroutine(ShootPlayer());
//             }
//     }
// }

    //public Transform firePoint;        // The position from where the projectile will be fired
    public GameObject projectilePrefab; // The prefab of the projectile to be fired
    public float fireRate;        // The rate at which the AI unit fires projectiles (in shots per second)
    //public float shootingRange = 10f;  // The maximum distance at which the AI unit can shoot enemies

    private float timeSinceLastShot = 0f;
    private SpriteRenderer _renderer;
    private Animator animator;

    public GameObject player;

    public bool isDead = false;

    public void Start()
    {
        //fireRate = 0.45f; 
        _renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        player = PlayerStats.playerStats.playerModel;
    }

    private void Update()
    {

        if (!isDead)
        {
            animator.SetBool("Attack", false);
        }

        timeSinceLastShot += Time.deltaTime;
        if (timeSinceLastShot >= 1f / fireRate)
            {
                // Fire the projectile
                FireProjectile(player.transform.position);
                timeSinceLastShot = 0f;
            }
        
    }

    // public IEnumerator Wait(float delayInSecs)
    // {
    //     yield return new WaitForSeconds(delayInSecs);
    // }

    // private void FireProjectile(Vector3 pos)
    // {
    //     animator.SetBool("Attack", true);
        
    //     StartCoroutine(Wait(1f));

    //     GameObject newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);


    //     Vector2 direction = (pos - transform.position).normalized;

    //     if (direction.x > 0)
    //     {
    //         _renderer.flipX = false;
    //     }
    //     else if (direction.x < 0)
    //     {
    //         _renderer.flipX = true;
    //     }

    //     newProjectile.GetComponent<Rigidbody2D>().velocity = direction * 1F;

    // }

    private void FireProjectile(Vector3 pos)
    {
        StartCoroutine(AttackWithDelay(pos));
    }

    private IEnumerator AttackWithDelay(Vector3 pos)
    {

    if (isDead)
    {
        yield return new WaitForSeconds(1.0f);
        yield break; // Exit the coroutine immediately if the boss is dead
    }
        animator.SetBool("Attack", true);

        // Wait for the attack animation to play (adjust the time accordingly)
        yield return new WaitForSeconds(0.5f); // Example delay time of 0.5 seconds

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

        newProjectile.GetComponent<Rigidbody2D>().velocity = direction * 2F;
    }


}
