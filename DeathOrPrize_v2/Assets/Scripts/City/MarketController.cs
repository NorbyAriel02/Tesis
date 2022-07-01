using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketController : MonoBehaviour
{
    public delegate void EventSell();
    public static EventSell OnSell;
    public delegate void EventBuy();
    public static EventBuy OnBuy;

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
        GenerarItemsMarket();
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
        int level = PlayerDataHelper.GetIdCurrentKingdom();
        int count = SlotMarket.Length / 2;
        List<ItemProperties> items = new List<ItemProperties>();
        for(int x = 0; x < count; x++)
        {
            ItemProperties item = Utilitis.GetRandomItem(level, Owner.seller);
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
        List<ItemProperties> items = InventoryHelper.GetListItemsFromPanel(SlotMarket);
        InventoryHelper.Save(items, PathHelper.MarketDataFile);
        IsClose = true;
        panelMarket.SetActive(false);
        panelMenu.SetActive(true);
    }
    public void Sell(ItemProperties itemSell)
    {
        AkSoundEngine.PostEvent("Item_Sell", this.gameObject);
        itemSell.owner = Owner.seller;
        int current = System.Convert.ToInt32(textCoins.text.Trim());
        textCoins.text = (current + itemSell.value).ToString();
        PlayerDataHelper.UpdateCoins((current + itemSell.value));
        List<ItemProperties> items = InventoryHelper.GetListItemsFromPanel(SlotMarket);
        InventoryHelper.Save(items, PathHelper.MarketDataFile);
        OnSell?.Invoke();
    }
    public bool Buy(ItemProperties itemBuy)
    {        
        int current = System.Convert.ToInt32(textCoins.text.Trim());
        if (current >= itemBuy.value)
        {
            AkSoundEngine.PostEvent("Item_Sell", this.gameObject);            
            itemBuy.owner = Owner.player;
            textCoins.text = (current - itemBuy.value).ToString();
            PlayerDataHelper.UpdateCoins((current - itemBuy.value));
            List<ItemProperties> items = InventoryHelper.GetListItemsFromPanel(SlotsInventoryPlayer);
            InventoryHelper.Save(items, PathHelper.InventoryDataFile);
        }
        else
        {            
            YouDontHaveEnough();
            return false;
        }
        OnBuy?.Invoke();
        return true;
    }
    void YouDontHaveEnough()
    {
        AkSoundEngine.PostEvent("Field_Error", this.gameObject);
        Debug.Log("Note alcanza");
    }
    #endregion
}
