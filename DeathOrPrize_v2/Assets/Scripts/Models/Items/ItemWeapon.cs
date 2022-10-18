using System;
[Serializable]

public class ItemWeapon : ItemModel
{
    public ItemWeapon()
    {
        this.IsStackable = false;
        this.sprite = "weapon";
        this.level = 0;
    }
    public float attackSpeed;
    public float damageArea;
    public float damage;
}
