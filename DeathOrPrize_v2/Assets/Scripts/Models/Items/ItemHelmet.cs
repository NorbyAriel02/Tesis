using System;
[Serializable]

public class ItemHelmet : ItemArmor
{
    public ItemHelmet()
    {
        this.IsStackable = false;
        this.sprite = "helmet";
        this.level = 0;
    }
}
