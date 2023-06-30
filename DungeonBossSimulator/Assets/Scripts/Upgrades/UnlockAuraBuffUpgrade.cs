using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UnlockAuraBuffUpgrade : Upgrade
{
    public void upgrade()
    {
        PlayerStats.playerStats.playerModel.GetComponent<AuraBuff>().enabled = true;
        UIManager.upgradeChoices = UIManager.upgradeChoices.Where(t => t.Item1 != "Unlock Aura Buff").ToArray();
        PlayerStats.playerStats.upgradePool.Add("Increase Aura Damage", new IncreaseAuraDmgUpgrade());
    }
}
