using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public KeyCode keyInventory;
    public Button btnClose;
    public GameObject inventory;
    public Animator animator;
    public GameObject prefabItemTemplate;
    public GameObject inventoryPanel;
    public GameObject[] Slots;
    public EquipmentManager equipment;
    public List<ItemProperties> items;
    private void OnEnable()
    {
        Drop.OnPickupItem += PickUp;
    }
    private void OnDisable()
    {
        Drop.OnPickupItem -= PickUp;
    }
    void Start()
    {        
        GetSlots();
        items = InventoryHelper.GetListItemsFromFile(PathHelper.InventoryDataFile);
        inventory.SetActive(false);
        btnClose.onClick.AddListener(CloseInventory);
    }
    void GetSlots()
    {
        Slots = ChildrenController.GetChildren(inventoryPanel); 
        for(int x = 0; x < Slots.Length; x++)
        {
            Slots[x].GetComponent<Slot>().ID = x;
            Slots[x].GetComponent<Slot>().empty = true;
        }
    }    
    void Update()
    {
        if (Input.GetKeyDown(keyInventory) && !inventory.activeSelf)
            OpenInventory();
        else if (Input.GetKeyDown(keyInventory) && inventory.activeSelf)
            CloseInventory();
    }
    public void OpenOrClose()
    {
        if (inventory.activeSelf)
            CloseInventory();
        else
            OpenInventory();
    }
    public void CloseInventory()
    {
        AkSoundEngine.PostEvent("UI_Exit", this.gameObject);
        List<ItemProperties> items = InventoryHelper.GetListItemsFromPanel(Slots);
        InventoryHelper.Save(items, PathHelper.InventoryDataFile);        
        equipment.Save();
        animator.SetBool("Close", true);
    }
    public void OpenInventory()
    {
        AkSoundEngine.PostEvent("UI_Click", this.gameObject);
        inventory.SetActive(true);
        animator.SetBool("Close", false);
        InventoryHelper.ShowItems(Slots, prefabItemTemplate, PathHelper.InventoryDataFile);        
        equipment.ShowEquipment();
    }    
    private void PickUp(GameObject goItem)
    {
        ItemProperties item = GetScript.Type<Drop>(goItem).item;
        if (AddItem(item))
        {
            AkSoundEngine.PostEvent("UI_Click", this.gameObject);
            Destroy(goItem);
        }
        else
            CantPickUp();
    }
    public bool AddItem(ItemProperties newItem)
    {        
        return InventoryHelper.AddItem(newItem, Slots, PathHelper.InventoryDataFile);
    }
    public void CantPickUp()
    {
        AkSoundEngine.PostEvent("Field_Error", this.gameObject);
    }
}
