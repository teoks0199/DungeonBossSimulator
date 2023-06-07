using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactAttack : MonoBehaviour
{
    public GameObject impactAttack;
    private GameObject playerModel;

    void Start()
    {
        playerModel = PlayerStats.playerStats.playerModel;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
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
