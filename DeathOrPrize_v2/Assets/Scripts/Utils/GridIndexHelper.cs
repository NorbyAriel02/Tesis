using System.Collections;
using System.Collections.Generic;
public class GridIndexHelper 
{
    public static int GetIndex(int index, Direcciones dir, int sizeGrid)
    {
        switch(dir)
        {
            case Direcciones.derecha:
                return IndexDerecha(index, sizeGrid);
            case Direcciones.izquierda:
                return IndexIzquierda(index, sizeGrid);
            case Direcciones.abajo:
                return IndexAbajo(index);
            case Direcciones.arriba:
                return IndexArriba(index);
        }

        return -1;
    }
    public static int[] GetIndexes3x3(int xMin, int xMax, int yMin, int yMax, List<CellModel> cells, int sizeKingdom)
    {
        int index = -1;
        int x = UnityEngine.Random.Range(xMin, xMax);
        int y = UnityEngine.Random.Range(yMin, yMax);
        foreach (CellModel cell in cells)
            if (cell.x == x && cell.y == y)
            {
                index = cell.index;
                break;
            }


        int[] vIndex = new int[]
        {   index,
            GridIndexHelper.IndexArriba(index),
            GridIndexHelper.IndexDerArriba(index, sizeKingdom),
            GridIndexHelper.IndexDerecha(index,sizeKingdom),
            GridIndexHelper.IndexDerAbajo(index, sizeKingdom),
            GridIndexHelper.IndexAbajo(index),
            GridIndexHelper.IndexIzqAbajo(index, sizeKingdom),
            GridIndexHelper.IndexIzquierda(index,sizeKingdom),
            GridIndexHelper.IndexIzqArriba(index, sizeKingdom)
        };

        return vIndex;
    }
    public static int IndexIzquierda(int index, int SizeGrid)
    {
        return index - (SizeGrid);
    }
    public static int IndexDerecha(int index, int SizeGrid)
    {
        return index + (SizeGrid);
    }
    public static int IndexArriba(int index)
    {
        return index + 1;
    }
    public static int IndexAbajo(int index)
    {
        return index - 1;
    }
    public static int IndexIzqAbajo(int index, int SizeGrid)
    {
        return index - (SizeGrid + 1);
    }
    public static int IndexDerAbajo(int index, int SizeGrid)
    {
        return index + (SizeGrid - 1);
    }
    public static int IndexIzqArriba(int index, int SizeGrid)
    {
        return index - (SizeGrid - 1);
    }
    public static int IndexDerArriba(int index, int SizeGrid)
    {
        return index + (SizeGrid + 1);
    }
}
public enum Direcciones { arriba, abajo, derecha, izquierda }