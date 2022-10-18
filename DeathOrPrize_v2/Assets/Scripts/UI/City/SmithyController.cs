using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmithyController : MonoBehaviour
{    
    public delegate void EventForja();
    public static EventForja OnForja;
    public string DataFile = "smithy";
    public int cost = 10;
    public Button btnExit;
    public Button btnForjar;
    public GameObject dialogue;
    public GameObject panelSmithy;    
    public GameObject panelItems;
    public GameObject prefabItemTemplate;
    public GameObject[] Slots;
    private float delay = 3;
    private float timer = 3;
    void Start()
    {
        //panelSmithy.SetActive(false);
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
    }
    void GetSlots()
    {
        Slots = ChildrenController.GetChildren(panelItems);
        for (int x = 0; x < Slots.Length; x++)
        {
            if (Slots[x].GetComponent<BaseSlot>() == null)
                continue;

            Slots[x].GetComponent<BaseSlot>().ID = x;
            Slots[x].GetComponent<BaseSlot>().empty = true;
        }
    }
    void Forjar()
    {        
        List<ItemModel> items = GetItems();        

        if(ValidarItems(items))
        {            
            //AkSoundEngine.PostEvent("Item_Forge", this.gameObject);
            int coints = PlayerDataHelper.GetCountCoins();
            int totalCost = (items[0].level * cost);
            PlayerDataHelper.UpdateCoins(coints-totalCost);
            ItemModel item = Utilitis.GetBestItem(items[0], DataFile);
            Slots[0].GetComponent<BaseSlot>().empty = true;
            Slots[1].GetComponent<BaseSlot>().empty = true;
            ChildrenController.RemoveAllChildren(Slots[0]);
            ChildrenController.RemoveAllChildren(Slots[1]);
            GameObject gItem = Instantiate(prefabItemTemplate, Slots[2].transform);
            Slots[2].GetComponent<BaseSlot>().empty = false;            
            Item scriptItem = gItem.GetComponent<Item>();
            scriptItem.SetItem(item);
            OnForja?.Invoke();
        }
        //else
        //    AkSoundEngine.PostEvent("Field_Error", this.gameObject);
    }
    bool ValidarItems(List<ItemModel> items)
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
        int coints = PlayerDataHelper.GetCountCoins();
        int _cost = (items[0].level * cost);
        if (_cost > coints)
        {            
            timer = delay;
            dialogue.SetActive(true);
            return false;
        }
        return true;
    }
    List<ItemModel> GetItems()
    {
        List<ItemModel> items = new List<ItemModel>();
        foreach (GameObject slot in Slots)
        {
            if (slot.GetComponent<BaseSlot>() == null)
                continue;

            if (!slot.GetComponent<BaseSlot>().empty)
            {
                GameObject item = ChildrenController.GetChild(slot);
                ItemModel data = item.GetComponent<Item>().item;
                items.Add(data);
            }
        }
        return items;
    }
    private void Update()
    {
        if (timer < 0)
            dialogue.SetActive(false);

        timer -= Time.deltaTime;
    }
}
