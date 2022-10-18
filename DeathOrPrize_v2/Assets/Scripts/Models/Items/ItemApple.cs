using System;
[Serializable]

public class ItemApple : ItemModel
{
    public ItemApple()
    {
        this.IsStackable = true;
        this.sprite = "fruits";
        this.level = 0;
    }
}
