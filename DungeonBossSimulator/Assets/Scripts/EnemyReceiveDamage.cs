using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyReceiveDamage : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public GameObject healthBar;
    public Slider healthBarSlider;
    public GameObject lootDrop;
    private Animator animator;
    public bool isDestroyed = false;

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
        if (health <= 0 && !isDestroyed) {
            StartCoroutine(DestroyWithDelay());
        }
    }

    private IEnumerator DestroyWithDelay()
    {
        animator.SetBool("Dead", true);
        LevelManager.instance.enemyCount -= 1;
        isDestroyed = true;
        if (LevelManager.instance.enemyCount <= 0)
        {
            // yield return new WaitForSeconds(0.5f);
            LevelManager.instance.NextLevel();
        }
       //gameObject.EnemyMeleeAttack.player = null;
       EnemyMeleeAttack enemMelee = GetComponent<EnemyMeleeAttack>();
       if (enemMelee != null)
       {
           enemMelee.isDead = true;
       }

       TestEnemyShooting wizard = GetComponent<TestEnemyShooting>();
       if (wizard != null)
       {
           wizard.isDead = true;
       }

       EnemyPriest priest = GetComponent<EnemyPriest>();
       if (priest != null)
       {
           priest.isDead = true;
       }

       BossHeroAttack bossHero = GetComponent<BossHeroAttack>();
        if (bossHero != null)
       {
           bossHero.isDead = true;
       }
//
        animator.SetBool("Dead", true);
        yield return new WaitForSeconds(1.0f); // Adjust the delay time as needed
        Destroy(gameObject);
        Instantiate(lootDrop, transform.position, Quaternion.identity);
        // LevelManager.instance.enemyCount -= 1;
//        if (LevelManager.instance.enemyCount <= 0)
//        {
//            LevelManager.instance.NextLevel();
//        }
    }
    private float CalculateHealthPercentage() {
        return (health / maxHealth);
    }
}

