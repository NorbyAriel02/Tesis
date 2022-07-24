using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public delegate bool CanAction();
    public static CanAction IsCellAction;
    public delegate void InCell();
    public static InCell OnPlayerInCell;
    public delegate void Action(int i);
    public static Action OnAction;

    public CellModel cellData;
    public float x;
    public float y;
    public int index;
    public int sizeKingdom;
    public CellType type;
    public SubCellType subtype;
    public BiomeType biome;        
    private IdleBattleManager battleManager;
    public bool ClickMe = false;
    
    void Start()
    {
        StartVar();
    }
    public virtual void StartVar()
    {   
        battleManager = GetScript.Type<IdleBattleManager>("Battle", this.name);        
    }
    public virtual void ActionCell()
    {
        if (!HasMovements)
            battleManager.StartBattle(index);        
    }
    public bool HasMovements
    {
        get
        {
            return IsCellAction();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("FacePlayer") && ClickMe)
            ActionCell();
                
        ClickMe = false;
    }
}
