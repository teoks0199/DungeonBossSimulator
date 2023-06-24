using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public Image image;
    public float cooldown1 = 1;
    public bool isCooldown = false;

////////////////////////////////////////////////////////////
    public static PlayerStats playerStats;

    public GameObject player;

    public float health;

    public float maxHealth;

////////// Health UI ///////////////////////////////////////
    public Canvas hpCanvas;
    public Canvas healthCanvas;
    private TMP_Text healthText;
    private Slider healthSlider;

////////// Cooldowns ///////////////////////////////////////

    public Image projectileAttackImage;

    public Image swipeAttackImage;

    public Image impactAttackImage;

    public float projectileCoolDown;

    public float swipeAttackCoolDown;

    public float impactAttackCoolDown;

    public bool isProjectileCooldown = false;

    public bool isSwipeAttackCooldown = false;

    public bool isImpactCooldown = false;


////////// Damage Numbers ///////////////////////////////////////

    public float projectileDamage;
    public float swipeDamage;
    public float auraBuffDamage;
    public float impactAttackDamage;

//////////////////////////////////////////////////////////

    public int coins;
    public GameObject impactAttack;
    public GameObject playerModel;

    public Dictionary<string, Upgrade> upgradePool = new Dictionary<string, Upgrade>();

    void Update() {

        if (isProjectileCooldown) {

            projectileCoolDownImage();

        }

        if (isSwipeAttackCooldown) {

            swipeAttackCoolDownImage();

        }

        if (isImpactCooldown) {

            impactAttackCoolDownImage();

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

        maxHealth = 150;
        health = maxHealth;
        projectileDamage = 1000;
        swipeDamage = 5;
        auraBuffDamage = 1F;
        impactAttackDamage = 10;
    
        projectileCoolDown = 0.1f;
        swipeAttackCoolDown = 0;
        impactAttackCoolDown = 2f;

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
            projectileAttackImage = healthCanvas.GetComponentsInChildren<Image>()[2];
            swipeAttackImage = healthCanvas.GetComponentsInChildren<Image>()[1];
            impactAttackImage = healthCanvas.GetComponentsInChildren<Image>()[3];
            healthSlider = healthCanvas.GetComponentInChildren<Slider>();
        }

        SetHealthUI();
    }

    public void DealDamage(float damage)
    {
        health -= damage;
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

    public void projectileCoolDownImage()
    {
        projectileAttackImage.fillAmount -= 1 / projectileCoolDown * Time.deltaTime;

        if (projectileAttackImage.fillAmount <= 0) {
            projectileAttackImage.fillAmount = 1;
            isProjectileCooldown = false;
        }

    }

        public void swipeAttackCoolDownImage()
    {
        swipeAttackImage.fillAmount -= 1 / swipeAttackCoolDown * Time.deltaTime;

        if (swipeAttackImage.fillAmount <= 0) {
            swipeAttackImage.fillAmount = 1;
            isSwipeAttackCooldown = false;
        }

    }


    public void impactAttackCoolDownImage()
    {
        Debug.Log("hello");
        impactAttackImage.fillAmount -= 1 / impactAttackCoolDown * Time.deltaTime;

        if (impactAttackImage.fillAmount <= 0) {
            impactAttackImage.fillAmount = 1;
            isImpactCooldown = false;
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
        LevelManager.instance.GameOver();
    }

    float CalculateHealthPercentage()
    {
        return (health / maxHealth);
    }

}

