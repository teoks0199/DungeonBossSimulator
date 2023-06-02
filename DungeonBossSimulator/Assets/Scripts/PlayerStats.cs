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
    // public GameObject HealthUI;
    public TMP_Text healthText;
    public Slider healthSlider;
    public float health;
    public float maxHealth = 999;
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
        // playerModel = Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
        DontDestroyOnLoad(this);
        health = maxHealth;

        upgradePool.Add("IncreaseMaxHealth", new IncreaseHealthUpgrade());
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Level 01")
        {
            playerModel = Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
            // impactAttack = Instantiate(impactAttack, new Vector3(0, -0.3f, 0), Quaternion.identity);
            // impactAttack.transform.SetParent(playerModel.transform);
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
        LevelManager.instance.GameOver();
    }

    float CalculateHealthPercentage()
    {
        return (health / maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            TriggerImpactAttack();
        }
    }

    void TriggerImpactAttack()
    {
        if (impactAttack != null)
        {
            // Instantiate the impact attack at the desired position relative to the player
            GameObject impact = Instantiate(impactAttack, playerModel.transform.position + new Vector3(0, -0.2f, 0), Quaternion.identity);
            // Optionally, you can parent the impact attack to the player for better organization
            impact.transform.SetParent(playerModel.transform);

            // Add a script to the impact attack object to handle despawning after the attack occurs
            ImpactAttackScript attackScript = impact.AddComponent<ImpactAttackScript>();
            attackScript.InitializeDespawn(); // Call a method to initialize the despawn process
        }
    }
}

