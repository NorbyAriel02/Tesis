using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UpdateStatPlayer : MonoBehaviour, IDropHandler
{
    public UIStatsPlayer ui;
    public PlayerStats stats;
    private Slot slot;
    void Awake()
    {
        slot = GetComponent<Slot>();
        stats = GetScript.Type<PlayerStats>("Player");
    }
    private void OnEnable()
    {
        //UpdateUI();
        //stats.SetStats(); 
    }
    void OnDisable()
    {
        //UpdateUI();
        //stats.SetStats();
    }
    public void OnDrop(PointerEventData eventData)
    {
        //UpdateUI();
        //stats.SetStats();
    }

    void UpdateUI()
    {
        //GameObject child = ChildrenController.GetChild(gameObject);
        //if (child == null)
        //{
        //    UpdateEmtyUI();
        //    return;
        //}
            
        //Item i = child.GetComponent<Item>();
        //if (i.properties.tItem == TypeItemInventory.Weapon)
        //{
        //    ui.SetTextWeapon(i.properties.damageBase.ToString(), i.properties.attackSpeedEquipped.ToString());
        //}
        //if (i.properties.tItem == TypeItemInventory.Armor)
        //{
        //    ui.SetTextArmor(i.properties.armor.ToString());
        //}
    }
    void UpdateEmtyUI()
    {
        //if (slot == null)
        //    return;

        //if (slot.tSlot == TypeSlot.SlotWeapon)
        //    ui.SetTextWeapon("0", "0");

        //if (slot.tSlot == TypeSlot.SlotArmor)
        //    ui.SetTextArmor("0");
    }
}
