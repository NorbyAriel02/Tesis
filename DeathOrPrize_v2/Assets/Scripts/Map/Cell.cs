using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public float x;
    public float y;
    public int index;
    public int sizeKingdom;
    public CellType type;
    public SubCellType subtype;
    public BiomeType biome;
    private NeighboringKingdomsController neighboringKingdomsController;
    private GameObject border;
    private PlayerMove playerMove;    
    private IdleBattleManager battleManager;
    private CityController city;
    void Start()
    {
        neighboringKingdomsController = GetComponentInParent<NeighboringKingdomsController>();
        border = ChildrenController.GetChildWithTag(gameObject, "Border");
        playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
        battleManager = GetScript.Type<IdleBattleManager>("Battle", this.name);
        city = GameObject.FindGameObjectWithTag("City").GetComponent<CityController>();
    }
    void ActionCell()
    {
        switch (type.id)
        {
            case 1:
                EnterCity();
                break;
            case 0:
            case 2:
                MovePlayer();
                StartBattle();
                break;
            case 3:
                Limit();
                break;
        }
    }
    void EnterCity()
    {
        playerMove.diceValue = 0;
        city.Enter(x, y, subtype.id);
    }
    void MovePlayer()
    {
        if (IsIntoDistance && playerMove.diceValue > 0)
        {
            playerMove.SetPosition(x, y);
            playerMove.diceValue--;
        }            
        else
            Debug.Log("No es posible");
    }
    void StartBattle()
    {
        if (playerMove.diceValue == 0)
        {
            battleManager.StartBattle(index);
        }
    }
    void Limit()
    {
        if (playerMove.diceValue <= 0)
            return;
        
        neighboringKingdomsController.LoadMap(subtype.id);

        playerMove.SetPositionNewKingdom(x, y, sizeKingdom);
    }    
    void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && IsIntoDistance && playerMove.diceValue > 0)
        {
            ActionCell();
        }

        if (border != null && IsIntoDistance)
            border.SetActive(true);
    }
    void OnMouseExit()
    {
        if (border != null)
            border.SetActive(false);
    }
    bool IsIntoDistance
    {
        get {            
            if (Vector3.Distance(playerMove.GetCurrentPosition(), transform.position) < 1.5f)
                return true;

            return false;
        }
    }
}
