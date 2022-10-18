using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStatsPlayer : MonoBehaviour
{
    public Text[] txtLevel;
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
    public void SetTextWeapon(ItemWeapon item)
    {
        if (item != null)
        {
            UpdateTexts(txtDamage, "Damage " + item.damage);
            UpdateTexts(txtAttackSpeed, "Attack Speed " + item.attackSpeed.ToString("0.00"));
        }
        else
        {
            UpdateTexts(txtDamage, "Damage " + 0);
            UpdateTexts(txtAttackSpeed, "Attack Speed " + 0);
        }
    }
    public void SetTextArmor(float value)
    {
        UpdateTexts(txtArmor, "Armor " + value);
    }
    public void SetTextLevel(int level)
    {
        UpdateTexts(txtLevel, "Level " + level);
    }
    void UpdateTexts(Text[] texts, string textValue)
    {        
        foreach (Text t in texts)
            t.text = textValue;
    }
    void ChangeStats()
    {
        SetTextArmor(0);
        SetTextWeapon(null);
        SetTextLevel(DataHelper.GetStats().level);        
        List<ItemModel> equip = DataHelper.GetListEquip();
        float totalArmor = 0;
        foreach (ItemModel i in equip)
        {
            if(i.GetType().GetField("armor") != null)
            {
                ItemArmor item = (ItemArmor)i;
                totalArmor += item.armor;
            }                

            if (i.GetType() == typeof(ItemWeapon))
                SetTextWeapon((ItemWeapon)i);
        }
        SetTextArmor(totalArmor);
    }
}
