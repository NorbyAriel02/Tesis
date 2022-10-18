using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusBattleController : MonoBehaviour
{
    public delegate void SettingEnemies(EnemiesXcellModel enemiesXcellModel);
    public static SettingEnemies OnSettingEnemies;
    public delegate void EndBattle();
    public static EndBattle OnBattleEnd;

    EnemiesXcellModel enemiesXcellModel;
    private AttackController[] attackControllers;
    private void OnEnable()
    {
        TempIdleBattleManager.OnExecuteAttack += CheckStatusBattle;
        SelectorDeEscenarios.OnBattle += LoadDataEnemies;
    }
    private void OnDisable()
    {
        TempIdleBattleManager.OnExecuteAttack += CheckStatusBattle;
        SelectorDeEscenarios.OnBattle += LoadDataEnemies;
    }
    void LoadDataEnemies(EnemiesXcellModel enemies)
    {
        enemiesXcellModel = enemies;        
    }
    void StartBattleNow()
    {//se llama al final de la animacion
        attackControllers = FindObjectsOfType<AttackController>();
        OnSettingEnemies?.Invoke(enemiesXcellModel);
    }
    void CheckStatusBattle()
    {
        if(DataHelper.GetStats().currentHealth <= 0)
        {
            OnBattleEnd?.Invoke();
            return;
        }

        for (int index = 0; index < enemiesXcellModel.enemies.Count; index++)
        {
            if (enemiesXcellModel.enemies[index].currentHealth <= 0)
            {
                DisableEnemy(index);
                enemiesXcellModel.enemies.Remove(enemiesXcellModel.enemies[index]);
            }
        }
        
        if (enemiesXcellModel.enemies.Count < 1)
            OnBattleEnd?.Invoke();
        
    }
    void DisableEnemy(int indexEnemy)
    {
        for (int index = 0; index < attackControllers.Length; index++)
        {
            if (attackControllers[index].GetType() == typeof(EnemyAttacks))
            {
                EnemyAttacks ea = (EnemyAttacks)attackControllers[index];
                if(ea.indexEnemy == indexEnemy)
                {
                    ea.gameObject.SetActive(false);
                }
            }
        }
    }
}
