using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SummonMeleeMinionUpgrade : Upgrade
{
    public GameObject minion;
    public GameObject minion2;

    public void upgrade()
    {
        minion = GameObject.Instantiate(PlayerStats.playerStats.meleeMinion, new Vector3(0, 0, 0), Quaternion.identity);
        minion2 = GameObject.Instantiate(PlayerStats.playerStats.meleeMinion, new Vector3(1, 1, 1), Quaternion.identity);
        GameObject.DontDestroyOnLoad(minion);
        GameObject.DontDestroyOnLoad(minion2);
    }

    
}
