using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketController : MonoBehaviour
{
    public string DataFile = "market";
    public bool GenereItemRamdom;    
    public GameObject[] prefabItemTemplate;
    public GameObject panelInventoryMarket;    
    private GameObject[] SlotMarket;
    private void OnEnable()
    {
        CityController.OnOpenMarket += UpdateView;
        GenerarItemsMarket();
    }
    private void OnDisable()
    {
        CityController.OnOpenMarket -= UpdateView;        
    }
    void Start()
    {   
        
    }
    
    void UpdateView()
    {
        if (SlotMarket == null)
        {
            SlotMarket = ViewHelper.GetSlots(panelInventoryMarket);            
        }

         ViewHelper.ShowItemsMarket(SlotMarket, prefabItemTemplate);        
    }
    void GenerarItemsMarket()
    {
        if (SlotMarket == null || SlotMarket.Length == 0)
            SlotMarket = ViewHelper.GetSlots(panelInventoryMarket);

        int level = DataHelper.GetIdCurrentKingdom();
        int count = SlotMarket.Length / 2;
        List<ItemModel> items = new List<ItemModel>();
        for(int x = 0; x < count; x++)
        {
            ItemModel item = Utilitis.GetRandomItem(level, Owner.seller, DataFile);
            items.Add(item);
        }
        DataHelper.CreateMarket(items);
        GenereItemRamdom = false;
    }        
}
