using System.Collections;
using UnityEngine;

public class BossHeroAttack : MonoBehaviour
{
    public GameObject player;
    public GameObject slashPrefab;

    public GameObject Ring;
    public float speed;
    public float damage = 3;
    public float projectileForce;
    public float knockbackForce;
    public float rollDuration = 0.5f;
    public float rollCooldown = 0.1f;
    public float rollDistanceThreshold = 3f;

    public int numSlashes = 3;
    public float slashDelay = 0.5f;
    public float attackCooldown = 3f;

    public bool isDead = false;
    private bool canRoll = false;
    private bool canAttack = true;
    private bool canDash = false;
    private SpriteRenderer _renderer;
    private Rigidbody2D rb;
    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!isDead)
        {
            player = PlayerStats.playerStats.playerModel;

            float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

            if (canAttack)
            {
            StartCoroutine(PerformSlashAttack());
            }

            if (canRoll)
            {
                StartCoroutine(Roll());
            }

            if (canDash)
            {
                StartCoroutine(DashAttack());
            }

            else
            {
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            }
 
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

    private IEnumerator PerformSlashAttack()
    {
        canAttack = false;
        animator.SetBool("Attack", true);

        for (int i = 0; i < numSlashes; i++)
        {
            GameObject slash = Instantiate(slashPrefab, transform.position, Quaternion.identity);
            Vector2 direction = (player.transform.position - transform.position).normalized;

            if (direction.x > 0)
            {
                _renderer.flipX = false;
            }
            else if (direction.x < 0)
            {
                _renderer.flipX = true;
            }

            slash.GetComponent<Rigidbody2D>().velocity = direction * projectileForce;
            yield return new WaitForSeconds(slashDelay);
        }

        yield return new WaitForSeconds(0.6f);
        animator.SetBool("Attack", false);
        canRoll = true;
    }

private IEnumerator Roll()
{
    canRoll = false;
    animator.SetBool("Roll", true);
    Vector2 rollDirection = (transform.position - player.transform.position).normalized;
    rb.velocity = rollDirection * speed * 5f;
    yield return new WaitForSeconds(0.8f);
    rb.velocity = Vector2.zero;
    animator.SetBool("Roll", false);
    yield return new WaitForSeconds(rollCooldown);
    canDash = true;
}

private IEnumerator DashAttack()
{
    canDash = false;
    //isAttacking = true;
    //animator.SetBool("DashAttack", true);

    Vector2 dashDirection = (player.transform.position - transform.position).normalized;
    rb.velocity = dashDirection * speed * 6f; // Adjust the speed of the dash as needed

    yield return new WaitForSeconds(0.8f); // Adjust the duration of the dash attack as needed

    rb.velocity = Vector2.zero;
    //isAttacking = false;
    //animator.SetBool("DashAttack", false);

    //yield return new WaitForSeconds(attackCooldown);
    canAttack = true;
}




}






