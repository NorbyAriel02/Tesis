using System;
[Serializable]

public class ItemRing : ItemModel
{
    public ItemRing()
    {
        this.IsStackable = false;
        this.sprite = "ring";
        this.level = 0;
    }
}
