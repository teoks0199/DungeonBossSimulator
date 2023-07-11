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
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Enemy")
        {
            
            if(collision.tag == "Minion")
            {
                //PlayerMovement.animator.SetTrigger("Hit");
                //PlayerStats.playerStats.DealDamage(damage); 
                collision.GetComponent<MinionReceiveDamage>().DealDamage(damage);                   
            }
            
        }
    }
}



    /*public IEnumerator Knockback()
    {
        float knockbackDuration = 0.01f;
        //float knockbackPower = 0.0005f;
        //float knockbackPower = 0.4f;
        Transform obj = PlayerStats.playerStats.player.transform;
        Vector2 difference = transform.position - obj.transform.position;
        float timer = 0;

        while (knockbackDuration > timer)
        {
            timer += Time.deltaTime;
            transform.position = new Vector2(transform.position.x + difference.x, transform.position.y + difference.y);
        }
        yield return 0;
    }*/

    /*IEnumerator ShootPlayer() {
            yield return new WaitForSeconds(cooldown);
            if (player!=null) {
                GameObject spell = Instantiate(projectile, transform.position, Quaternion.identity);
                Debug.Log(player.transform.position);
                Vector2 myPos = transform.position;
                Vector2 targetPos = player.transform.position;
                Vector2 direction = (targetPos - myPos).normalized;
                spell.GetComponent<Rigidbody2D>().velocity = direction * projectileForce;
                spell.GetComponent<TestEnemyProjectile>().damage = Random.Range(minDamage, maxDamage);
                StartCoroutine(ShootPlayer());
            }
    }*/

