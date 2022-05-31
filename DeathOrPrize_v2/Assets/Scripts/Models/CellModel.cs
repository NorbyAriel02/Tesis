using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CellModel 
{
    public int IDkingdom;
    public int sizeKingdom;
    public int index;
    public CellType type;
    public SubCellType subtype;
    public BiomeType biome;
    public float x;
    public float y;
    public bool Fog = true;
}
