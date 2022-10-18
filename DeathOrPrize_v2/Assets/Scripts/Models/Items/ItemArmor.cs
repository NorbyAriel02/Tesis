using System;
[Serializable]
public class ItemArmor : ItemModel
{
    public ItemArmor()
    {
        this.IsStackable = false;
        this.sprite = "armor";
        this.level = 0;
    }
    public float armor;
}
