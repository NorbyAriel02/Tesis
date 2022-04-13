using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTypesController : MonoBehaviour
{
    public BiomeCreator biome;
    public CellTypesCreator cellTypes;
    public CellSubTypesCreator celSubTypes;
    void Start()
    {
        biome.CreateBiomes();
        cellTypes.CreateCellTypes();
        celSubTypes.CreateCellTypes();
        SceneHelper.Load("WorldCreator");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
