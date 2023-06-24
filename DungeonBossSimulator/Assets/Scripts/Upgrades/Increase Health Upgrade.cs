using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseHealthUpgrade : Upgrade
{
    // Start is called before the first frame update
    public void upgrade()
    {
        PlayerStats.playerStats.maxHealth += 50;
        PlayerStats.playerStats.HealCharacter(50);
        //Debug.Log("Upgraded");
    }
}
