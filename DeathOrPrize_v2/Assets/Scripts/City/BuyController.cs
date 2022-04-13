using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class BuyController : MonoBehaviour, IDropHandler
{
    public bool MarketOpen;
    private MarketController market;
    private Slot slot;

    void Start()
    {
        slot = GetComponent<Slot>();
        SetMarket();
    }
    void SetMarket()
    {
        GameObject goMarket = GameObject.FindGameObjectWithTag("Market");
        if(goMarket == null)
        {
            //Debug.Log("Script buy dice Game Object Market is null");
            return;
        }            
        
        market = goMarket.GetComponent<MarketController>();
        if (market == null)
            Debug.Log("Script buy dice Market script is null");
    }
    public void OnDrop(PointerEventData eventData)
    {

        if (market == null)
            SetMarket();

        if (market == null || market.IsClose)
            return;

        if (eventData.pointerDrag != null && slot.empty)
        {
            Item i = eventData.pointerDrag.GetComponent<Item>();
            if(i.properties.owner == Owner.seller)
                if(!market.Buy(i.properties))
                    slot.cancelar = true;
        }
    }
}
