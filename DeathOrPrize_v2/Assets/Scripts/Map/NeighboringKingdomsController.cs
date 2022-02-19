using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeighboringKingdomsController : MonoBehaviour
{
    private DataFileController fileController = new DataFileController();
    private List<KingdomModel> listKingdomNeighbor;
    private int IdKingdom = 0;
    private LoadMaps loadMaps;    
    void Start()
    {        
        listKingdomNeighbor = fileController.GetData<List<KingdomModel>>(PathHelper.NeighborKingdomDataFile);
        loadMaps = GetComponent<LoadMaps>();
    }
    public void LoadMapNorth()
    {
        IdKingdom = listKingdomNeighbor[IdKingdom].NorthernNeighbor;
        loadMaps.LoadGrid(IdKingdom);         
    }
    public void LoadMapSouth()
    {
        IdKingdom = listKingdomNeighbor[IdKingdom].SouthNeighbor;
        loadMaps.LoadGrid(IdKingdom);
    }
    public void LoadMapEast()
    {
        IdKingdom = listKingdomNeighbor[IdKingdom].EastNeighbor;
        loadMaps.LoadGrid(IdKingdom);
    }
    public void LoadMapWest()
    {
        IdKingdom = listKingdomNeighbor[IdKingdom].WestNeighbor;
        loadMaps.LoadGrid(IdKingdom);
    }


    void Test()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            LoadMapWest();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            LoadMapEast();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            LoadMapSouth();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            LoadMapNorth();
        }
    }
    void Update()
    {
        //Test();
    }
}
