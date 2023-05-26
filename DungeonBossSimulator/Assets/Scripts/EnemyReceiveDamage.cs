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

    private void Start()
    {
        health = maxHealth;
    }

    public void DealDamage(float damage)
    {
        healthBar.SetActive(true);
        health -= damage;
        CheckDeath();
        healthBarSlider.value = CalculateHealthPercentage();
    }

    public void HealCharacter(float heal)
    {
        health += heal;
        CheckOverheal();
        healthBarSlider.value = CalculateHealthPercentage();
    }

    private void CheckOverheal()
    {
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    private void CheckDeath()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        Instantiate(lootDrop, transform.position, Quaternion.identity);
        LevelManager.instance.enemyCount--;
        if (LevelManager.instance.enemyCount <= 0)
        {
            LevelManager.instance.NextLevel();
        }
    }

    private float CalculateHealthPercentage()
    {
        return health / maxHealth;
    }

    private void Update()
    {

    }
}

