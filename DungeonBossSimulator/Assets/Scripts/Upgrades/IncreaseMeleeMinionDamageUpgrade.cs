using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseMeleeMinionDamageUpgrade : Upgrade
{
    public void upgrade()
    {
        PlayerStats.playerStats.meleeMinionDamage += 5;
    }
}
