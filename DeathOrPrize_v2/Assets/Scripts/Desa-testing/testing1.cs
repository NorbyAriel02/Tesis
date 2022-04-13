using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testing1 : MonoBehaviour
{
    public Button btnTest;
    public Text mensaje;
    public float kingdoms;
    public GameObject cube;
    public Transform padre;
    // Start is called before the first frame update
    void Start()
    {
        btnTest.onClick.AddListener(test);
    }
    int[,] matrizIds;
    void test()
    {
        ChildrenController.RemoveAllChildren(padre.gameObject);
        float ladoF = Mathf.Pow(kingdoms, 0.5f);
        float abajo = Mathf.Floor(ladoF);
        float arriba = Mathf.Ceil(ladoF);
        
        mensaje.text = ("Matriz de " + abajo + " X " + arriba);
        int a = System.Convert.ToInt32(abajo);
        int b = System.Convert.ToInt32(arriba);

        matrizIds = GenerarMatriz(a, b);
        List<KingdomModel> listNeighborKingdom = new List<KingdomModel>();

        for (int x = 0; x < a; x++)
        {
            for (int y = 0; y < b; y++)
            {
                KingdomModel km = new KingdomModel();
                km.IDkingdom = matrizIds[x, y];
                km.row = x;
                km.col = y;
                km.NorthernNeighbor = GetNeighborNorth(x, y, a, b);
                km.SouthNeighbor = GetNeighborSouth(x, y, a, b);
                km.EastNeighbor = GetNeighborEast(x, y, a, b);
                km.WestNeighbor = GetNeighborWest(x, y, a, b);
                listNeighborKingdom.Add(km);
            }
        }

        foreach(KingdomModel k in listNeighborKingdom)
        {
            InstantiateDefault(k);
        }
    }

    int[,] GenerarMatriz(int a, int b)
    {
        int id = 1;
        int[,] m = new int[a, b];
        for (int x = 0; x < a; x++)
        {
            for (int y = 0; y < b; y++)
            {
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

        if (id == 0)
            Debug.Log("No hay id");

        return id;
    }
    int GetNeighborNorth(int x, int y, int a, int b)
    {
        int yn = y + 1;

        if (y == (b - 1))
            yn = 0;
        
        int id = matrizIds[x, yn];

        if (id == 0)
            Debug.Log("No hay id");

        return id;
    }
    int GetNeighborWest(int x, int y, int a, int b)
    {
        int xn = x - 1;

        if (x == 0)
            xn = (a - 1);

        int id = matrizIds[xn, y];

        if (id == 0)
            Debug.Log("No hay id xn" + xn);

        return id;
    }
    int GetNeighborEast(int x, int y, int a, int b)
    {
        int xn = x + 1;

        if (x == (a - 1))
            xn = 0;

        int id = matrizIds[xn, y];

        if (id == 0)
            Debug.Log("No hay id xn" + xn);

        return id;
    }

    void InstantiateDefault(KingdomModel kData)
    {
        GameObject goCell = Instantiate(cube, padre);
        goCell.GetComponentInChildren<TextMesh>().text = kData.IDkingdom + System.Environment.NewLine + kData.NorthernNeighbor+kData.SouthNeighbor+ kData.EastNeighbor + kData.WestNeighbor;
        goCell.transform.SetPositionAndRotation(new Vector3(kData.row, kData.col, 0), Quaternion.identity);
    }
}
