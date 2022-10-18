using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NeighboringKingdomsController : MonoBehaviour
{
    public delegate void KingdomChange(int id);
    public static KingdomChange OnKingdomChange;
    
    private DataFileController fileController = new DataFileController();
    private List<KingdomModel> listKingdomNeighbor;    
    private LoadMaps loadMaps;
    private Kingdom kingdom;
    private void OnEnable()
    {
        LimitCell.OnCellAction += LoadMap;
    }
    private void OnDisable()
    {
        LimitCell.OnCellAction -= LoadMap;
    }
    void Start()
    {
        kingdom = GetComponent<Kingdom>();
        listKingdomNeighbor = fileController.GetEncryptedData<List<KingdomModel>>(PathHelper.NeighborKingdomDataFile);
        loadMaps = GetComponent<LoadMaps>();
        kingdom.idKingdom = PlayerDataHelper.GetIdCurrentKingdom();         
    }
    public void LoadMapNorth()
    {
        kingdom.idKingdom = listKingdomNeighbor[kingdom.idKingdom - 1].NorthernNeighbor;    
        loadMaps.LoadGrid(kingdom.idKingdom);         
    }
    public void LoadMapSouth()
    {
        kingdom.idKingdom = listKingdomNeighbor[kingdom.idKingdom - 1].SouthNeighbor;        
        loadMaps.LoadGrid(kingdom.idKingdom);
    }
    public void LoadMapEast()
    {
        kingdom.idKingdom = listKingdomNeighbor[kingdom.idKingdom-1].EastNeighbor;        
        loadMaps.LoadGrid(kingdom.idKingdom);
    }
    public void LoadMapWest()
    {
        kingdom.idKingdom = listKingdomNeighbor[kingdom.idKingdom-1].WestNeighbor;        
        loadMaps.LoadGrid(kingdom.idKingdom);
    }

    public void LoadMap(int CellLimitId)
    {
        loadMaps.UpdateDataKingdom();
        switch (CellLimitId)
        {
            case 0:
                LoadMapEast();
                break;
            case 1:
                LoadMapWest();
                break;
            case 2:
                LoadMapNorth();
                break;
            case 3:
                LoadMapSouth();
                break;
        }
        OnKingdomChange?.Invoke(CellLimitId);
    }
    void Test()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            LoadMapWest();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            LoadMapEast();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            LoadMapSouth();
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            LoadMapNorth();
        }
    }
}
