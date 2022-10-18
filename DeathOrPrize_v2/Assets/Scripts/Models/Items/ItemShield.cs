using System;
[Serializable]
public class ItemShield : ItemArmor
{
    public ItemShield()
    {
        this.IsStackable = false;
        this.sprite = "shield";
        this.level = 0;
    }
}
