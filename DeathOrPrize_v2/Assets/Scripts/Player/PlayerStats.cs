using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float attackSpeed = 0.5f;
    public float attackSpeedTimer = 0f;
    public PlayerStatsModel stats;
    private void OnEnable()
    {
        DragAndDrop.OnMoveItem += UpdateEquip;
        LevelController.OnLevelChange += LevelChange;
    }
    private void OnDisable()
    {
        DragAndDrop.OnMoveItem -= UpdateEquip;
        LevelController.OnLevelChange -= LevelChange;
    }
    private void Start()
    {
        UpdateEquip();
    }
    public void LevelChange()
    {
        PlayerStatsModel _stats = DataHelper.GetStats();
        this.stats.currentHealth = _stats.maxHealth;
        this.stats.maxHealth = _stats.maxHealth;
        this.stats.experience = _stats.experience;
        this.stats.level = _stats.level;
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
    public void UpdateEquip()
    {
        ResetEquip();
        DataHelper.UpdateEquipment(stats.equipment);
        List<ItemProperties> equip = DataHelper.GetListEquip();
        foreach (ItemProperties i in equip)
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
