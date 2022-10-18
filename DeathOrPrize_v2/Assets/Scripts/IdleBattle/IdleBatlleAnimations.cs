using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBatlleAnimations : MonoBehaviour
{
    public Animator animationPanel;
    private void OnEnable()
    {
        TempIdleBattleManager.OnPrepareBattle += StartBattleAnimation;
        StatusBattleController.OnBattleEnd += EndBattleAnimation;
    }
    private void OnDisable()
    {
        TempIdleBattleManager.OnPrepareBattle -= StartBattleAnimation;
        StatusBattleController.OnBattleEnd -= EndBattleAnimation;
    }
    void StartBattleAnimation()
    {
        animationPanel.SetBool("StartBattle", true);
        animationPanel.SetBool("EndBattle", false);
    }
    void EndBattleAnimation()
    {
        animationPanel.SetBool("StartBattle", false);
        animationPanel.SetBool("EndBattle", true);
    }
}
