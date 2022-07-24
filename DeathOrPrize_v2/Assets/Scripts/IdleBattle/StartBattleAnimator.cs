using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBattleAnimator : MonoBehaviour
{
    public IdleBattleManager idleBattleManager;
    public BossQuest idleBattleBoss;
    public void StartBattle()
    {
        if(idleBattleManager != null)
            idleBattleManager.InBattle();
        
        if(idleBattleBoss != null)
            idleBattleBoss.InBattle();
    }
}
