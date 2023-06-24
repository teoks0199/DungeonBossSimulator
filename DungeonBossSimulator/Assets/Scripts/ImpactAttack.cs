using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactAttack : MonoBehaviour
{
    public GameObject impactAttack;
    private GameObject playerModel;
    private bool isCooldown = false; // Flag to track if the attack is on cooldown
    private float cooldownTimer = 0f; // Timer to measure the cooldown duration

    void Start()
    {
        playerModel = PlayerStats.playerStats.playerModel;
    }

    void Update()
    {
        if (!isCooldown && Input.GetKeyDown("space"))
        {
            TriggerImpactAttack();
        }

        UpdateCooldown();
    }

    void UpdateCooldown()
    {
        if (isCooldown)
        {
            cooldownTimer += Time.deltaTime;

            if (cooldownTimer >= PlayerStats.playerStats.impactAttackCoolDown)
            {
                isCooldown = false;
                cooldownTimer = 0f;
            }
        }
    }

    void TriggerImpactAttack()
    {
        if (impactAttack != null)
        {
            isCooldown = true;

            GameObject impact = Instantiate(impactAttack, playerModel.transform.position + new Vector3(0, -0.2f, 0), Quaternion.identity);
            impact.transform.SetParent(playerModel.transform);
            ImpactAttackScript attackScript = impact.AddComponent<ImpactAttackScript>();
            PlayerStats.playerStats.isImpactCooldown = true;
            PlayerStats.playerStats.impactAttackCoolDownImage();
            attackScript.InitializeDespawn();
        }
    }
}
