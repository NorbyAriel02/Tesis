using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellTutorial : Cell
{    
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
        Debug.Log("Turorial");
    }
}
