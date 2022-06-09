using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBattleAnimator : MonoBehaviour
{
    public IdleBattleManager idleBattleManager;
    public void StartBattle()
    {
        idleBattleManager.InBattle();
    }
}
