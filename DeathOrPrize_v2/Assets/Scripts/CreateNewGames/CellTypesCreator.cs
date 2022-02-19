using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellTypesCreator : MonoBehaviour
{
    public List<CellType> cellTypes;
    public bool ForceCreation;
    // Start is called before the first frame update
    void Start()
    {
        CreateCellTypes();
        Debug.Log("Listo tipos");
    }

    void CreateCellTypes()
    {
        DataFileController fileController = new DataFileController();

        if (fileController.Exists(PathHelper.CellTypesDataFile) && !ForceCreation)
            return;

        fileController.Save<List<CellType>>(cellTypes, PathHelper.CellTypesDataFile);
    }
}