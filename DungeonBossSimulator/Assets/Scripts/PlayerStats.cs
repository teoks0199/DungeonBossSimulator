using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{

    public static PlayerStats playerStats;

    public GameObject player;
    public TMP_Text healthText;
    public Slider healthSlider;
    public float health;
    public float maxHealth;
    public int coins;

    private Rigidbody2D rb;

    void Awake() {
        //health = maxHealth;
        if(playerStats != null) {
            Destroy(playerStats);
        }
        else {
            playerStats = this;
        }
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        Time.timeScale = 1;
        rb = GetComponent<Rigidbody2D>();
        //health = 4;
        //player.SetActive(true);
        health = maxHealth;
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
            //Debug.Log(health);
            PlayerDied();
        }
    }

    private void PlayerDied()
    {
        //Debug.Log("died");
        //Destroy(player);
        //player.SetActive(false);
        Time.timeScale = 0;

        LevelManager.instance.GameOver();
        //gameObject.SetActive(false);
    }

    float CalculateHealthPercentage() {
        return (health / maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}