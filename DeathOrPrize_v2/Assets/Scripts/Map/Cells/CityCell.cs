using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityCell : Cell
{
    public delegate void EnterCity(float x, float y, int subTypeId);    
    public static EnterCity OnEnterCity;    
    
    void Start()
    {
        base.StartVar();    
    }
    public override void ActionCell()
    {
        Enter();
    }
    void Enter()
    {        
        PlayerDataHelper.UpdateIdCurrentKingdom(cellData.IDkingdom);
        OnEnterCity?.Invoke(x, y, subtype.id);        
    }
}
