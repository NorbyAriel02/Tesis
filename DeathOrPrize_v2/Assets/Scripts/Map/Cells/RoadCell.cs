using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadCell : Cell
{
    public delegate void RoadDialogue();
    public static RoadDialogue OnRoadDialogue;
    public delegate void RoadBattle();
    public static RoadBattle OnRoadBattle;
    
    public Dialogue dialogs;
    public DayNightCicle cicle;
    void Start()
    {
        cicle = GameObject.FindGameObjectWithTag("Game").GetComponent<DayNightCicle>();
        dialogs = GameObject.FindGameObjectWithTag("Story").GetComponent<Dialogue>();
        StartVar();
    }
    public override void StartVar()
    {
        base.StartVar();
    }

    public override void ActionCell()
    {
        if (HasMovements)
            return;

        if (cicle.IsDay)
            dialogs.StartDialogue(cicle.diceRollNumber);
        else
            base.ActionCell();
    }
}
