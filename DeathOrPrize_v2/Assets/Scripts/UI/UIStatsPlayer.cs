using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStatsPlayer : MonoBehaviour
{
    public Text[] txtDamage;
    public Text[] txtArmor;
    public Text[] txtAttackSpeed;
    public Text[] txtDamageArea;
    private void OnEnable()
    {
        DragAndDrop.OnMoveItem += ChangeStats;
    }
    private void OnDisable()
    {
        DragAndDrop.OnMoveItem -= ChangeStats;
    }
    private void Awake()
    {
        ChangeStats();
    }
    public void SetTextWeapon(ItemProperties item)
    {
        if(item != null)
        {
            UpdateTexts(txtDamage, "Damage " + item.damageBase);
            UpdateTexts(txtAttackSpeed, "Attack Speed " + item.attackSpeedEquipped.ToString("0.00"));
        }
        else
        {
            UpdateTexts(txtDamage, "Damage " + 0);
            UpdateTexts(txtAttackSpeed, "Attack Speed " + 0);
        }        
    }
    public void SetTextArmor(ItemProperties item)
    {
        if (item != null)
            UpdateTexts(txtArmor, "Armor " + item.armor);
        else
            UpdateTexts(txtArmor, "Armor " + 0);
    }
    void UpdateTexts(Text[] texts, string textValue)
    {        
        foreach (Text t in texts)
            t.text = textValue;
    }
    void ChangeStats()
    {
        SetTextArmor(null);
        SetTextWeapon(null);
        List<ItemProperties> equip = DataHelper.GetListEquip();
        foreach (ItemProperties i in equip)
        {
            if (i.tItem == TypeItemInventory.Armor)
                SetTextArmor(i);

            if (i.tItem == TypeItemInventory.Weapon)
                SetTextWeapon(i);
        }
    }
}
