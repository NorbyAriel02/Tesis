using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilitis 
{    
    public static ItemProperties GetRandomItem(int level)
    {
        ItemProperties item = new ItemProperties();
        int type = Random.Range(1, 3);
        if (type == 1)
        {
            item = GetWeapon(level);
        }            
        else
        {
            item = GetArmor(level);
        }            
        
        return item;
    }
    static ItemProperties GetWeapon(int level)
    {
        float r = Random.Range(10, 15);
        ItemProperties item = new ItemProperties();
        item.tItem = TypeItemInventory.Weapon;
        item.level = level;
        item.value = level;
        item.damageBase = level + r;
        item.attackSpeedBase = 5 - (5*((r+level)/100));
        item.area = level > 5 ? true : false;
        item.weight = 2;
        item.labelData = r.ToString();

        return item;
    }
    static ItemProperties GetArmor(int level)
    {
        float r = Random.Range(10, 15);
        ItemProperties item = new ItemProperties();
        item.tItem = TypeItemInventory.Armor;
        item.level = level;
        item.value = level;
        item.defending = level + r;
        item.weight = 3;
        item.labelData = r.ToString();

        return item;
    }
    public static ItemProperties GetBestItem(ItemProperties i)
    {
        float r = Random.Range(10, 15);
        ItemProperties item = new ItemProperties();
        item.tItem = i.tItem;
        item.level = i.level + 1;
        item.value = i.level + 1;
        if (i.tItem == TypeItemInventory.Armor)
        {
            item.defending = i.level + r;
            item.weight = 3;
            item.labelData = item.defending.ToString();
        }
        else
        {
            item.damageBase = i.damageBase + r;
            item.attackSpeedBase = 5 - (5 * ((r + i.level) / 100));
            item.area = item.level > 5 ? true : false;
            item.weight = 2;
            item.labelData = item.damageBase.ToString();
        }

        return item;
    }
}
