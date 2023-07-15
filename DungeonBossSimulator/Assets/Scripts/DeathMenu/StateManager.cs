using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{
    public void ReloadCurrentScene() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ChangeSceneByName(string name)
    {
        if (name != null)
        {
            if (name == "MainMenu") 
            {
                PlayerStats.playerStats.DestroyGameObjectsWithTag("Minion");
                Destroy(PlayerStats.playerStats.playerModel);
                Destroy(PlayerStats.playerStats.healthCanvas);
                Destroy(PlayerStats.playerStats);
                
            }
            
            SceneManager.LoadScene(name);
            PlayerStats.playerStats.respawnMinions();
        }
    }
}
