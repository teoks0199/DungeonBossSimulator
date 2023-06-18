using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UnlockImpactAttackUpgrade : Upgrade
{
    // Start is called before the first frame update

    void Start ()
    {
        //.GetComponent<YourScript>().enabled = false;
    }

    public void upgrade()
    {
        PlayerStats.playerStats.playerModel.GetComponent<ImpactAttack>().enabled = true;
        //UIManager.upgradeChoices = UIManager.upgradeChoices.Where(t => !t.Equals(("Unlock Impact Attack", new UnlockImpactAttackUpgrade()))).ToArray();
        UIManager.upgradeChoices = UIManager.upgradeChoices.Where(t => t.Item1 != "Unlock Impact Attack").ToArray();
        PlayerStats.playerStats.upgradePool.Add("Increase Impact Attack Damage", new IncreaseImpactDmgUpgrade());
    }
}
