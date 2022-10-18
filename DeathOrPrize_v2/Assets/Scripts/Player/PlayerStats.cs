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
    public void EquipArmor(ItemArmor item)
    {
        stats.equipment.armor += item.armor;        
    }
    public void EquipWeapon(ItemWeapon item)
    {
        stats.equipment.damage = item.damage;
        stats.equipment.attackSpeed = item.attackSpeed;
        attackSpeed = stats.equipment.attackSpeed;
        attackSpeedTimer = 0f;        
    }
    public void UpdateEquip()
    {
        ResetEquip();        
        List<ItemModel> equip = DataHelper.GetListEquip();
        for(int x = 0; x < equip.Count; x++)
        {            
            if (equip[x].GetType().GetField("armor") != null)
            {
                EquipArmor((ItemArmor)equip[x]);                
            }
            if (equip[x].GetType().GetField("damage") != null)
            {
                EquipWeapon((ItemWeapon)equip[x]);
            }
        }
        DataHelper.UpdateEquipment(stats.equipment);
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
