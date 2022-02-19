using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Trash : MonoBehaviour, IDropHandler
{
    public GameObject panel;
    public Button btnDelete;
    public Button btnCancel;
    private GameObject item;
    private Slot slot;
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
        slot = go.GetComponentInParent<Slot>();        
    }
    void SlotEmptyFalse()
    {
        slot = item.GetComponentInParent<Slot>();
        if (slot != null)
            slot.empty = false;

        slot = null;        
    }
    void SlotEmptyTrue()
    {
        slot = item.GetComponentInParent<Slot>();
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
        panel.SetActive(false);        
    }
    void Cancel()
    {
        SlotEmptyFalse();
        panel.SetActive(false);        
    }
    

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            item = eventData.pointerDrag.gameObject;
            panel.SetActive(true); 
        }
    }
}
