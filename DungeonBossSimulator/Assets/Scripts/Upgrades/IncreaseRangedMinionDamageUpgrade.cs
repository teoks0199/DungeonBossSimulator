using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseRangedMinionDamageUpgrade : Upgrade
{
    public void upgrade()
    {
        PlayerStats.playerStats.rangedMinionDamage += 5;
        PlayerStats.playerStats.rangedMinionProjSpeed += 0.25F;
    }
}
