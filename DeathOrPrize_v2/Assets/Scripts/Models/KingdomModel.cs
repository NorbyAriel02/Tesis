using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class KingdomModel 
{
    public int IDkingdom;    
    public BiomeType biome;
    public int NorthernNeighbor;
    public int SouthNeighbor;
    public int EastNeighbor;
    public int WestNeighbor;
    public int row;
    public int col;
}
