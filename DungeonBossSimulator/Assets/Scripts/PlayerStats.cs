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

    public Image image;

    public float cooldown1 = 1;
    public bool isCooldown = false;

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

    void Update() {

        if (isCooldown) {

            cooldownImage();

        }

    }
    

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
        swipeDamage = 5;
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
            image = healthCanvas.GetComponentsInChildren<Image>()[1];
            image.fillAmount = 1;
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
        // cooldownImage();
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

    public void cooldownImage()
    {
        image.fillAmount -= 1 / cooldown1 * Time.deltaTime;

        if (image.fillAmount <= 0) {
            image.fillAmount = 1;
            isCooldown = false;
        }

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

