using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDamageUpgrade : Upgrade
{
    // Start is called before the first frame update
    public void upgrade()
    {
        PlayerStats.playerStats.projectileDamage += 10;
    }
}
