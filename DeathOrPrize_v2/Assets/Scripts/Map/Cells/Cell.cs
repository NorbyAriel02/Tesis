using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public CellModel cellData;
    public float x;
    public float y;
    public int index;
    public int sizeKingdom;
    public CellType type;
    public SubCellType subtype;
    public BiomeType biome;    
    private PlayerMove playerMove;
    private IdleBattleManager battleManager;    
    void Start()
    {
        StartVar();
    }
    public virtual void StartVar()
    {        
        playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
        battleManager = GetScript.Type<IdleBattleManager>("Battle", this.name);        
    }
    public virtual void ActionCell()
    {
        Debug.Log("action " + gameObject.name);
        MovePlayer();
        if (!HasMovements)
            StartBattle();
    }
    void MovePlayer()
    {
        playerMove.SetPosition(x, y);
        playerMove.diceValue--;
    }
    public void DiceReset()
    {
        playerMove.diceValue = 0;
    }    
    public bool HasMovements
    {
        get 
        {
            return playerMove.diceValue <= 0 ? false : true;
        }        
    }
    public void SetPositionNextKingdom()
    {
        playerMove.SetPositionNewKingdom(x, y, sizeKingdom);
    }
    void StartBattle()
    {
        battleManager.StartBattle(index);
    }
          
}
