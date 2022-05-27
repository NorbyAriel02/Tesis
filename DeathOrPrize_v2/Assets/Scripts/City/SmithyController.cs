using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmithyController : MonoBehaviour
{
    public Button btnExit;
    public Button btnForjar;
    public GameObject panelSmithy;
    public GameObject panelMenu;
    public GameObject panelItems;
    public GameObject prefabItemTemplate;
    public GameObject[] Slots;
    void Start()
    {
        panelSmithy.SetActive(false);
    }
    private void OnEnable()
    {
        btnExit.onClick.AddListener(Exit);
        btnForjar.onClick.RemoveAllListeners();
        btnForjar.onClick.AddListener(Forjar);
        GetSlots();
    }
    void Exit()
    {
        panelSmithy.SetActive(false);
        panelMenu.SetActive(true);
    }
    void GetSlots()
    {
        Slots = ChildrenController.GetChildren(panelItems);
        for (int x = 0; x < Slots.Length; x++)
        {
            Slots[x].GetComponent<Slot>().ID = x;
            Slots[x].GetComponent<Slot>().empty = true;
        }
    }
    void Forjar()
    {
        
        List<ItemProperties> items = GetItems();
        if(ValidarItems(items))
        {
            AkSoundEngine.PostEvent("Item_Forge", this.gameObject);
            ItemProperties item = Utilitis.GetBestItem(items[0]);
            Slots[0].GetComponent<Slot>().empty = true;
            Slots[1].GetComponent<Slot>().empty = true;
            ChildrenController.RemoveAllChildren(Slots[0]);
            ChildrenController.RemoveAllChildren(Slots[1]);
            GameObject gItem = Instantiate(prefabItemTemplate, Slots[2].transform);
            Slots[2].GetComponent<Slot>().empty = false;            
            Item scriptItem = gItem.GetComponent<Item>();
            scriptItem.SetItem(item);
        }
        else
            AkSoundEngine.PostEvent("Field_Error", this.gameObject);
    }
    bool ValidarItems(List<ItemProperties> items)
    {
        if (items.Count != 2)
        {
            Debug.Log("Necesitas 2 items iguales para poder forjar uno superior");
            return false;
        }
        if (items[0].level != items[1].level)
        {
            Debug.Log("Necesitas 2 items iguales para poder forjar uno superior");
            return false;
        }
        return true;
    }
    List<ItemProperties> GetItems()
    {
        List<ItemProperties> items = new List<ItemProperties>();
        foreach (GameObject slot in Slots)
        {
            if (!slot.GetComponent<Slot>().empty)
            {
                GameObject item = ChildrenController.GetChild(slot);
                ItemProperties data = item.GetComponent<Item>().properties;
                items.Add(data);
            }
        }

        return items;
    }
}
