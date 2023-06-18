using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseAuraDmgUpgrade : Upgrade
{
    public void upgrade()
    {
        PlayerStats.playerStats.auraBuffDamage += 5F;
    }
}
