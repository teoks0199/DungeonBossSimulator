using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats playerStats;

    public GameObject player;
    public Canvas hpCanvas;
    //public TMP_Text hpText;
    //public Slider hpSlider;
    public Canvas healthCanvas;
    private TMP_Text healthText;
    private Slider healthSlider;
    
    public float health;
    public float maxHealth = 999;
    public float projectileDamage;
    public float swipeDamage;
    public float auraBuffDamage;
    public float impactAttackDamage;
    public int coins;
    public GameObject impactAttack;
    public GameObject playerModel;

    public Dictionary<string, Upgrade> upgradePool = new Dictionary<string, Upgrade>();
    

    void Awake()
    {

        if (playerStats != null)
        {
            Destroy(playerStats);
        }
        else
        {
            playerStats = this;
        }
        DontDestroyOnLoad(this);
        
        health = maxHealth;
        projectileDamage = 1000;
        swipeDamage = 10;
        auraBuffDamage = 1F;
        impactAttackDamage = 10;

        upgradePool.Add("Increase Max Health", new IncreaseHealthUpgrade());
        upgradePool.Add("Increase Projectile Damage", new ProjectileDamageUpgrade());
        upgradePool.Add("Increase Swipe Damage", new SwipeDamageUpgrade());
        upgradePool.Add("Heal 50HP", new HealUpgrade());
        upgradePool.Add("Unlock Impact Attack", new UnlockImpactAttackUpgrade());
        upgradePool.Add("Unlock Aura Buff", new UnlockAuraBuffUpgrade());
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Level 01")
        {
            playerModel = Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
            healthCanvas = Instantiate(hpCanvas);
            DontDestroyOnLoad(healthCanvas);
            healthText = healthCanvas.GetComponentInChildren<TMP_Text>();
            healthSlider = healthCanvas.GetComponentInChildren<Slider>();
        }

        SetHealthUI();
    }

    public void DealDamage(float damage)
    {
        health -= damage;
        // Debug.Log(damage);
        SetHealthUI();
        CheckDeath();
    }

    public void HealCharacter(float heal)
    {
        health += heal;
        CheckOverheal();
        SetHealthUI();
    }

    private void CheckOverheal()
    {
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    private void SetHealthUI()
    {
        healthSlider.value = CalculateHealthPercentage();
        healthText.text = Mathf.Ceil(health).ToString() + " / " + Mathf.Ceil(maxHealth).ToString();
    }

    private void CheckDeath()
    {
        Debug.Log(health);
        if (health <= 0)
        {
            PlayerDied();
        }
    }

    private void PlayerDied()
    {
        var temp = this;
        playerStats = new PlayerStats();
        Destroy(temp.playerModel);
        Destroy(temp);
        //Destroy(healthCanvas);
        LevelManager.instance.GameOver();
    }

    float CalculateHealthPercentage()
    {
        return (health / maxHealth);
    }

}

