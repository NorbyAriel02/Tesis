using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float attackSpeedBase = 1;    
    public float attackSpeedTimer = 1;
    public PlayerStatsModel stats;
    EquipmentManager equipment;
    InventoryManager inventory;
    DataFileController fileController = new DataFileController();
    void Start()
    {
        stats = fileController.GetData<PlayerStatsModel>(PathHelper.PlayerStatsDataFile);
        if (stats == null)
        {
            stats = new PlayerStatsModel();
            stats.attackSpeed = attackSpeedBase;
            stats.damage = 0;
            stats.defending = 0;
            stats.health = 10;
        }
        attackSpeedTimer = stats.attackSpeed;
        equipment = GameObject.FindGameObjectWithTag("Inventory").GetComponent<EquipmentManager>();
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventoryManager>();
    }

    public void SetStats()
    {    
        for(int x = 0; x < equipment.items.Count; x++)
        {
            if (equipment.items[x].tItem == TypeItemInventory.Weapon)
                stats.damage = equipment.items[x].damageBase;

            if (equipment.items[x].tItem == TypeItemInventory.Armor)
                stats.defending = equipment.items[x].defending;
        }        
    }
    void Update()
    {
        
    }
}
