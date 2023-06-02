
using System.Collections;
using System.Collections.Generic;
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

    public void ToggleDeathPanel() 
    {
        deathPanel.SetActive(!deathPanel.activeSelf);
    }

    public void ToggleNextLevelPanel() 
    {
        int randomIndex = Random.Range(0, PlayerStats.playerStats.upgradePool.Count);
        string randomKey = PlayerStats.playerStats.upgradePool.Keys.ToList()[randomIndex];

        TextMeshProUGUI buttonText = Upgrade1.GetComponentInChildren<TextMeshProUGUI>(); // Access the Text component of the button
        buttonText.text = randomKey; // Update the text on the button

        upgradePanel.SetActive(true);
        Upgrade1.onClick.AddListener(() => 
        {
            PlayerStats.playerStats.upgradePool[randomKey].upgrade();
            upgradePanel.SetActive(false);
        });

        nextLevelPanel.SetActive(!nextLevelPanel.activeSelf);
    }
}