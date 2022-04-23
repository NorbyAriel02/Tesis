using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeighboringKingdoms : MonoBehaviour
{
    private WorldCreator WorldCreator;
    private float kingdoms;
    DataFileController fileController = new DataFileController();
    void Start()
    {
        WorldCreator = GetComponent<WorldCreator>();
        kingdoms = WorldCreator.numberOfKingdom;
        CreateNeighborFileData();
    }
    int[,] matrizIds = null;
    void CreateNeighborFileData()
    {
        int[] xy = GetDimensionsMatrix();

        matrizIds = GenerarMatriz(xy[0], xy[1]);

        List<KingdomModel> listNeighborKingdom = GetListNeighborKingdom(xy[0], xy[1]);        

        fileController.SaveEncrypted<List<KingdomModel>>(listNeighborKingdom, PathHelper.NeighborKingdomDataFile);
    }
    List<KingdomModel> GetListNeighborKingdom(int a, int b)
    {
        List<KingdomModel> listNeighborKingdom = new List<KingdomModel>();

        for (int x = 0; x < a; x++)
        {
            for (int y = 0; y < b; y++)
            {
                KingdomModel km = new KingdomModel();
                km.IDkingdom = matrizIds[x, y];
                if (km.IDkingdom == 0)
                    break;
                km.row = x;
                km.col = y;
                km.NorthernNeighbor = GetNeighborNorth(x, y, a, b);
                km.SouthNeighbor = GetNeighborSouth(x, y, a, b);
                km.EastNeighbor = GetNeighborEast(x, y, a, b);
                km.WestNeighbor = GetNeighborWest(x, y, a, b);
                listNeighborKingdom.Add(km);
            }
        }
        return listNeighborKingdom;
    }
    int[] GetDimensionsMatrix()
    {
        float ladoF = Mathf.Pow(kingdoms, 0.5f);
        float row = Mathf.Floor(ladoF);
        float col = Mathf.Ceil(ladoF);

        if ((row * col) < kingdoms)
            row = col;
        
        int a = System.Convert.ToInt32(row);
        int b = System.Convert.ToInt32(col);

        return new int[] { a, b };
    }
    int[,] GenerarMatriz(int a, int b)
    {
        int id = 1;
        int[,] m = new int[a, b];
        for (int x = 0; x < a; x++)
        {
            for (int y = 0; y < b; y++)
            {
                if (id > kingdoms)
                    break;

                m[x, y] = id;
                id++;
            }
        }
        return m;
    }
    int GetNeighborSouth(int x, int y, int a, int b)
    {
        int yn = y - 1;

        if (y == 0)
            yn = (b - 1);

        int id = matrizIds[x, yn];

        while (id == 0)
        {
            yn--;
            id = matrizIds[x, yn];
        }

        return id;
    }
    int GetNeighborNorth(int x, int y, int a, int b)
    {
        int yn = y + 1;

        if (y == (b - 1))
            yn = 0;

        int id = matrizIds[x, yn];

        if (id == 0)
        {
            yn = 0;
            id = matrizIds[x, yn];
        }

        return id;
    }
    int GetNeighborWest(int x, int y, int a, int b)
    {
        int xn = x - 1;

        if (x == 0)
            xn = (a - 1);

        int id = matrizIds[xn, y];

        while (id == 0)
        {
            xn--;
            id = matrizIds[xn, y];
        }

        return id;
    }
    int GetNeighborEast(int x, int y, int a, int b)
    {
        int xn = x + 1;

        if (x == (a - 1))
            xn = 0;

        int id = matrizIds[xn, y];

        if (id == 0)
        {
            xn = 0;
            id = matrizIds[xn, y];
        }

        return id;
    }
}
