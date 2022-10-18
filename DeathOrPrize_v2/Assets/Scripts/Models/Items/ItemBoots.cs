using System;
[Serializable]

public class ItemBoots : ItemArmor
{
    public ItemBoots()
    {
        this.IsStackable = false;
        this.sprite = "boots";
        this.level = 0;
    }
}
