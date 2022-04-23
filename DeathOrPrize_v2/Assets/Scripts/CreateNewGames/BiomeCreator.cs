using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeCreator : MonoBehaviour
{    
    public List<BiomeType> biomes;
    public bool ForceCreation;
    void Start()
    {
        
        
    }
    public void CreateBiomes()
    {
        DataFileController fileController = new DataFileController();
        
        if (fileController.Exists(PathHelper.BiomesDataFile) && !ForceCreation)
            return;

        fileController.SaveEncrypted<List<BiomeType>>(biomes, PathHelper.BiomesDataFile);
    }
}
