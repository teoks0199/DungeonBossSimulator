using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [SerializeField] public float enemyCount;
    [SerializeField] public GameObject playerPrefab;

    private void Awake()
    {
        if (LevelManager.instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void GameOver()
    {
        UIManager _ui = GetComponent<UIManager>();
        if (_ui != null) 
        {
            _ui.ToggleDeathPanel();
        }
    }

    public void NextLevel()
    {
        UIManager _ui = GetComponent<UIManager>();
        if (_ui != null) 
        {
            _ui.ToggleNextLevelPanel();
        }
    }

}
