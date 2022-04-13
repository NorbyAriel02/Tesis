using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeighboringKingdoms : MonoBehaviour
{
    private WorldCreator WorldCreator;
    DataFileController fileController = new DataFileController();
    void Start()
    {
        WorldCreator = GetComponent<WorldCreator>();
        CreateNeighborFileData();
    }
    int[,] matrizIds = null;
    void CreateNeighborFileData()
    {
        float ladoF = Mathf.Pow(WorldCreator.numberOfKingdom, 0.5f);
        float abajo = Mathf.Floor(ladoF);
        int lado = System.Convert.ToInt32(ladoF);
        matrizIds = GenerarMatriz(lado);
        List<KingdomModel> listNeighborKingdom = new List<KingdomModel>();
        
        for(int x = 0; x < lado; x++)
        {
            for (int y = 0; y < lado; y++)
            {
                KingdomModel km = new KingdomModel();
                km.IDkingdom = matrizIds[x,y];
                km.row = x;
                km.col = y;
                km.NorthernNeighbor = GetNeighborNorth(x, y, lado);
                km.SouthNeighbor = GetNeighborSouth(x, y, lado);
                km.EastNeighbor = GetNeighborEast(x, y, lado);
                km.WestNeighbor = GetNeighborWest(x, y, lado);
                listNeighborKingdom.Add(km);
        
            }
        }

        fileController.Save<List<KingdomModel>>(listNeighborKingdom, PathHelper.NeighborKingdomDataFile);
    }
    int[,] GenerarMatriz(int lado)
    {
        int id = 1;
        int[,] matrizIds = new int[lado, lado];
        for (int x = 0; x < lado; x++)
        {
            for (int y = 0; y < lado; y++)
            {
                matrizIds[x, y] = id;
                id++;
            }
        }
        return matrizIds;
    }    
    int GetNeighborNorth(int x, int y, int lado)
    {
        int xn = x - 1;
        
        if (x == 0)
            xn = (lado - 1);

        return matrizIds[xn, y];
    }
    int GetNeighborSouth(int x, int y, int lado)
    {
        int xn = x + 1;

        if (x == (lado - 1))
            xn = 0;

        return matrizIds[xn, y];
    }
    int GetNeighborWest(int x, int y, int lado)
    {
        int yn = y - 1;

        if (y == 0)
            yn = (lado - 1);

        return matrizIds[x, yn];
    }
    int GetNeighborEast(int x, int y, int lado)
    {
        int yn = y + 1;

        if (y == (lado - 1))
            yn = 0;

        return matrizIds[x, yn];
    }
}
