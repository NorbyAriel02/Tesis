using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectorDeEscenarios : MonoBehaviour
{
    public delegate void Battle(EnemiesXcellModel enemies);
    public static Battle OnBattle;
    private void OnEnable()
    {
        Cell.OnPlayerInCell += DevibarEscenario;        
    }
    private void OnDisable()
    {
        Cell.OnPlayerInCell -= DevibarEscenario;
    }
    void DevibarEscenario(int indexCell)
    {
        int kingdom = DataHelper.GetIdCurrentKingdom();
        EnemiesXcellModel enemiesXcellModel = DataHelper.GetStatsEnemies(indexCell, kingdom);
        if (enemiesXcellModel.enemies.Count > 0)
        {            
            OnBattle?.Invoke(enemiesXcellModel);
        }
    }
}
