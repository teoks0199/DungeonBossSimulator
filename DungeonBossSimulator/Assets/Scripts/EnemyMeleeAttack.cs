using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
{
    public GameObject player;
    public GameObject projectile;
    public float speed;
    private float distance;
    public float damage = 3;
    public float projectileForce;
    public float cooldown;
    public bool isDead = false;
    private SpriteRenderer _renderer;
    private Rigidbody2D rb;

    // New variable for knockback force
    public float knockbackForce;

    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!isDead)
        {
            player = PlayerStats.playerStats.playerModel;
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
        else
        {
            GetComponent<BoxCollider2D>().enabled = false;
        }

        if (transform.position.x < player.transform.position.x)
        {
            _renderer.flipX = false;
        }
        else if (transform.position.x > player.transform.position.x)
        {
            _renderer.flipX = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerMovement.animator.SetTrigger("Hit");
            PlayerStats.playerStats.DealDamage(damage);
            Vector2 difference = transform.position - collision.transform.position;
            rb.AddForce(difference.normalized * knockbackForce, ForceMode2D.Impulse);          
        }
        
        if(collision.gameObject.CompareTag("Minion"))
        { 
            collision.gameObject.GetComponent<MinionReceiveDamage>().DealDamage(damage);   
            //Vector2 difference = transform.position - collision.transform.position;
            //rb.AddForce(difference.normalized * knockbackForce, ForceMode2D.Impulse);                       
        }   
    }



    // void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (collision.tag != "Enemy")
    //     {
    //         if(collision.tag == "Minion")
    //         {
    //             //PlayerMovement.animator.SetTrigger("Hit");
    //             //PlayerStats.playerStats.DealDamage(damage); 
    //             collision.GetComponent<MinionReceiveDamage>().DealDamage(damage);      
    //             Vector2 difference = transform.position - collision.transform.position;
    //             rb.AddForce(difference.normalized * knockbackForce, ForceMode2D.Impulse);             
    //         }
            
    //     }
    // }
}
