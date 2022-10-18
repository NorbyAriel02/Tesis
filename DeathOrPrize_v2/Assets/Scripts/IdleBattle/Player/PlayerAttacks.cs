using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacks : DOP_AttackController
{
    PlayerBattleAnimation animator;
    private void OnEnable()
    {
        StatusBattleController.OnSettingEnemies += SettingStatsPlayer;
        StatusBattleController.OnBattleEnd += EndBattle;
        animator = GetComponent<PlayerBattleAnimation>();
    }
    private void OnDisable()
    {
        StatusBattleController.OnSettingEnemies -= SettingStatsPlayer;
        StatusBattleController.OnBattleEnd -= EndBattle;
    }
    private void SettingStatsPlayer(EnemiesXcellModel enemiesXcellModel)
    {
        PlayerStatsModel stast = DataHelper.GetStats();
        attackSpeed = stast.equipment.attackSpeed;
        attacks.Damage = stast.equipment.damage;
        animator.StartWalk();
    }
    private void EndBattle()
    {
        inBattle = false;
    }
    public override void Attack()
    {
        base.Attack();
    }
}
