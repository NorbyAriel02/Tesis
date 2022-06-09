using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilitis 
{
    public static int PrecioBase = 15;
    public static int DamageBase = 10;
    public static int IncrementoDamage = 5;
    public static int DefendingBase = 50;
    public static int IncrementoDefending = 10;
    public static float probabilidadGolpeEnArea = 0.35f;
    public static ItemProperties GetRandomItem(int level, Owner owner)
    {
        ItemProperties item = new ItemProperties();        
        int type = Random.Range(1, 3);
        if (type == 1)
        {
            item = GetWeapon(level, owner);
        }            
        else
        {
            item = GetArmor(level, owner);
        }            
        
        return item;
    }
    
    static ItemProperties GetWeapon(int level, Owner owner)
    {
        float r = Random.Range(DamageBase, DamageBase+IncrementoDamage);
        ItemProperties item = new ItemProperties();
        item.owner = owner;
        item.tItem = TypeItemInventory.Weapon;
        item.level = level;
        item.value = PrecioBase * level;
        item.damageBase = level + r;
        item.attackSpeedEquipped = AttackSpeed(level);
        item.attackSpeedBase = AttackSpeed(level);
        item.area = IsHitArea();
        item.weight = 2;
        item.labelData = item.damageBase.ToString();

        return item;
    }

    static bool IsHitArea()
    {
        float r = Random.Range(0f, 1f);
        return r > probabilidadGolpeEnArea ? false:true;
    }
    static ItemProperties GetArmor(int level, Owner owner)
    {
        float r = Random.Range(DefendingBase, DefendingBase+IncrementoDefending);
        ItemProperties item = new ItemProperties();
        item.owner = owner;
        item.tItem = TypeItemInventory.Armor;
        item.level = level;
        item.value = PrecioBase + level;
        item.armor = level * r;
        item.weight = 3;
        item.labelData = item.armor.ToString();

        return item;
    }
    public static ItemProperties GetBestItem(ItemProperties i)
    {
        int r = Random.Range(1, 3);
        ItemProperties item = new ItemProperties();
        
        if (i.tItem == TypeItemInventory.Armor)
        {
            item = GetArmor(i.level + r, Owner.player);
        }
        else
        {
            item = GetWeapon(i.level + r, Owner.player);
        }

        return item;
    }

    public static float AttackSpeed(int level)
    {
        float b = 6;
        float balance = 0.5f;
        float attacks = (b - level * balance);

        if (attacks < 0.5f)
            attacks = 0.5f;

        float deltaTime = 0.5f;
        
        float attackSpeed = deltaTime / attacks;


        return attackSpeed;
    }
}
