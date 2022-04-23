using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellQuest : MonoBehaviour
{    
    public int IDboss;    
    private PlayerMove playerMove;
    private BossQuest bossQuest;
    private Cell cell;
    void Start()
    {   
        playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
        bossQuest = GetScript.Type<BossQuest>("Boss", this.name);
        cell = GetComponent<Cell>();
    }
    void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && IsIntoDistance && playerMove.diceValue > 0)
        {
            if(cell.subtype.id == 1)
                EnterCave();
        }
    }    
    bool IsIntoDistance
    {
        get
        {
            if (Vector3.Distance(playerMove.GetCurrentPosition(), transform.position) < 1.5f)
                return true;

            return false;
        }
    }    
    void EnterCave()
    {
        playerMove.diceValue = 0;
        bossQuest.StartBattle(IDboss);
    }
}
