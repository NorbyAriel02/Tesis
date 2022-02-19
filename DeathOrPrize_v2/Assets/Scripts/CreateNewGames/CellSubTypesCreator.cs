using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellSubTypesCreator : MonoBehaviour
{
    public List<SubCellType> cellCampo;
    public List<SubCellType> cellCity;
    public List<SubCellType> cellRoad;
    public List<SubCellType> cellLimit;
    Dictionary<int, List<SubCellType>> CellSubTypes;
    public bool ForceCreation;
    // Start is called before the first frame update
    void Start()
    {
        CreateCellTypes();
        Debug.Log("Listo Sub tipos");
    }
    void CreateCellTypes()
    {
        CellSubTypes = new Dictionary<int, List<SubCellType>>();
        CellSubTypes.Add(cellCampo[0].idType, cellCampo);
        CellSubTypes.Add(cellCity[0].idType, cellCity);
        CellSubTypes.Add(cellRoad[0].idType, cellRoad);
        CellSubTypes.Add(cellLimit[0].idType, cellLimit);

        DataFileController fileController = new DataFileController();

        if (fileController.Exists(PathHelper.CellSubTypesDataFile) && !ForceCreation)
            return;

        fileController.Save<Dictionary<int, List<SubCellType>>>(CellSubTypes, PathHelper.CellSubTypesDataFile);
    }
}
