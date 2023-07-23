using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseMeleeMinionDamageUpgrade : Upgrade
{
    public void upgrade()
    {
        PlayerStats.playerStats.meleeMinionDamage += 5;
        //PlayerStats.playerStats.meleeMinionSpeed += 0.05F;
    }
}
