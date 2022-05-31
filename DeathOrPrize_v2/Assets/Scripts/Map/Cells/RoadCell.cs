using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadCell : Cell
{
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
        //base.ActionCell();
        MovePlayer();
        if (!HasMovements)
            if (cicle.IsDay)
                dialogs.StartDialogue();
            else
                StartBattle();

    }
}
