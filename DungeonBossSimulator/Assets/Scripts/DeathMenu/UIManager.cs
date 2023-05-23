using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject deathPanel;
    [SerializeField] GameObject nextLevelPanel;

    public void ToggleDeathPanel() 
    {
        deathPanel.SetActive(!deathPanel.activeSelf);
    }

    public void ToggleNextLevelPanel() 
    {
        nextLevelPanel.SetActive(!nextLevelPanel.activeSelf);
    }
}
