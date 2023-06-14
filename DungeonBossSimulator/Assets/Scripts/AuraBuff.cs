using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraBuff : MonoBehaviour
{
    public GameObject auraBuff;
    private GameObject playerModel;

    void Start()
    {
        playerModel = PlayerStats.playerStats.playerModel;
        TriggerAuraBuff();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown("q"))
        {
            TriggerAuraBuff();
        }*/
        //TriggerAuraBuff();
    }

    void TriggerAuraBuff()
    {
        if (auraBuff != null)
        {
            // Instantiate the impact attack at the desired position relative to the player
            GameObject aura = Instantiate(auraBuff, playerModel.transform.position + new Vector3(0, -0.02f, 0), Quaternion.identity);
            // Optionally, you can parent the impact attack to the player for better organization
            aura.transform.SetParent(playerModel.transform);
            // Add a script to the impact attack object to handle despawning after the attack occurs
            AuraBuffScript auraBuffScript = aura.AddComponent<AuraBuffScript>();
            // attackScript.InitializeDespawn(); // Call a method to initialize the despawn process
        }
    }
}