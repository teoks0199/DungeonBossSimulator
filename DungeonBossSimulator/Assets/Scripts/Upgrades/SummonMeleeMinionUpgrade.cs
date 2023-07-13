using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SummonMeleeMinionUpgrade : Upgrade
{
    public GameObject minion;

    public void upgrade()
    {
        minion = GameObject.Instantiate(PlayerStats.playerStats.meleeMinion, new Vector3(0, 0, 0), Quaternion.identity);
        GameObject.DontDestroyOnLoad(minion);
    }

    
}
