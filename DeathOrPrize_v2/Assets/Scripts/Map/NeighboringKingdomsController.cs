using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NeighboringKingdomsController : MonoBehaviour
{
    public Text textReino;
    private int IdKingdom = 0;
    private DataFileController fileController = new DataFileController();
    private List<KingdomModel> listKingdomNeighbor;    
    private LoadMaps loadMaps;    
    void Start()
    {        
        listKingdomNeighbor = fileController.GetEncryptedData<List<KingdomModel>>(PathHelper.NeighborKingdomDataFile);
        loadMaps = GetComponent<LoadMaps>();
    }
    public void LoadMapNorth()
    {
        IdKingdom = listKingdomNeighbor[IdKingdom].NorthernNeighbor;
        textReino.text = "Reino " + IdKingdom;
        loadMaps.LoadGrid(IdKingdom);         
    }
    public void LoadMapSouth()
    {
        IdKingdom = listKingdomNeighbor[IdKingdom].SouthNeighbor;
        textReino.text = "Reino " + IdKingdom;
        loadMaps.LoadGrid(IdKingdom);
    }
    public void LoadMapEast()
    {
        IdKingdom = listKingdomNeighbor[IdKingdom].EastNeighbor;
        textReino.text = "Reino " + IdKingdom;
        loadMaps.LoadGrid(IdKingdom);
    }
    public void LoadMapWest()
    {
        IdKingdom = listKingdomNeighbor[IdKingdom].WestNeighbor;
        textReino.text = "Reino " + IdKingdom;
        loadMaps.LoadGrid(IdKingdom);
    }

    public void LoadMap(int CellLimitId)
    {
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
        PlayerDataHelper.UpdateIdKingdom(IdKingdom);
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
