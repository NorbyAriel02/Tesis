using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesBarHealth : MonoBehaviour
{
    public Image healthBar;
    
    private void Start()
    {
        
    }
    private void OnEnable()
    {
        TempIdleBattleManager.OnDamagedEnemy += UpdateHealth;
    }
    private void OnDisable()
    {
        TempIdleBattleManager.OnDamagedEnemy -= UpdateHealth;
    }
    public void UpdateHealth(float helth, float helthMax, string name)
    {
        Debug.Log(gameObject.name + " - " + name);
        if (gameObject.name == name)
        {
            healthBar.fillAmount = helth / helthMax;
        }            
    }
}
