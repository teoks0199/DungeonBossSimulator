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

    public GameObject meleeMinion;

    public GameObject rangedMinion;

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

    public float health;
    public float maxHealth;
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
        projectileDamage = 15;
        swipeDamage = 5;
        auraBuffDamage = 4F;
        impactAttackDamage = 20;
    
        projectileCoolDown = 0.5f;
        swipeAttackCoolDown = 0.3f;
        impactAttackCoolDown = 2f;

        upgradePool.Add("Increase Max Health", new IncreaseHealthUpgrade());
        upgradePool.Add("Increase Projectile Damage", new ProjectileDamageUpgrade());
        upgradePool.Add("Increase Swipe Damage", new SwipeDamageUpgrade());
        upgradePool.Add("Heal 100HP", new HealUpgrade());
        upgradePool.Add("Unlock Impact Attack [Spacebar]", new UnlockImpactAttackUpgrade());
        upgradePool.Add("Unlock Aura Buff", new UnlockAuraBuffUpgrade());
        upgradePool.Add("Summon Melee Minion", new SummonMeleeMinionUpgrade());
        upgradePool.Add("Summon Ranged Minion", new SummonRangedMinionUpgrade());
    }

    void Start()
    {
        if ((SceneManager.GetActiveScene().name == "Level 01") || (SceneManager.GetActiveScene().name.EndsWith("TestScene")))
        {
            if (healthCanvas == null) {
                playerModel = Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
                healthCanvas = Instantiate(hpCanvas);
                DontDestroyOnLoad(healthCanvas);
                healthText = healthCanvas.GetComponentInChildren<TMP_Text>();
                projectileAttackImage = healthCanvas.GetComponentsInChildren<Image>()[4];
                swipeAttackImage = healthCanvas.GetComponentsInChildren<Image>()[3];
                impactAttackImage = healthCanvas.GetComponentsInChildren<Image>()[5];
                healthSlider = healthCanvas.GetComponentInChildren<Slider>();
                SetHealthUI();
                    }
        }

        //SetHealthUI();
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
        impactAttackImage.fillAmount -= 1 / impactAttackCoolDown * Time.deltaTime;

        if (impactAttackImage.fillAmount <= 0) {
            impactAttackImage.fillAmount = 1;
            isImpactCooldown = false;
        }

    }

    private void CheckDeath()
    {
        //Debug.Log(health);
     if (health <= 0)
     {
         try
         {
             PlayerDied();
         }
         catch (System.Exception ex)
         {
             Debug.LogError("Exception occurred while handling player death: " + ex.Message);
         }
     }
    }

    private void PlayerDied()
    {
        var temp = this;
        playerStats = new PlayerStats();
        Destroy(temp.playerModel);
        Destroy(temp);
        Destroy(healthCanvas);
        DestroyGameObjectsWithTag("Minion");
        LevelManager.instance.GameOver();
    }

    public void DestroyGameObjectsWithTag(string tag)
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);

        for (int i = 0; i < gameObjects.Length; i++)
        {
            Destroy(gameObjects[i]);
        }
    }

    float CalculateHealthPercentage()
    {
        return (health / maxHealth);
    }
}
