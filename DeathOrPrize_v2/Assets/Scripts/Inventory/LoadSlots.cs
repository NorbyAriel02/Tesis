using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSlots : MonoBehaviour
{
    [Header("Panel que contiene los Slots del Inventario")]
    [Tooltip("Agrega el panel contenedor del inventario")]
    public GameObject panel;
    [Header("Prefeb del modelo item para inventario")]
    public GameObject[] prefabItemTemplate;
    [Header("El Archivo donde se guardan los item que mostrara es controller")]
    public string dataFile = "default";
    private GameObject[] Slots;

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
        if (Slots == null)
            Slots = ViewHelper.GetSlots(panel);

        ViewHelper.ShowItems(Slots, prefabItemTemplate, dataFile);
    }
}
