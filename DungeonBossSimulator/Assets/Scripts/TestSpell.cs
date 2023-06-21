using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpell : MonoBehaviour
{
    public GameObject projectile;
    //public float damage = PlayerStats.playerStats.projectileDamage;
    public float projectileForce;
    public float cooldownDuration;

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
        if (Input.GetMouseButtonDown(1) && Time.time >= lastFireTime + cooldownDuration)
        {
            // isFiring = true;
            StartCoroutine(FireProjectiles());
        }
        // else if (Input.GetMouseButtonUp(0))
        // {
        //     isFiring = false;
        // }
    }

    // private void Update()
    // {
    //     if (Input.GetMouseButtonDown(1) && Time.time >= playerStats.lastFireTime + playerStats.cooldownDuration)
    //     {
    //         isFiring = true;
    //         StartCoroutine(FireProjectiles());
    //     }
    //     else if (Input.GetMouseButtonUp(0))
    //     {
    //         isFiring = false;
    //     }

    //     // Update the cooldown UI
    //     if (cooldownImage != null)
    //     {
    //         float cooldownTimeRemaining = Mathf.Clamp(playerStats.lastFireTime + playerStats.cooldownDuration - Time.time, 0f, playerStats.cooldownDuration);
    //         float fillAmount = cooldownTimeRemaining / playerStats.cooldownDuration;
    //         cooldownImage.fillAmount = fillAmount;
    //     }
    // }


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
            PlayerStats.playerStats.isCooldown = true;
            PlayerStats.playerStats.cooldownImage();
            yield return new WaitForSeconds(cooldownDuration);
        }
}




