using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellTutorial : Cell
{
    public delegate void ActionCellTutorial();
    public static ActionCellTutorial OnActionCell;
    void Start()
    {
        base.StartVar();        
    }
    public override void ActionCell()
    {
        Tutorial();
    }
    void Tutorial()
    {
        //Debug.Log("Turorial");
        OnActionCell?.Invoke();
    }
}
