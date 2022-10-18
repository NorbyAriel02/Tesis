using System;
[Serializable]

public class ItemCape : ItemArmor
{
    public ItemCape()
    {
        this.IsStackable = false;
        this.sprite = "cape";
        this.level = 0;
    }
}
