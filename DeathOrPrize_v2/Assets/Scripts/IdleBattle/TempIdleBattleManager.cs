using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempIdleBattleManager : MonoBehaviour
{
    public delegate void PrepareBattle();
    public static PrepareBattle OnPrepareBattle;
    public delegate void ActionExecuteAttack();
    public static ActionExecuteAttack OnExecuteAttack;
    public delegate void DamagedEnemy(float health, float healtMax, string name);
    public static DamagedEnemy OnDamagedEnemy;

    private AttackController[] attackControllers;
    PlayerStatsModel stastPlayer;
    EnemiesXcellModel enemiesXcellModel;    

    private void OnEnable()
    {
        AttackController.OnAttack += ExecuteAttack;
        SelectorDeEscenarios.OnBattle += LoadDataEnemies;
        StatusBattleController.OnSettingEnemies += StartingTheBattleNow;
    }
    private void OnDisable()
    {
        AttackController.OnAttack -= ExecuteAttack;
        SelectorDeEscenarios.OnBattle -= LoadDataEnemies;
        StatusBattleController.OnSettingEnemies -= StartingTheBattleNow;
    }
    void LoadDataEnemies(EnemiesXcellModel enemies)
    {        
        enemiesXcellModel = enemies;
        OnPrepareBattle?.Invoke();
    }
    void StartingTheBattleNow(EnemiesXcellModel enemiesXcellModel)
    {
        stastPlayer = DataHelper.GetStats();
        attackControllers = FindObjectsOfType<AttackController>();                
    }
    void AsignTargets()
    {
        attackControllers = FindObjectsOfType<AttackController>();
        Dictionary<string, string> targets = new Dictionary<string, string>();
        List<string> controllers = new List<string>();
        List<string> target = new List<string>();
        foreach (AttackController ac in attackControllers)
        {
            if(!controllers.Contains(ac.GetType().ToString()))
            {
                controllers.Add(ac.GetType().ToString());
                target.Add(ac.gameObject.name);
            }
        }
        foreach (AttackController ac in attackControllers)
        {
            if (controllers[0].Equals(ac.GetType().ToString()))
            {
                ac.AssignTarget(target[1]);
            }
            else
                ac.AssignTarget(target[0]);            
        }
    }
    public void ExecuteAttack(Attacks attacks)
    {
        if (attacks.Target == "Player")
        {
            EnemyAttack(attacks);
        }
        else
        {
            PlayerAttack(attacks);
        }
        OnExecuteAttack?.Invoke();
    }
    
    void EnemyAttack(Attacks attacks)
    {
        float d = attacks.Damage;
        float a = stastPlayer.equipment.armor;
        float damage = IdleBattleHelper.GetRealDamage(a, d);
        DataHelper.RestHealth(damage);        
    }
    void PlayerAttack(Attacks attacks)
    {
        float d = attacks.Damage;
        float a = enemiesXcellModel.enemies[0].armor;
        float damage = IdleBattleHelper.GetRealDamage(a, d);
        enemiesXcellModel.enemies[0].currentHealth -= damage;
        OnDamagedEnemy?.Invoke(enemiesXcellModel.enemies[0].currentHealth, enemiesXcellModel.enemies[0].maxHealth, attacks.Target);        
    }
}
