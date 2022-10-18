using System;
[Serializable]
public class ItemModel 
{
    public Owner owner;
    public string name = "none";
    public string sprite = "default";
    public int IndexSlot = -1;    
    public bool IsStackable = false;
    public int Stack = 1;
    public string DataFile = "default";
    public int level = 1;
    public int value;
    public float weight;
}
