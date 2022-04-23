using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellSubTypesCreator : MonoBehaviour
{
    //public List<List<SubCellType>> SubTypes;
    public List<SubCellType> cellCampo;
    public List<SubCellType> cellCity;
    public List<SubCellType> cellRoad;
    public List<SubCellType> cellLimit;
    public List<SubCellType> cellCave;
    Dictionary<int, List<SubCellType>> CellSubTypes;
    public bool ForceCreation;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void CreateCellTypes()
    {
        CellSubTypes = new Dictionary<int, List<SubCellType>>();
        CellSubTypes.Add(cellCampo[0].idType, cellCampo);
        CellSubTypes.Add(cellCity[0].idType, cellCity);
        CellSubTypes.Add(cellRoad[0].idType, cellRoad);
        CellSubTypes.Add(cellLimit[0].idType, cellLimit);
        CellSubTypes.Add(cellCave[0].idType, cellCave);

        DataFileController fileController = new DataFileController();

        if (fileController.Exists(PathHelper.CellSubTypesDataFile) && !ForceCreation)
            return;

        fileController.SaveEncrypted<Dictionary<int, List<SubCellType>>>(CellSubTypes, PathHelper.CellSubTypesDataFile);
    }
}
