using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseImpactDmgUpgrade : Upgrade
{
    public void upgrade()
    {
        PlayerStats.playerStats.impactAttackDamage += 10;
    }
}
