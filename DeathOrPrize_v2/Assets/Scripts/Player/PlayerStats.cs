using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float attackSpeed = 0.5f;    
    public float attackSpeedTimer = 0f;
    public PlayerStatsModel stats;    
    //EquipmentManager equipment;
    InventoryManager inventory;//para obtener el peso que lleva el player
    LevelSystem levelSystem;
    void Awake()
    {
        stats = PlayerDataHelper.GetStats();
        attackSpeed = stats.attackSpeed;
        //equipment = GetScript.Type<EquipmentManager>("Inventory");
        inventory = GetScript.Type<InventoryManager>("Inventory");
                
    }
    private void OnEnable()
    {
        LevelController.StartLevelSystem += SetLevelSystem;
        Slot.OnEquipedArmor += SetArmor;
        Slot.OnEquipedWeapon += SetWeapon;
        CityCell.ClicOnDoorCity += Heal;
    }
    private void OnDisable()
    {
        LevelController.StartLevelSystem -= SetLevelSystem;
        Slot.OnEquipedArmor -= SetArmor;
        Slot.OnEquipedWeapon -= SetWeapon;
    }
    public void SetLevelSystem(LevelSystem levelSystem)
    {
        this.levelSystem = levelSystem;

        this.levelSystem.OnLevelChanged += LevelSystem_OnLevelChanged;
        this.levelSystem.OnExperienceChanged += LevelSystem_OnExperienceChanged;
    }

    private void LevelSystem_OnExperienceChanged(object sender, System.EventArgs e)
    {
        stats.experience = levelSystem.Experience;
        PlayerDataHelper.SaveStats(stats);
    }

    private void LevelSystem_OnLevelChanged(object sender, System.EventArgs e)
    {
        stats.level = levelSystem.LevelNumber;
        stats.maxHealth = stats.maxHealth + (levelSystem.LevelNumber * 5);
        PlayerDataHelper.SaveStats(stats);
    }
    public void SetArmor(ItemProperties item)
    {
        stats.armor = item.armor;
        PlayerDataHelper.SaveStats(stats);
    }
    public void SetWeapon(ItemProperties item)
    {
        stats.damage = item.damageBase;
        stats.attackSpeed = item.attackSpeedEquipped;
        attackSpeed = stats.attackSpeed;
        attackSpeedTimer = 0f;
        PlayerDataHelper.SaveStats(stats);
    }
    public void Heal(float x, float y, int subTypeId)
    {
        stats.currentHealth = stats.maxHealth;        
        PlayerDataHelper.SaveStats(stats);
    }
    //public void SetStats()
    //{    
    //    for(int x = 0; x < equipment.items.Count; x++)
    //    {
    //        if (equipment.items[x].tItem == TypeItemInventory.Weapon)
    //        {
    //            stats.damage = equipment.items[x].damageBase;
    //            stats.attackSpeed = equipment.items[x].attackSpeedEquipped;
    //            attackSpeed = stats.attackSpeed;
    //            attackSpeedTimer = 0f;
    //        }


    //        if (equipment.items[x].tItem == TypeItemInventory.Armor)
    //            stats.armor = equipment.items[x].armor;
    //    }        
    //}
}
