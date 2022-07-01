using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ConditionController : MonoBehaviour
{
    public bool tutorial;
    public GameObject panelGameOver;
    private PlayerPosition playerPosition;
    private PlayerStats playerStats;
    // Start is called before the first frame update
    void Start()
    {        
        playerStats = GetScript.Type<PlayerStats>("Player");
        playerPosition = GetScript.Type<PlayerPosition>("Player");
        panelGameOver.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {        
        if(playerStats.stats.currentHealth <= 0 && !panelGameOver.activeSelf)
        {
            if(tutorial)
            {
                PlayerDataHelper.StartNewGame();
            }
            else
                playerPosition.TrasladePlayerStartPosition();
            panelGameOver.SetActive(true);
        }
    }
}
