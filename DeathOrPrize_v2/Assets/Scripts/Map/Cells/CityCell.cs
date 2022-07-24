using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityCell : Cell
{    
    public delegate void DoorCity(float x, float y, int subTypeId);    
    public static DoorCity ClicOnDoorCity;    
    
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
        DataHelper.UpdateIdCurrentKingdom(cellData.IDkingdom);
        ClicOnDoorCity?.Invoke(x, y, subtype.id);        
    }
}
