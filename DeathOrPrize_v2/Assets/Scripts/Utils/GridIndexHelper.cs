
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