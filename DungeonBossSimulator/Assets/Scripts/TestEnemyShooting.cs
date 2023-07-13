using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyShooting : MonoBehaviour
{
    public GameObject projectile;
    public GameObject player;
    public float minDamage;
    public float maxDamage;
    public float projectileForce;
    public float cooldown;
    private SpriteRenderer _renderer;
    private Animator animator;
    public bool isDead = false;

    public void Start()
    {
        StartCoroutine(ShootPlayer());
        //player = PlayerStats.playerStats.playerModel;
        _renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update() {
        if (!isDead)
        {
            player = PlayerStats.playerStats.playerModel;
            animator.SetBool("Attack", false);
        } 
        else
        {
            GetComponent<BoxCollider2D> ().enabled = false;
        }  
    }

    IEnumerator ShootPlayer() {
            yield return new WaitForSeconds(cooldown);
            if (player!=null) {
                animator.SetBool("Attack", true);
                yield return new WaitForSeconds(0.45f);
                GameObject spell = Instantiate(projectile, transform.position, Quaternion.identity);
                //Debug.Log(player.transform.position);
                Vector2 myPos = transform.position;
                Vector2 targetPos = player.transform.position;
                Vector2 direction = (targetPos - myPos).normalized;
           if (direction.x > 0) {
                _renderer.flipX = false; }
           else if (direction.x < 0) {
                _renderer.flipX = true; 
            }

            spell.GetComponent<Rigidbody2D>().velocity = direction * projectileForce;
            spell.GetComponent<TestEnemyProjectile>().damage = Random.Range(minDamage, maxDamage);
            // animator.SetBool("Attack", true);
            StartCoroutine(ShootPlayer());
            }
    }
}
