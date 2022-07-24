using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadMaps : MonoBehaviour
{
    public delegate void LoadMap();
    public static LoadMap OnLoadMap;
    public bool ActiveText;
    public List<string> paths;
    public GameObject[] prefabTiles;
    public GameObject[] prefabCity;
    public GameObject[] prefabRoad;
    public GameObject[] prefabLimit;
    public GameObject[] prefabCave;
    List<CellModel> dataMap = new List<CellModel>();
    DataFileController fileController = new DataFileController();
    private List<Sprite[]> sheets;
    void Start()
    {        
        LoadSpriteSheets();        
        LoadGrid(PlayerDataHelper.GetIdCurrentKingdom());
    }

    void LoadSpriteSheets()
    {
        sheets = new List<Sprite[]>();
        for (int x = 0; x < paths.Count; x++)
        {
            Sprite[] sheet = fileController.LoadSpriteSheet(paths[x]);
            sheets.Add(sheet);
        }
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
        DataHelper.UpdateIdCurrentKingdom(idKingdom);        
        OnLoadMap?.Invoke();
    }
    void InstantiateCell(GameObject[] listGO, CellModel cellData)
    {
        GameObject goCell = Instantiate(listGO[cellData.subtype.id], transform);
        SetData(goCell, cellData);
        SetBiome(goCell);
        SetFog(goCell, cellData);
        if (ActiveText)
            LogDev(goCell, cellData);
    }
    void SetFog(GameObject goCell, CellModel cellData)
    {
        if(!cellData.Fog)
        {
            GameObject[] children = ChildrenController.GetChildren(goCell);
            foreach(GameObject go in children)
            {
                ExploredMap exploredMap = go.GetComponent<ExploredMap>();
                if(exploredMap != null)
                {
                    go.SetActive(false);
                    return;
                }
            }
        }
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

        go.GetComponent<SpriteRenderer>().sprite = sheets[cellScript.cellData.biome.id][cellScript.cellData.subtype.id];
    }
    List<CellModel> GetDataCells()
    {
        List<CellModel> data = new List<CellModel>();
        GameObject[] children = ChildrenController.GetChildren(gameObject);
        foreach(GameObject child in children)
        {
            Cell cell = child.GetComponent<Cell>();
            if(cell != null)
            {
                data.Add(cell.cellData);
            }
        }
        return data;
    }
    public void UpdateDataKingdom()
    {
        List<CellModel> data = GetDataCells();
        fileController.SaveEncrypted<List<CellModel>>(data, PathHelper.WolrdDataFile(PlayerDataHelper.GetIdCurrentKingdom()));
    }
    private void OnDestroy()
    {
        UpdateDataKingdom();
    }
}
