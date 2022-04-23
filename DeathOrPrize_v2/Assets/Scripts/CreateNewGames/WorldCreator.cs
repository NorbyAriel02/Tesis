using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldCreator : MonoBehaviour
{
    public int numberOfKingdom = 9;
    public int sizeKingdom = 55;
    List<CellType> CellTypes;
    Dictionary<int, List<SubCellType>> CellSubTypes;
    List<CellModel> cells;
    List<BiomeType> biomes;    
    DataFileController fileController = new DataFileController();
    
    void Start()
    {
        LoadBiomes();
        LoadCellTypes();
        LoadCellSubTypes();
        CreateWorld();
        SceneManager.LoadScene("Tutorial");
    }
    void LoadBiomes()
    {
        biomes = fileController.GetEncryptedData<List<BiomeType>>(PathHelper.BiomesDataFile);
    }
    void LoadCellTypes()
    {
        CellTypes = fileController.GetEncryptedData<List<CellType>>(PathHelper.CellTypesDataFile);
    }
    void LoadCellSubTypes()
    {
        CellSubTypes = fileController.GetEncryptedData<Dictionary<int, List<SubCellType>>>(PathHelper.CellSubTypesDataFile);
    }
    void CreateWorld()
    {
        for(int x = 1; x <= numberOfKingdom; x++)
        {
            CreateKingdom(x, GetBiome());
        }
    }
    void CreateKingdom(int idKingdom, int biome)
    {
        cells = GetDataKingdom(biome, idKingdom);
        string path = PathHelper.WolrdDataFile(idKingdom);
        
        fileController.SaveEncrypted<List<CellModel>>(cells, path);
    }
    List<CellModel> GetDataKingdom(int biome, int idKingdom)
    {
        cells = new List<CellModel>();
        CreateGrid(biome, idKingdom);
        MarkLimitCells();
        MarkCityCells();
        MarkRoadCells();
        MarkCave();
        return cells;
    }
    void CreateGrid(int biome, int idKingdom)
    {
        //string str = "";
        for (int x = 0; x < sizeKingdom; x++)
        {
            //str = str + "{";
            for (int y = 0; y < sizeKingdom; y++)
            {
                CellModel cell = new CellModel();
                cell.IDkingdom = idKingdom;
                cell.index = cells.Count;
                cell.x = x;
                cell.y = y;
                cell.biome = biomes[biome];
                cell.type = CellTypes[0];
                int i = Random.Range(0, CellSubTypes[0].Count);
                cell.subtype = CellSubTypes[0][i];
                cell.sizeKingdom = sizeKingdom;
                //str = str +  "[" + cell.index + "-" + cell.x + "-" + cell.y + "]|";
                cells.Add(cell);
            }
            //str = str + "}" + System.Environment.NewLine;
        }
        //fileController.Log(str, PathHelper.LogFile);
    }
    void MarkLimitCells()
    {
        for(int i = 0; i < cells.Count; i++)
        {
            float x = cells[i].x;
            float y = cells[i].y;
            
            if (y == (sizeKingdom-1))
            {
                cells[i].type = CellTypes[3];
                cells[i].subtype = CellSubTypes[CellTypes[3].id][0];
            }
            
            if (y == 0)
            {
                cells[i].type = CellTypes[3];
                cells[i].subtype = CellSubTypes[CellTypes[3].id][1];
            }

            if (x == (sizeKingdom - 1))
            {
                cells[i].type = CellTypes[3];
                cells[i].subtype = CellSubTypes[CellTypes[3].id][2];
            }

            if (x == 0)
            {
                cells[i].type = CellTypes[3];
                cells[i].subtype = CellSubTypes[CellTypes[3].id][3];
            }

            if (
                (x == 0 && y == 0) || 
                (x == (sizeKingdom - 1) && y == (sizeKingdom - 1)) || 
                (x == 0 && y == (sizeKingdom - 1)) || 
                (x == (sizeKingdom - 1) && y == 0)
               )
            {
                cells[i].type = CellTypes[5];
                cells[i].subtype = new SubCellType();
            }            
        }

    }

    #region city
    void MarkCityCells()
    {
        MarkCapitalCity();
        for(int i = 0; i < 4; i++)
        {
            MarkCity2Door(i);
        }
    }
    void MarkCapitalCity()
    {
        int mid = Mathf.RoundToInt(sizeKingdom / 2);
        
        int[] vIndex = GridIndexHelper.GetIndexes3x3(mid-2, mid+2, mid-2, mid+2, cells, sizeKingdom);
        for(int i = 0; i < vIndex.Length; i++)
        {
            cells[vIndex[i]].type = CellTypes[1];
            cells[vIndex[i]].subtype = CellSubTypes[CellTypes[1].id][i];
        }
        /*Definitivamente esto tiene que estar mal*/
        if (cells[vIndex[1]].IDkingdom == 1)
            PlayerDataHelper.SaveStartPositionPlayerKingdom1(new Vector3(cells[vIndex[1]].x, cells[vIndex[1]].y, 0));
    }
    void MarkCave()
    {
        int mid = Mathf.RoundToInt(sizeKingdom / 2);

        int[] vIndex = GridIndexHelper.GetIndexes3x3(8, 10, 8, 10, cells, sizeKingdom);
        for (int i = 0; i < vIndex.Length; i++)
        {
            cells[vIndex[i]].type = CellTypes[4];
            cells[vIndex[i]].subtype = CellSubTypes[CellTypes[4].id][i];
        }        
    }
    void MarkCity2Door(int index)
    {
        int mid = Mathf.RoundToInt(sizeKingdom / 2);
        int tercio = Mathf.RoundToInt(sizeKingdom / 3);
        int[] vIndex = null;
        if (index == 0)
        {
            vIndex = GridIndexHelper.GetIndexes3x3(5, tercio - 3, mid - 2, mid + 2, cells, sizeKingdom);            
        }

        if (index == 1)
        {
            vIndex = GridIndexHelper.GetIndexes3x3(mid - 2, mid + 2, (sizeKingdom - tercio), sizeKingdom - 5, cells, sizeKingdom);
        }
        
        if (index == 2)
        {
            vIndex = GridIndexHelper.GetIndexes3x3((sizeKingdom - tercio), sizeKingdom - 5, mid - 2, mid + 2, cells, sizeKingdom);            
        }

        if (index == 3)
        {
            vIndex = GridIndexHelper.GetIndexes3x3(mid - 2, mid + 2, 5, tercio - 3, cells, sizeKingdom);
        }

        for (int i = 0; i < vIndex.Length; i++)
        {
            cells[vIndex[i]].type = CellTypes[1];
            cells[vIndex[i]].subtype = CellSubTypes[CellTypes[1].id][i];
        }

        if (index == 1 || index == 3)
        {            
            cells[vIndex[3]].subtype = CellSubTypes[CellTypes[1].id][10]; 
            cells[vIndex[7]].subtype = CellSubTypes[CellTypes[1].id][12]; 
        }

        if (index == 0 || index == 2)
        {
            cells[vIndex[1]].subtype = CellSubTypes[CellTypes[1].id][9];
            cells[vIndex[5]].subtype = CellSubTypes[CellTypes[1].id][11];
        }
    }
    #endregion

    #region road
    void MarkRoadCells()
    {
        List<int> indexesCenter = GetIndexesCenterCitys();
        List<CityIndexs> Citys = GetIndexesCitys(indexesCenter);
        List<FacingDoors> listFacingDoors = GetFacingDoors(Citys);
        
        foreach (FacingDoors facingDoors in listFacingDoors)
        {
            GenerateRoad(facingDoors.indexDoorCity, facingDoors.indexDoorCapital);
        }        
    }
    void GenerateRoad(int iCity, int iCap)
    {        
        bool vertical = false;
        if (Mathf.Abs(cells[iCity].y - cells[iCap].y) > 6)
            vertical = false;
        else
            vertical = true;

        int loop = 0;
        
        while (cells[iCity].x != cells[iCap].x || cells[iCity].y != cells[iCap].y)
        {
            loop++;
            if (vertical)
            {
                iCity = MoveX(iCity, iCap);
                iCap = MoveX(iCap, iCity);                
            }
            else
            {
                iCity = MoveY(iCity, iCap);
                iCap = MoveY(iCap, iCity);                
            }
            MarkRoad(iCity);
            MarkRoad(iCap);
            if (vertical && loop > 2)
            {
                iCity = MoveY(iCity, iCap);
                iCap = MoveY(iCap, iCity);
            }
            else if (!vertical && loop > 2)
            {
                iCity = MoveX(iCity, iCap);
                iCap = MoveX(iCap, iCity);
            }
            MarkRoad(iCity);
            MarkRoad(iCap);
        }
    }
    int MoveX(int MoveX, int xComparate)
    {
        if (cells[MoveX].x < cells[xComparate].x)
        {
            MoveX = GridIndexHelper.IndexDerecha(MoveX, sizeKingdom);
        }
        else if(cells[MoveX].x > cells[xComparate].x)
        {
            MoveX = GridIndexHelper.IndexIzquierda(MoveX, sizeKingdom);
        }

        return MoveX;
    }
    int MoveY(int moveY, int yComparate)
    {
        if (cells[moveY].y < cells[yComparate].y)
        {
            moveY = GridIndexHelper.IndexArriba(moveY);            
        }
        else if (cells[moveY].y > cells[yComparate].y)
        {
            moveY = GridIndexHelper.IndexAbajo(moveY);            
        }
        return moveY;
    }
    void MarkRoad(int index)
    {
        cells[index].type = CellTypes[2];
        cells[index].subtype = CellSubTypes[2][0];
    }
    List<int> GetIndexesCenterCitys()
    {
        List<int> indexes = new List<int>();        
        foreach (CellModel cell in cells)
        {
            if (cell.type.id == 1 && cell.subtype.id == 0)
                indexes.Add(cell.index);
        }

        return indexes;
    }
    List<CityIndexs> GetIndexesCitys(List<int> indexesCenter)
    {
        List<CityIndexs> Citys = new List<CityIndexs>();

        foreach(int index in indexesCenter)
        {
            CityIndexs city = new CityIndexs();
            city.indexCenter = index;
            int i = GridIndexHelper.IndexArriba(index);
            if (cells[i].type.id == 1 && cells[i].subtype.id == 1)
                city.indexDoorNorth = i;

            i = GridIndexHelper.IndexDerecha(index, sizeKingdom);
            if (cells[i].type.id == 1 && cells[i].subtype.id == 3)
                city.indexDoorEast = i;
            
            i = GridIndexHelper.IndexAbajo(index);
            if (cells[i].type.id == 1 && cells[i].subtype.id == 5)
                city.indexDoorSouth = i;

            i = GridIndexHelper.IndexIzquierda(index,sizeKingdom);
            if (cells[i].type.id == 1 && cells[i].subtype.id == 7)
                city.indexDoorWest = i;

            Citys.Add(city);
        }

        return Citys;
    }
    List<FacingDoors> GetFacingDoors(List<CityIndexs> citys)
    {
        List<FacingDoors> facingDoors = new List<FacingDoors>();
        int indexCityCap = -1;
        for(int i = 0; i < citys.Count; i++)
        {
            if(citys[i].indexDoorEast > 0 &&
                citys[i].indexDoorNorth > 0 &&
                citys[i].indexDoorSouth > 0 &&
                citys[i].indexDoorWest > 0)
            {
                indexCityCap = i;
                break;
            }
        }
        for (int i = 0; i < citys.Count; i++)
        {
            if (i == indexCityCap)
                continue;

            FacingDoors door = new FacingDoors(); 
            if(citys[i].indexDoorSouth == -1 && citys[i].indexDoorNorth == -1 && cells[citys[i].indexCenter].x < cells[citys[indexCityCap].indexCenter].x)
            {
                door.indexDoorCapital = citys[indexCityCap].indexDoorWest;
                door.indexDoorCity = citys[i].indexDoorEast;
                facingDoors.Add(door);
                continue;
            }
            if (citys[i].indexDoorSouth == -1 && citys[i].indexDoorNorth == -1 && cells[citys[i].indexCenter].x > cells[citys[indexCityCap].indexCenter].x)
            {
                door.indexDoorCapital = citys[indexCityCap].indexDoorEast;
                door.indexDoorCity = citys[i].indexDoorWest;
                facingDoors.Add(door);
                continue;
            }
            if (citys[i].indexDoorEast == -1 && citys[i].indexDoorWest == -1 && cells[citys[i].indexCenter].y < cells[citys[indexCityCap].indexCenter].y)
            {
                door.indexDoorCapital = citys[indexCityCap].indexDoorSouth;
                door.indexDoorCity = citys[i].indexDoorNorth;
                facingDoors.Add(door);
                continue;
            }
            if (citys[i].indexDoorEast == -1 && citys[i].indexDoorWest == -1 && cells[citys[i].indexCenter].y > cells[citys[indexCityCap].indexCenter].y)
            {
                door.indexDoorCapital = citys[indexCityCap].indexDoorNorth;
                door.indexDoorCity = citys[i].indexDoorSouth;
                facingDoors.Add(door);
                continue;
            }
        }

        return facingDoors;
    }
    #endregion

    #region biome
    int[] arrA = new int[10];
    int i = 0;
    int GetBiome()
    {
        int biome = Random.Range(0, biomes.Count);
        int loop = 0;
        while (arrA.Any(n => n == biome))
        {
            biome = Random.Range(0, biomes.Count);
            loop++;
            if (loop > 100)//si me quede sin biomas, repito uno random
            {
                return biome;
            }                
        }
        arrA[i] = biome;
        i++;
        return biome;
    }
    #endregion
}

class CityIndexs
{
    public int indexCenter = -1;
    public int indexDoorNorth = -1;
    public int indexDoorSouth = -1;
    public int indexDoorEast = -1;
    public int indexDoorWest = -1;
}

class FacingDoors
{
    public int indexDoorCapital;
    public int indexDoorCity;
}