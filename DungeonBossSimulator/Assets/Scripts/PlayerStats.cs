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

    //public GameObject HealthUI;
    public TMP_Text healthText;
    public Slider healthSlider;
    public float health;
    private float maxHealth = 99;
    public int coins;

    public GameObject playerModel;

    void Awake() 
    {
        if(playerStats != null) {
            Destroy(playerStats);
        }
        else {
            playerStats = this;
        }
        //playerModel = Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
        DontDestroyOnLoad(this);
        health = maxHealth;
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Level 01") {
            playerModel = Instantiate(player, new Vector3(0, 0, 0), Quaternion.identity);
        }
        
        SetHealthUI();
    }

    public void DealDamage(float damage) 
    {
        health -= damage;
        //Debug.Log(damage);
        SetHealthUI();
        CheckDeath();
        
    }

    public void HealCharacter(float heal) {

        health += heal;
        CheckOverheal();
        SetHealthUI();
    }

    private void CheckOverheal() 
    {
        if (health > maxHealth) {
            health = maxHealth;
        }
    }

    private void SetHealthUI() {
        healthSlider.value = CalculateHealthPercentage();
        healthText.text = Mathf.Ceil(health).ToString() + " / " + Mathf.Ceil(maxHealth).ToString();
    }

    private void CheckDeath() 
    {
        Debug.Log(health);
        if (health <= 0) {
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

    float CalculateHealthPercentage() {
        return (health / maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        //SetHealthUI();
    }
}