using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeCreator : MonoBehaviour
{    
    public List<BiomeType> biomes;
    public bool ForceCreation;
    void Start()
    {
        CreateBiomes();
        Debug.Log("Listo Biomas");
    }
    void CreateBiomes()
    {
        DataFileController fileController = new DataFileController();
        
        if (fileController.Exists(PathHelper.BiomesDataFile) && !ForceCreation)
            return;

        fileController.Save<List<BiomeType>>(biomes, PathHelper.BiomesDataFile);
    }
}
