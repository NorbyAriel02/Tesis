using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMaps : MonoBehaviour
{
    public bool ActiveText;
    public GameObject[] prefabTiles;
    public GameObject[] prefabCity;
    public GameObject[] prefabRoad;
    public GameObject[] prefabLimit;
    List<CellModel> dataMap = new List<CellModel>();
    DataFileController fileController = new DataFileController();
    void Start()
    {
        LoadGrid(0);
    }

    public void LoadGrid(int idKingdom)
    {
        ChildrenController.RemoveAllChildren(gameObject);
        dataMap = fileController.GetData<List<CellModel>>(PathHelper.WolrdDataFile(idKingdom));

        if (dataMap == null)
            Debug.Log("no se cargan los datos del mapa");

        foreach (CellModel cellData in dataMap)
        {
            if (cellData.type.id == 0)
                InstantiateDefault(cellData);
            if (cellData.type.id == 1)
                InstantiateCity(cellData);
            if (cellData.type.id == 2)
                InstantiateRoad(cellData);
            if (cellData.type.id == 3)
                InstantiateLimit(cellData);
        }
    }
    void InstantiateDefault(CellModel cellData)
    {
        GameObject goCell = Instantiate(prefabTiles[cellData.subtype.id], transform);
        SetData(goCell, cellData);
        if (ActiveText)
        {
            goCell.GetComponentInChildren<TextMesh>().text = cellData.x + "-" + cellData.y;
        }
    }    
    void InstantiateCity(CellModel cellData)
    {
        GameObject goCell = Instantiate(prefabCity[cellData.subtype.id], transform);
        SetData(goCell, cellData);
    }
    void InstantiateRoad(CellModel cellData)
    {
        GameObject goCell = Instantiate(prefabRoad[cellData.subtype.id], transform);
        SetData(goCell, cellData);
    }
    void InstantiateLimit(CellModel cellData)
    {
        GameObject goCell = Instantiate(prefabLimit[cellData.subtype.id], transform);
        SetData(goCell, cellData);
    }
    void SetData(GameObject goCell, CellModel cellData)
    {
        goCell.transform.position = new Vector3(cellData.x, cellData.y, 0);
        Cell cellScript = goCell.GetComponent<Cell>();
        cellScript.x = cellData.x;
        cellScript.y = cellData.y;
        cellScript.index = cellData.index;
        cellScript.type = cellData.type;
        cellScript.subtype = cellData.subtype;
        cellScript.biome = cellData.biome;
        cellScript.sizeKingdom = cellData.sizeKingdom;
    }
}