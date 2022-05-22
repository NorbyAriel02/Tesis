using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMaps : MonoBehaviour
{
    public bool ActiveText;
    public List<Sprite> spriteCountry;
    public GameObject[] prefabTiles;
    public GameObject[] prefabCity;
    public GameObject[] prefabRoad;
    public GameObject[] prefabLimit;
    public GameObject[] prefabCave;
    List<CellModel> dataMap = new List<CellModel>();
    DataFileController fileController = new DataFileController();
    
    void Start()
    {  
        
        LoadGrid(PlayerDataHelper.GetIdKingdom());
    }

    public void LoadGrid(int idKingdom)
    {   
        ChildrenController.RemoveAllChildren(gameObject);
        dataMap = fileController.GetEncryptedData<List<CellModel>>(PathHelper.WolrdDataFile(idKingdom));

        if (dataMap == null)
            Debug.Log("no se cargan los datos del mapa");

        foreach (CellModel cellData in dataMap)
        {
            if (cellData.type.id == 0)//campo
                InstantiateCell(prefabTiles, cellData);
            if (cellData.type.id == 1)//city
                InstantiateCell(prefabCity, cellData);
            if (cellData.type.id == 2)//camino
                InstantiateCell(prefabRoad, cellData);
            if (cellData.type.id == 3)//limite del mapa
                InstantiateCell(prefabLimit, cellData);
            if (cellData.type.id == 4)//limite del mapa
                InstantiateCell(prefabCave, cellData);
        }
    }
    void InstantiateCell(GameObject[] listGO, CellModel cellData)
    {
        GameObject goCell = Instantiate(listGO[cellData.subtype.id], transform);
        SetData(goCell, cellData);
        SetBiome(goCell);   
        if (ActiveText)
            LogDev(goCell, cellData);
    }
    void LogDev(GameObject goCell, CellModel cellData)
    {
        TextMesh tm = goCell.GetComponentInChildren<TextMesh>();
        if(tm != null)
            goCell.GetComponentInChildren<TextMesh>().text = cellData.x + "-" + cellData.y;
    }    
    void SetData(GameObject goCell, CellModel cellData)
    {
        goCell.transform.position = new Vector3(cellData.x, cellData.y, 0);
        Cell cellScript = goCell.GetComponent<Cell>();
        cellScript.cellData = cellData;
        cellScript.x = cellData.x;
        cellScript.y = cellData.y;
        cellScript.index = cellData.index;
        cellScript.type = cellData.type;
        cellScript.subtype = cellData.subtype;
        cellScript.biome = cellData.biome;
        cellScript.sizeKingdom = cellData.sizeKingdom;
    }
    void SetBiome(GameObject go)
    {        
        Cell cellScript = go.GetComponent<Cell>();
        if (cellScript.type.id != 0)
            return;

        switch(cellScript.biome.id)
        {
            case 1:
                go.GetComponent<SpriteRenderer>().sprite = spriteCountry[0];
                break;
            case 2:
                go.GetComponent<SpriteRenderer>().sprite = spriteCountry[1];
                break;
            case 3:
                go.GetComponent<SpriteRenderer>().sprite = spriteCountry[2];
                break;
            case 4:
                go.GetComponent<SpriteRenderer>().sprite = spriteCountry[3];
                break;
            case 5:
                go.GetComponent<SpriteRenderer>().sprite = spriteCountry[4];
                break;
            case 6:
                go.GetComponent<SpriteRenderer>().sprite = spriteCountry[5];
                break;
            case 7:
                go.GetComponent<SpriteRenderer>().sprite = spriteCountry[6];
                break;
            case 8:
                go.GetComponent<SpriteRenderer>().sprite = spriteCountry[7];
                break;
            case 9:
                go.GetComponent<SpriteRenderer>().sprite = spriteCountry[8];
                break;
        }
    }
}
