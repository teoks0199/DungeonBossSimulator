using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeAttack : MonoBehaviour
{
    public GameObject swipeAttack;
    private GameObject playerModel;
    private SpriteRenderer spriteR;
    private SpriteRenderer selfSprite;

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
            if (Time.time - lastAttackTime >= PlayerStats.playerStats.swipeAttackCoolDown)
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
            // Get the mouse position in world space
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // Make sure the z-coordinate is 0 for 2D games

            // Calculate the direction from the player model to the mouse position
            Vector3 direction = mousePosition - playerModel.transform.position;
            direction.Normalize(); // Normalize the direction vector to have a magnitude of 1

            // Define the desired length of the direction vector
            float desiredLength = 0.3f;

            // Multiply the direction vector by the desired length
            direction *= desiredLength;

            // Instantiate the swipe attack at the player's position and add the direction to its position
            GameObject swipe = Instantiate(swipeAttack, playerModel.transform.position + direction, Quaternion.identity);
            swipe.transform.SetParent(playerModel.transform);

            // Get the SpriteRenderer component of the swipe attack
            SpriteRenderer swipeSprite = swipe.GetComponent<SpriteRenderer>();

            // Set the flipX property of the swipe attack's sprite based on the flip status of the player model
            swipeSprite.flipX = !spriteR.flipX;

            PlayerStats.playerStats.isSwipeAttackCooldown = true;
            PlayerStats.playerStats.swipeAttackCoolDownImage();

            // Add the SwipeAttackScript component and initialize despawning
            SwipeAttackScript attackScript = swipe.AddComponent<SwipeAttackScript>();
            attackScript.InitializeDespawn();
        }
    }



}
