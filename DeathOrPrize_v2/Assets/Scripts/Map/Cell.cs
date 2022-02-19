using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public float x;
    public float y;
    public float index;
    public int sizeKingdom;
    public CellType type;
    public SubCellType subtype;
    public BiomeType biome;
    private NeighboringKingdomsController neighboringKingdomsController;
    private GameObject border;
    private PlayerMove playerMove;
    private CamaraMove cam;
    private int _kingdomID;
    void Start()
    {
        neighboringKingdomsController = GetComponentInParent<NeighboringKingdomsController>();
        border = ChildrenController.GetChildWithTag(gameObject, "Border");
        playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CamaraMove>();
    }

    void ActionCell()
    {
        switch (type.id)
        {
            case 0:
            case 2:
                MovePlayer();
                break;
            case 3:
                Limit();
                break;
        }
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
    void Limit()
    {
        if (playerMove.diceValue <= 0)
            return;

        switch (subtype.id)
        {
            case 0:
                neighboringKingdomsController.LoadMapEast();
                break;
            case 1:
                neighboringKingdomsController.LoadMapWest();
                break;
            case 2:
                neighboringKingdomsController.LoadMapNorth();
                break;
            case 3:
                neighboringKingdomsController.LoadMapSouth();
                break;
        }
        SetPositionNewKingdom();
    }
    void SetPositionNewKingdom()
    {
        cam.ActiveLoadKingdom();
        float xn = -1;
        float yn = -1;

        if(x == 0)
        {
            xn = sizeKingdom - 2;
            yn = y;
        }
        if(x == (sizeKingdom-1))
        {
            xn = 1;
            yn = y;
        }
        if (y == (sizeKingdom - 1))
        {
            xn = x;
            yn = 1;
        }
        if (y == 0)
        {
            xn = x;
            yn = sizeKingdom - 2;
        }
        playerMove.SetPositionNewKingdom(xn, yn);        
    }
    void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && IsIntoDistance)
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
