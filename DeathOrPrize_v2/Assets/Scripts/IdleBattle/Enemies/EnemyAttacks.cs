using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacks : DOP_AttackController
{
    public delegate void Setting();
    public static Setting OnSettingProperties;
    public int indexEnemy = 0;
    EnemyAnimationController animator;
    private void Start()
    {
        animator = GetComponent<EnemyAnimationController>();
    }
    private void OnEnable()
    {
        if(attacks == null)
            attacks = new Attacks();
        StatusBattleController.OnSettingEnemies += SettingProperties;
        StatusBattleController.OnBattleEnd += EndBattle;
    }
    private void OnDisable()
    {
        StatusBattleController.OnSettingEnemies -= SettingProperties;
        StatusBattleController.OnBattleEnd -= EndBattle;
    }
    private void SettingProperties(EnemiesXcellModel enemiesXcellModel)
    {
        if(indexEnemy >= enemiesXcellModel.enemies.Count)
            gameObject.SetActive(false);
        else
        {
            attackSpeed = enemiesXcellModel.enemies[indexEnemy].attackSpeed;
            attacks.Damage = enemiesXcellModel.enemies[indexEnemy].damage;
            animator.SetViewEnemy(enemiesXcellModel.enemies[indexEnemy].type.id);
            animator.Walk();
            OnSettingProperties?.Invoke();
        }        
    }
    public void StartBattle()
    {
        animator.Idle();
        this.timer = this.attackSpeed;
        this.inBattle = true;
    }
    private void EndBattle()
    {
        this.inBattle = false;
    }
    public override void Attack()
    {
        Debug.Log(gameObject.name + " ataca en el " + System.DateTime.Now.Second.ToString());
        animator.Attack();
        base.Attack();
    }
}
