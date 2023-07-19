using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinionReceiveDamage : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public GameObject healthBar;
    public Slider healthBarSlider;
    public GameObject lootDrop;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        animator = GetComponent<Animator>();
    }

    public void DealDamage(float damage) 
    {
        healthBar.SetActive(true);
        health -= damage;
        CheckDeath();
        healthBarSlider.value = CalculateHealthPercentage();
    }

    public void HealCharacter(float heal) {
        health += heal;
        CheckOverheal();
        healthBarSlider.value = CalculateHealthPercentage();
    }

    private void CheckOverheal() 
    {
        if (health > maxHealth) {
            health = maxHealth;
        }
    }

    private void CheckDeath()
    {
        if (health <= 0) {
            StartCoroutine(DestroyWithDelay());
        }
    }

    private IEnumerator DestroyWithDelay()
    {

        animator.SetBool("Dead", true);
        yield return new WaitForSeconds(1.0f); // Adjust the delay time as needed
        //Destroy(gameObject);
        Instantiate(lootDrop, transform.position, Quaternion.identity);
        //animator.SetBool("Run", true);
        gameObject.SetActive(false);

    }
    private float CalculateHealthPercentage() {
        return (health / maxHealth);
    }
}