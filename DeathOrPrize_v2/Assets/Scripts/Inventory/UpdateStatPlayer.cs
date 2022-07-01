using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
//la finalidad de este scipt es que si el content esta actigo actualice
//la info del player con los objetos equipaso

public class UpdateStatPlayer : MonoBehaviour
{
    public delegate void ChangeStats(ItemProperties _armor, ItemProperties _weapon);
    public static ChangeStats OnChangeStats;

    public GameObject[] slots;
    public ItemProperties weapon;
    public ItemProperties armor;
    public PlayerStats stats;    
    void Awake()
    {
        slots = ChildrenController.GetChildren(gameObject);
        
        stats = GetScript.Type<PlayerStats>("Player");
    }
    private void OnEnable()
    {
        slots = ChildrenController.GetChildren(gameObject);
        DragAndDrop.OnChangeStats += UpdateStats;
        UpdateStats();
    }
    private void OnDisable()
    {
        DragAndDrop.OnChangeStats -= UpdateStats;
    }
    void UpdateStats()
    {
        LoadItem();
        stats.SetArmor(armor);
        stats.SetWeapon(weapon);
        OnChangeStats?.Invoke(armor, weapon);
    }

    void LoadItem()
    {
        weapon = new ItemProperties();
        armor = new ItemProperties();
        foreach (GameObject slot in slots)
        {
            GameObject goItem = ChildrenController.GetChild(slot);
            if (goItem != null)
            {
                Item pItem = goItem.GetComponent<Item>();
                if (pItem.properties.tItem == TypeItemInventory.Armor)
                    armor = pItem.properties;

                if (pItem.properties.tItem == TypeItemInventory.Weapon)
                    weapon = pItem.properties;

            }
        }
    }
}
