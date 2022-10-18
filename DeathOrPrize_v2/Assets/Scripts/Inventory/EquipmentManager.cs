using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    [Header("Panel que contiene los Slots del Equipamiento")]
    [Tooltip("Agrega el panel contenedor del Equipamiento")]
    public GameObject viewEquipment;
    [Header("Prefeb del modelo item para inventario")]
    public GameObject[] prefabItemTemplate;
    private GameObject[] SlotsEquipment;
    private void OnEnable()
    {        
        InventoryUI.OnOpen += UpdateView;
        DragAndDrop.OnMoveItem += UpdateView;
        CityController.OnEnterCity += UpdateView;
    }
    private void OnDisable()
    {        
        InventoryUI.OnOpen -= UpdateView;
        DragAndDrop.OnMoveItem -= UpdateView;
        CityController.OnEnterCity -= UpdateView;
    }
    public void UpdateView()
    {        
        if (SlotsEquipment == null)
            SlotsEquipment = ViewHelper.GetSlots(viewEquipment);
     
        ViewHelper.ShowItemsEquip(SlotsEquipment, prefabItemTemplate);
    }
}
