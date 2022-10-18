using System;
[Serializable]

public class ItemGloves : ItemArmor
{
    public ItemGloves()
    {
        this.IsStackable = false;
        this.sprite = "gloves";
        this.level = 0;
    }
}
