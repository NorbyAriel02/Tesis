using System;
public enum TypeItemInventory { Item, Weapon, Armor }
public enum Owner { player, seller }
[Serializable]

public class ItemProperties 
{
    public Owner owner;
    /*Esta clase es variable, dependiendo del proyecto tendra mas o menos propiedades
     en teoria creo que solo propiedades, ahora no veo razo para que tenga algun metodo*/
    public TypeItemInventory typeItem;
    public TypeSlot typeSlot;
    public string name = "none";
    public string sprite = "default";
    public int IndexSlot = -1;
    public string DataFile = "default";
    public bool IsStackable = false;
    public int Stack = 9999;

    /*Requisitos del player para poder utilizar el item*/
    public float strength;
    public float dexterity;
    public float agility;
    public float resistance;
    public float constitution;

    /*propiedades*/
    public int value = 1;
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
    public float armor;

    /*Plus values*/
    public float plusEnergy;
    public float plusHealth;
    public float regenereHelth;
    public float regenereEnergy;
}