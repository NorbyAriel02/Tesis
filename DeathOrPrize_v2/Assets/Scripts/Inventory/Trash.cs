using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Trash : MonoBehaviour, IDropHandler
{
    public GameObject panelMensaje;
    public Button btnDelete;
    public Button btnCancel;
    private GameObject item;
    private BaseSlot slot;
    InventoryManager inventory;
    
    private void OnEnable()
    {
        GetComponent<Image>().raycastTarget = true;
    }
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventoryManager>();
        btnDelete.onClick.AddListener(Delete);
        btnCancel.onClick.AddListener(Cancel);
    }
    void AssignScript(GameObject go)
    {
        slot = go.GetComponentInParent<BaseSlot>();        
    }
    void SlotEmptyFalse()
    {
        slot = item.GetComponentInParent<BaseSlot>();
        if (slot != null)
            slot.empty = false;

        slot = null;        
    }
    void SlotEmptyTrue()
    {
        slot = item.GetComponentInParent<BaseSlot>();
        if (slot != null)
            slot.empty = true;        

        slot = null;
    }
    void OnDisable()
    {
        GetComponent<Image>().raycastTarget = false;
    }
    void Delete()
    {
        SlotEmptyTrue();
        Destroy(item);
        panelMensaje.SetActive(false);        
    }
    void Cancel()
    {
        SlotEmptyFalse();
        panelMensaje.SetActive(false);        
    }
    

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            item = eventData.pointerDrag.gameObject;
            panelMensaje.SetActive(true); 
        }
    }
}
