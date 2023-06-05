using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpell : MonoBehaviour
{
    public GameObject projectile;
    public float minDamage;
    public float maxDamage;
    public float projectileForce;
    public float cooldownDuration;

    private bool isFiring = false;
    private float lastFireTime;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= lastFireTime + cooldownDuration)
        {
            isFiring = true;
            StartCoroutine(FireProjectiles());
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isFiring = false;
        }
    }

    IEnumerator FireProjectiles()
    {
        while (isFiring)
        {
            GameObject spell = Instantiate(projectile, transform.position, Quaternion.identity);
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 myPos = transform.position;
            Vector2 direction = (mousePos - myPos).normalized;
            spell.GetComponent<Rigidbody2D>().velocity = direction * projectileForce;
            // Random damage
            spell.GetComponent<TestProjectile>().damage = Random.Range(minDamage, maxDamage);
            lastFireTime = Time.time; // Update the last fire time
            yield return new WaitForSeconds(cooldownDuration);
        }
    }
}




