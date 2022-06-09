using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStatsPlayer : MonoBehaviour
{
    public Text txtDamage;
    public Text txtArmor;
    public Text txtAttackSpeed;
    public Text txtDamageArea;
    private void OnEnable()
    {
        Slot.OnEquipedArmor += SetTextArmor;
        Slot.OnEquipedWeapon += SetTextWeapon;
    }
    private void OnDisable()
    {
        Slot.OnEquipedArmor -= SetTextArmor;
        Slot.OnEquipedWeapon -= SetTextWeapon;
    }
    public void SetTextWeapon(ItemProperties item)
    {
        txtDamage.text = "Damage " + item.damageBase;
        txtAttackSpeed.text = "Attack Speed " + item.attackSpeedBase;
    }
    public void SetTextArmor(ItemProperties item)
    {
        txtArmor.text = "Armor " + item.armor;
    }
}
