using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionController : MonoBehaviour
{
    public GameObject panelGameOver;
    private PlayerStats playerStats;
    // Start is called before the first frame update
    void Start()
    {        
        playerStats = GetScript.Type<PlayerStats>("Player");
        panelGameOver.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(playerStats.stats.currentHealth <= 0)
        {
            panelGameOver.SetActive(true);
        }
    }
}
