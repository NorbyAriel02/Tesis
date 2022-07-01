using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityCellTuto : Cell
{
    public delegate void EnterCity(int step);
    public static EnterCity OnEnterCity;
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
        //Debug.Log("Enter City");
        OnEnterCity?.Invoke(2);
    }
}
