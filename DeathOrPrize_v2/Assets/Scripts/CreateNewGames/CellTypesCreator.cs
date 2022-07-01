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
        
        
    }

    public void CreateCellTypes()
    {
        DataFileController fileController = new DataFileController();

        if (fileController.Exists(PathHelper.CellTypesDataFile) && !ForceCreation)
            return;

        fileController.SaveEncrypted<List<CellType>>(cellTypes, PathHelper.CellTypesDataFile);
        //Debug.Log("Listo tipos");
    }
}
