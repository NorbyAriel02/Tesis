using System;
[Serializable]

public class ItemProperties 
{
    /*Esta clase es variable, dependiendo del proyecto tendra mas o menos propiedades
     en teoria creo que solo propiedades, ahora no veo razo para qie tenga algun metodo*/
    public TypeItemInventory tItem;

    public int IndexSlot = -1;
    /*Requisitos del player para poder utilizar el item*/
    public float strength;
    public float dexterity;
    public float agility;
    public float resistance;
    public float constitution;

    /*propiedades*/
    public int SubType = 0;
    public int level;
    public bool area;
    public string labelData;
    public float EnergyPerAttacksEquipped;
    public float EnergyPerAttacksBase;
    public float timeBetweenAttacksEquipped;
    public float timeBetweenAttacksBase;
    public float attackSpeedEquipped;
    public float attackSpeedBase;
    public float weight;
    public float critical;
    public float damageAreaEquipped;
    public float damageAreaBase;
    public float damageAreaMax;
    public float damageEquipped;
    public float damageBase;
    public float damageMax;
    public float defending;

    /*Plus values*/
    public float plusEnergy;
    public float plusHealth;
    public float regenereHelth;
    public float regenereEnergy;
}
public enum TypeItemInventory { Item, Weapon, Armor }

//void AssignData()
//{
//    //itemData = new ItemData();
//    //itemData.strength = strength;
//    //itemData.dexterity = dexterity;
//    //itemData.agility = agility;
//    //itemData.resistance = resistance;
//    //itemData.constitution = constitution;
//    //itemData.type = type;
//    //itemData.SubType = SubType;
//    //itemData.level = level;
//    //itemData.area = area;
//    //itemData.labelData = labelData;
//    //itemData.EnergyPerAttacksEquipped = EnergyPerAttacksEquipped;
//    //itemData.EnergyPerAttacksBase = EnergyPerAttacksBase;
//    //itemData.timeBetweenAttacksEquipped = timeBetweenAttacksEquipped;
//    //itemData.timeBetweenAttacksBase = timeBetweenAttacksBase;
//    //itemData.attackSpeedEquipped = attackSpeedEquipped;
//    //itemData.attackSpeedBase = attackSpeedBase;
//    //itemData.weight = weight;
//    //itemData.critical = critical;
//    //itemData.damageAreaEquipped = damageAreaEquipped;
//    //itemData.damageAreaBase = damageAreaBase;
//    //itemData.damageAreaMax = damageAreaMax;
//    //itemData.damageEquipped = damageEquipped;
//    //itemData.damageBase = damageBase;
//    //itemData.damageMax = damageMax;
//    //itemData.defending = defending;
//    //itemData.plusEnergy = plusEnergy;
//    //itemData.plusHealth = plusHealth;
//    //itemData.regenereHelth = regenereHelth;
//    //itemData.regenereEnergy = regenereEnergy;
//}