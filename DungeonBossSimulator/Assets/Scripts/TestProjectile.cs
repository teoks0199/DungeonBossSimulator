using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestProjectile : MonoBehaviour
{
    // Start is called before the first frame update
    public float damage;
    private SpriteRenderer _renderer;

    void Start() {
        _renderer = GetComponent<SpriteRenderer>();
    }

    void Update() {
            if (Input.GetAxisRaw("Horizontal") > 0)
    {
        _renderer.flipX = false;
    }
    else if (Input.GetAxisRaw("Horizontal") < 0)
    {
        _renderer.flipX = true;
    }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name != "Player")
        {
            if (collision.GetComponent<EnemyReceiveDamage>() != null) 
            {
                collision.GetComponent<EnemyReceiveDamage>().DealDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
