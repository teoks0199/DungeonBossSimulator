using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeDamageUpgrade : Upgrade
{
    public void upgrade()
    {
        PlayerStats.playerStats.swipeDamage += 3;
    }
}
