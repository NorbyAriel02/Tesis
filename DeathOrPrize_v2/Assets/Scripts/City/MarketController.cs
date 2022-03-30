using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketController : MonoBehaviour
{
    public bool GenereItemRamdom;
    public bool IsClose;
    public GameObject prefabItemTemplate;
    public Button btnExit;
    public Text textCoins;
    public GameObject panelMarket;
    public GameObject panelMenu;
    public GameObject panelInventoryPlayer;
    public GameObject panelInventoryMarket;
    public GameObject[] SlotsInventoryPlayer;
    public GameObject[] SlotMarket;
    void Start()
    {
        btnExit.onClick.AddListener(Exit);        
        GetSlot();
        if (GenereItemRamdom)
            GenerarItemsMarket();

        IsClose = true;
        panelMarket.SetActive(false);        
    }
    private void OnEnable()
    {
        IsClose = false;
        LoadInventoryMarket();
    }
    void LoadInventoryMarket()
    {
        if (SlotMarket == null || SlotMarket.Length <= 0)
            GetSlot();

        InventoryHelper.ShowItems(SlotMarket, prefabItemTemplate, PathHelper.MarketDataFile);
    }
    void GenerarItemsMarket()
    {
        int count = SlotMarket.Length / 2;
        List<ItemProperties> items = new List<ItemProperties>();
        for(int x = 0; x < count; x++)
        {
            ItemProperties item = Utilitis.GetRandomItem(1);
            items.Add(item);
        }
        InventoryHelper.Save(items, PathHelper.MarketDataFile);
        GenereItemRamdom = false;
    }    
    void GetSlot()
    {
        SlotMarket = ChildrenController.GetChildren(panelInventoryMarket);
        for (int x = 0; x < SlotMarket.Length; x++)
        {
            SlotMarket[x].GetComponent<Slot>().ID = x;            
        }

        SlotsInventoryPlayer = ChildrenController.GetChildren(panelInventoryPlayer);        
    }
    #region button actions
    void Exit()
    {
        IsClose = true;
        panelMarket.SetActive(false);
        panelMenu.SetActive(true);
    }
    public void Sell(ItemProperties itemSell)
    {        
        int current = System.Convert.ToInt32(textCoins.text.Trim());
        textCoins.text = (current + itemSell.value).ToString();
        List<ItemProperties> items = InventoryHelper.GetListItemsFromPanel(SlotMarket);
        InventoryHelper.Save(items, PathHelper.MarketDataFile);
    }
    public bool Buy(ItemProperties itemBuy)
    {        
        int current = System.Convert.ToInt32(textCoins.text.Trim());
        if (current >= itemBuy.value)
        {
            textCoins.text = (current - itemBuy.value).ToString();
            List<ItemProperties> items = InventoryHelper.GetListItemsFromPanel(SlotsInventoryPlayer);
            InventoryHelper.Save(items, PathHelper.InventoryDataFile);
        }
        else
        {            
            YouDontHaveEnough();
            return false;
        }

        return true;

    }

    void YouDontHaveEnough()
    {
        //Agregar Feedback aca
        Debug.Log("Note alcanza");
    }
    #endregion
}
