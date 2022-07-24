using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerStats : MonoBehaviour
{
    public float attackSpeed = 0.5f;
    public float attackSpeedTimer = 0f;
    public PlayerStatsModel stats;
    private void OnEnable()
    {        
        Slot.OnEquipedArmor += EquipArmor;
        Slot.OnEquipedWeapon += EquipWeapon;
        Slot.OnMoveItem += UpdateStats;
    }
    private void OnDisable()
    {     
        Slot.OnEquipedArmor -= EquipArmor;
        Slot.OnEquipedWeapon -= EquipWeapon;
        Slot.OnMoveItem -= UpdateStats;
    }    
    private void Start()
    {
        UpdateStats(null);
    }
    public void EquipArmor(ItemProperties item)
    {
        stats.equipment.armor = item.armor;
        DataHelper.UpdateEquipment(stats.equipment);
    }
    public void EquipWeapon(ItemProperties item)
    {
        stats.equipment.damage = item.damageBase;
        stats.equipment.attackSpeed = item.attackSpeedEquipped;
        attackSpeed = stats.equipment.attackSpeed;
        attackSpeedTimer = 0f;
        DataHelper.UpdateEquipment(stats.equipment);
    }
    public void UpdateStats(ItemProperties itemProperties)
    {
        ResetEquip();
        DataHelper.UpdateEquipment(stats.equipment);
        List<ItemProperties> equip = DataHelper.GetListEquip();
        foreach(ItemProperties i in equip)
        {
            if (i.tItem == TypeItemInventory.Armor)
                EquipArmor(i);

            if (i.tItem == TypeItemInventory.Weapon)
                EquipWeapon(i);
        }
    }
    void ResetEquip()
    {
        stats.equipment.armor = 0;
        stats.equipment.damage = 1;
        stats.equipment.attackSpeed = 0.01f;
        attackSpeed = 0.01f;
        attackSpeedTimer = 0f;
    }
}
