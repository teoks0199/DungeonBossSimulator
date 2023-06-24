using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealUpgrade : Upgrade
{
    // Start is called before the first frame update
    public void upgrade()
    {
        PlayerStats.playerStats.HealCharacter(100);
        //Debug.Log("Upgraded");
    }
}
