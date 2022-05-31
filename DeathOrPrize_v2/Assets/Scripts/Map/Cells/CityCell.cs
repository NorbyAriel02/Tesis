using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityCell : Cell
{
    private CityController city;
    void Start()
    {
        base.StartVar();
        city = GameObject.FindGameObjectWithTag("City").GetComponent<CityController>();
    }
    public override void ActionCell()
    {
        EnterCity();
    }
    void EnterCity()
    {
        DiceReset();
        PlayerDataHelper.UpdateIdCurrentKingdom(cellData.IDkingdom);
        city.Enter(x, y, subtype.id);
    }
}
