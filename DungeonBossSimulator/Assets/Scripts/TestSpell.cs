using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpell : MonoBehaviour
{
    public GameObject projectile;
    //public float damage = PlayerStats.playerStats.projectileDamage;
    public float projectileForce;
    // public float cooldownDuration;

    private bool isFiring = false;
    private float lastFireTime;

    // private PlayerStats playerStats;

    // public Image cooldownImage;


    // private void Start()
    // {
    //     playerStats = FindObjectOfType<PlayerStats>();
    // }


    void Update()
    {
        if (Input.GetMouseButtonDown(1) && Time.time >= lastFireTime + PlayerStats.playerStats.projectileCoolDown)
        {
            // isFiring = true;
            StartCoroutine(FireProjectiles());
        }
        // else if (Input.GetMouseButtonUp(0))
        // {
        //     isFiring = false;
        // }
    }

    IEnumerator FireProjectiles()
    {
        // while (isFiring)
        // {
            GameObject spell = Instantiate(projectile, transform.position, Quaternion.identity);
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 myPos = transform.position;
            Vector2 direction = (mousePos - myPos).normalized;
            spell.GetComponent<Rigidbody2D>().velocity = direction * projectileForce;
            // Random damage
            //spell.GetComponent<TestProjectile>().damage = damage;
            lastFireTime = Time.time; // Update the last fire time
            PlayerStats.playerStats.isProjectileCooldown = true;
            PlayerStats.playerStats.projectileCoolDownImage();
            yield return new WaitForSeconds(PlayerStats.playerStats.projectileCoolDown);
        }
}




