using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonRangedMinionUpgrade : Upgrade
{
    public GameObject minion;
    public GameObject minion2;

    public void upgrade()
    {
        minion = GameObject.Instantiate(PlayerStats.playerStats.rangedMinion, new Vector3(0, 0, 0), Quaternion.identity);
        GameObject.DontDestroyOnLoad(minion);
        PlayerStats.playerStats.minionsToActivate.Add(minion);
        if (!PlayerStats.playerStats.upgradePool.ContainsKey("Increase Ranged Minion Damage"))
        {
            PlayerStats.playerStats.upgradePool.Add("Increase Ranged Minion Damage", new IncreaseRangedMinionDamageUpgrade());
        }
    }

    
}
