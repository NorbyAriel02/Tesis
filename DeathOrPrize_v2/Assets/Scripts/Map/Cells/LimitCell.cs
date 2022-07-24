using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitCell : Cell
{
    public delegate void CellAction(int id);
    public static CellAction OnCellAction;
    public delegate void SetPositionKingdom(float x, float y, int sizeKingdom);
    public static SetPositionKingdom OnSetPositionKingdom;    
    
    void Start()
    {    
        base.StartVar();
    }
    public override void ActionCell()
    {
        Limit();
    }
    void Limit()
    {           
        OnCellAction?.Invoke(subtype.id);
        OnSetPositionKingdom?.Invoke(this.x, this.y, this.sizeKingdom);
    }
}
