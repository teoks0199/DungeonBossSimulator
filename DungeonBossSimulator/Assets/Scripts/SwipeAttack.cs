using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeAttack : MonoBehaviour
{
    public GameObject swipeAttack;
    private GameObject playerModel;
    private SpriteRenderer spriteR;
    private SpriteRenderer selfSprite;

    public float cooldownDuration = 1f; // The duration of the cooldown in seconds
    private float lastAttackTime; // The time when the last attack occurred

    void Start()
    {
        playerModel = PlayerStats.playerStats.playerModel;
        spriteR = playerModel.GetComponent<SpriteRenderer>();
        selfSprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time - lastAttackTime >= cooldownDuration)
            {
                TriggerSwipeAttack();
                lastAttackTime = Time.time; // Update the last attack time
            }
        }
    }

    void TriggerSwipeAttack()
    {
        if (swipeAttack != null)
        {
            bool isFlipped = spriteR.flipX;

            // Calculate the position offset based on the flip status
            Vector3 positionOffset = isFlipped ? new Vector3(0.2f, -0.12f, 0) : new Vector3(-0.2f, -0.12f, 0);

            // Instantiate the swipe attack at the desired position relative to the player
            GameObject swipe = Instantiate(swipeAttack, playerModel.transform.position + positionOffset, Quaternion.identity);
            swipe.transform.SetParent(playerModel.transform);

            // Get the SpriteRenderer component of the swipe attack
            SpriteRenderer swipeSprite = swipe.GetComponent<SpriteRenderer>();

            // Set the flipX property of the swipe attack's sprite based on the flip status of the player model
            swipeSprite.flipX = !isFlipped;

            // Add the SwipeAttackScript component and initialize despawning
            SwipeAttackScript attackScript = swipe.AddComponent<SwipeAttackScript>();
            attackScript.InitializeDespawn();
        }
    }
}
