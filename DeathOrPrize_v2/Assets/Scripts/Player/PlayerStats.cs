using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float attackSpeed = 0.5f;    
    public float attackSpeedTimer = 0f;
    public PlayerStatsModel stats;    
    EquipmentManager equipment;
    InventoryManager inventory;//para obtener el peso que lleva el player  
    void Start()
    {
        stats = PlayerDataHelper.GetStats();
        attackSpeed = stats.attackSpeed;
        equipment = GetScript.Type<EquipmentManager>("Inventory");
        inventory = GetScript.Type<InventoryManager>("Inventory");
    }

    public void SetStats()
    {    
        for(int x = 0; x < equipment.items.Count; x++)
        {
            if (equipment.items[x].tItem == TypeItemInventory.Weapon)
            {
                stats.damage = equipment.items[x].damageBase;
                stats.attackSpeed = equipment.items[x].attackSpeedEquipped;
                attackSpeed = stats.attackSpeed;
                attackSpeedTimer = 0f;
            }
                

            if (equipment.items[x].tItem == TypeItemInventory.Armor)
                stats.defending = equipment.items[x].defending;
        }        
    }
}
