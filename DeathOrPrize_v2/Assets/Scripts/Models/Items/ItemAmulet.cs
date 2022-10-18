using System;
[Serializable]
public class ItemAmulet : ItemModel
{
    public ItemAmulet()
    {
        this.IsStackable = false;
        this.sprite = "amulet";
        this.level = 0;
    }
}
