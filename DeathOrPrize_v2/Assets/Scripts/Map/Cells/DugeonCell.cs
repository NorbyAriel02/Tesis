using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DugeonCell : Cell
{
    public int IDboss;
    private BossQuest bossQuest;
    void Start()
    {
        base.StartVar();
        bossQuest = GetScript.Type<BossQuest>("Boss", this.name);
    }
    public override void ActionCell()
    {
        EnterCave();
    }
    void EnterCave()
    {
        DiceReset();
        bossQuest.StartBattle(IDboss);
    }
}
