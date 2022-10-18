using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Utilitis 
{
    public static string[] spritesName = { "amulet", "weapon", "shield", "armor", "cape", "gloves", "helmet", "boots", "ring", "fruits", "default" };
    public static float PesoBase = 0.5f;
    public static int PrecioBase = 15;
    public static int DamageBase = 10;
    public static int IncrementoDamage = 5;
    public static int ArmorBase = 50;
    public static int IncrementoDefending = 10;
    public static float probabilidadGolpeEnArea = 0.35f;
    public static T GetItem<T>(int level, Owner owner, string DataFile) where T : ItemModel, new()
    {
        T item = new T();        
        item.level = level;
        item.owner = owner;
        item.DataFile = DataFile;
        item.name = RandomString(15);
        item.value = level * PrecioBase;
        item.weight = PesoBase;
        return item;
    }    
    public static ItemAmulet GetAmulet(int level, Owner owner, string dataFile)
    {
        ItemAmulet amulet = GetItem<ItemAmulet>(level, owner, dataFile);
        amulet.sprite = spritesName[0];

        return amulet;
    }
    public static ItemRing GetRing(int level, Owner owner, string dataFile)
    {
        ItemRing ring = GetItem<ItemRing>(level, owner, dataFile);
        ring.sprite = spritesName[8];

        return ring;
    }
    public static ItemBoots GetBoots(int level, Owner owner, string dataFile)
    {
        ItemBoots boots = GetItem<ItemBoots>(level, owner, dataFile);
        boots.sprite = spritesName[7];
        boots.armor = ArmorBase * level;

        return boots;
    }
    public static ItemGloves GetGloves(int level, Owner owner, string dataFile)
    {
        ItemGloves gloves = GetItem<ItemGloves>(level, owner, dataFile);
        gloves.sprite = spritesName[5];
        gloves.armor = ArmorBase * level;

        return gloves;
    }
    public static ItemWeapon GetWeapon(int level, Owner owner, string DataFile)
    {
        ItemWeapon item = GetItem<ItemWeapon>(level, owner, DataFile);
        item.damage = level * 10;
        item.attackSpeed = AttackSpeed(level);
        item.sprite = spritesName[1];

        return item;
    }
    public static ItemShield GetShield(int level, Owner owner, string dataFile)
    {
        ItemShield shield = GetItem<ItemShield>(level, owner, dataFile);
        shield.sprite = spritesName[2];
        shield.armor = ArmorBase * level;

        return shield;
    }
    public static ItemHelmet GetHelmet(int level, Owner owner, string dataFile)
    {
        ItemHelmet shield = GetItem<ItemHelmet>(level, owner, dataFile);
        shield.sprite = spritesName[6];
        shield.armor = ArmorBase * level;

        return shield;
    }
    public static ItemCape GetCape(int level, Owner owner, string dataFile)
    {
        ItemCape shield = GetItem<ItemCape>(level, owner, dataFile);
        shield.sprite = spritesName[4];
        shield.armor = ArmorBase * level;

        return shield;
    }
    public static ItemApple GetApple(int level, Owner owner, string dataFile)
    {
        ItemApple apple = GetItem<ItemApple>(level, owner, dataFile);
        apple.sprite = spritesName[9];        

        return apple;
    }
    public static ItemArmor GetArmor(int level, Owner owner, string dataFile)
    {
        ItemArmor armor = GetItem<ItemArmor>(level, owner, dataFile);
        armor.sprite = spritesName[3];
        armor.armor = ArmorBase * level;
        return armor;
    }
    public static ItemModel GetRandomItem(int level, Owner owner, string DataFile)
    {
        int r = Random.Range(0, 10);
        switch(r)
        {
            case 0:
                return GetAmulet(level, owner, DataFile);
            case 1:
                return GetRing(level, owner, DataFile);
            case 2:
                return GetWeapon(level, owner, DataFile);
            case 3:
                return GetBoots(level, owner, DataFile);
            case 4:
                return GetGloves(level, owner, DataFile);
            case 5:
                return GetShield(level, owner, DataFile);
            case 6:
                return GetHelmet(level, owner, DataFile);
            case 7:
                return GetWeapon(level, owner, DataFile);
            case 8:
                return GetApple(level, owner, DataFile);
            case 9:
                return GetArmor(level, owner, DataFile);
        }
        ItemModel item = new ItemModel();
        item.level = level;
        item.owner = owner;
        item.name = GetNameItem();
        item.DataFile = DataFile;
        item.value = level * PrecioBase;
        item.IsStackable = Random.Range(0, 2) == 0 ? true : false;
        item.IsStackable = false;        

        return item;
    }
    public static ItemModel GetRandomItemStack(int level, Owner owner, string DataFile)
    {
        ItemModel item = new ItemModel();
        item.level = level;
        item.owner = owner;
        item.name = RandomString(15);
        item.DataFile = DataFile;
        item.value = level * PrecioBase;
        item.IsStackable = true;
        return item;
    }    
    public static T GetItemArmor<T>(int level, Owner owner, string DataFile) where T : ItemArmor, new()
    {
        T item = new T();
        item.level = level;
        item.owner = owner;
        item.DataFile = DataFile;
        item.name = GetNameItem();
        item.armor = level * ArmorBase;
        return item;
    }
    private static System.Random random = new System.Random();

    static string GetNameItem()
    {
        return System.DateTime.Now.ToString("yyyyMMddHHmmss") + RandomString(15); 
    }
    public static string RandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }
    static bool IsHitArea()
    {
        float r = Random.Range(0f, 1f);
        return r > probabilidadGolpeEnArea ? false:true;
    }
    static ItemModel GetArmor(int level, Owner owner)
    {
        float r = Random.Range(ArmorBase, ArmorBase+IncrementoDefending);
        ItemModel item = new ItemModel();
        item.owner = owner;
        //item.typeItem = TypeItemInventory.Armor;
        //item.typeSlot = TypeSlot.SlotArmor;
        //item.level = level;
        //item.value = PrecioBase + level;
        //item.armor = level * r;
        //item.weight = 3;
        //item.labelData = item.armor.ToString();

        return item;
    }
    public static ItemModel GetBestItem(ItemModel i, string dataFile)
    {
        int r = Random.Range(1, 3);
        ItemModel item = new ItemModel();
        switch(i.sprite)
        {
            case "weapon":
                return GetWeapon(i.level + r, i.owner, dataFile);
            case "boots":
                return GetBoots(i.level + r, i.owner, dataFile);
            case "helmet":
                return GetHelmet(i.level + r, i.owner, dataFile);
            case "gloves":
                return GetGloves(i.level + r, i.owner, dataFile);
            case "armor":
                return GetArmor(i.level + r, i.owner, dataFile);
            case "cape":
                return GetCape(i.level + r, i.owner, dataFile);
            case "shield":
                return GetShield(i.level + r, i.owner, dataFile);
        }        
        return item;
    }
    public static float AttackSpeed(int level)
    {
        //level max ----- 0.5f
        //level 1 ------ 10
        //level 2 ------ x
        float speedMax = 11;
        float variante = Random.Range(-0.5f,0.5f);
        //float b = 6;
        float balance = 10;
        float attacks = (speedMax - (level*((speedMax*balance)/100))) - variante;

        if (attacks < 0.5f)
            attacks = 0.5f;

        //float deltaTime = 0.5f;
        
        //float attackSpeed = deltaTime / attacks;

        return attacks;
    }
}
