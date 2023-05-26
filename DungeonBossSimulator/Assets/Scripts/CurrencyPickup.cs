using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyPickup : MonoBehaviour
{
    //public enum PickupObject{COIN,GEM};
    //public PickupObject currentObject;
    public int pickupQuantity;

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player") 
        {
            /*if (currentObject == PickupObject.COIN)
            {
                PlayerStats.playerStats.coins += pickupQuantity;
                Debug.Log(PlayerStats.playerStats.coins);
            }
            else if (currentObject == PickupObject.GEM)
            {
                PlayerStats.playerStats.gems += pickupQuantity;
                Debug.Log(PlayerStats.playerStats.gems);
            }*/

            PlayerStats.playerStats.coins += pickupQuantity;
            Debug.Log(PlayerStats.playerStats.coins);
            Destroy(gameObject);
        }
    }
}