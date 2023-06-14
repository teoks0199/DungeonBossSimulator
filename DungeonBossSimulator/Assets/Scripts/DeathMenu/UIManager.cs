
using System.Collections;
using System.Collections.Generic;
//using System.ValueTuple;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject deathPanel;
    [SerializeField] GameObject nextLevelPanel;
    [SerializeField] GameObject upgradePanel;
    public Button Upgrade1;
    public Button Upgrade2;
    public Button Upgrade3;
    //Dictionary<string, Upgrade> upgradePool = PlayerStats.playerStats.upgradePool;
    public static (string, Upgrade)[] upgradeChoices;
    

    public void ToggleDeathPanel() 
    {
        deathPanel.SetActive(!deathPanel.activeSelf);
    }

    public void ToggleNextLevelPanel() 
    {
        //int randomIndex = Random.Range(0, PlayerStats.playerStats.upgradePool.Count);
        //string randomKey = PlayerStats.playerStats.upgradePool.Keys.ToList()[randomIndex];
        upgradeChoices =  new (string, Upgrade)[] {getRandomUpgrade(), getRandomUpgrade(), getRandomUpgrade()};

        Upgrade1.GetComponentInChildren<TextMeshProUGUI>().text = upgradeChoices[0].Item1; // Update the text on the button
        Upgrade2.GetComponentInChildren<TextMeshProUGUI>().text = upgradeChoices[1].Item1;
        Upgrade3.GetComponentInChildren<TextMeshProUGUI>().text = upgradeChoices[2].Item1;

        upgradePanel.SetActive(true);
        Upgrade1.onClick.AddListener(() => 
        {
            //PlayerStats.playerStats.upgradePool[randomKey].upgrade();
            upgradeChoices[0].Item2.upgrade();
            upgradePanel.SetActive(false);
            restoreUpgradePool();
        });
        Upgrade2.onClick.AddListener(() => 
        {
            //PlayerStats.playerStats.upgradePool[randomKey].upgrade();
            upgradeChoices[1].Item2.upgrade();
            upgradePanel.SetActive(false);
            restoreUpgradePool();
        });
        Upgrade3.onClick.AddListener(() => 
        {
            //PlayerStats.playerStats.upgradePool[randomKey].upgrade();
            upgradeChoices[2].Item2.upgrade();
            upgradePanel.SetActive(false);
            restoreUpgradePool();
        });



        nextLevelPanel.SetActive(!nextLevelPanel.activeSelf);

    }

    private (string, Upgrade) getRandomUpgrade()
    {
        int randomIndex = Random.Range(0, PlayerStats.playerStats.upgradePool.Count);
        string randomKey = PlayerStats.playerStats.upgradePool.Keys.ToList()[randomIndex];
        (string, Upgrade) u = (randomKey, PlayerStats.playerStats.upgradePool[randomKey]);
        PlayerStats.playerStats.upgradePool.Remove(u.Item1);
        return u;
    }

    private void restoreUpgradePool()
    {
        for (int i = 0; i < upgradeChoices.Length; i++) 
            {
                PlayerStats.playerStats.upgradePool.Add(upgradeChoices[i].Item1, upgradeChoices[i].Item2);
            }
    }
}