using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitCell : Cell
{
    private NeighboringKingdomsController neighboringKingdomsController;
    // Start is called before the first frame update
    void Start()
    {
        neighboringKingdomsController = GetComponentInParent<NeighboringKingdomsController>();
        base.StartVar();
    }

    public override void ActionCell()
    {
        Limit();
    }

    void Limit()
    {
        //if (!HasMovements)
        //    return;

        neighboringKingdomsController.LoadMap(subtype.id);

        SetPositionNextKingdom();
    }
}
