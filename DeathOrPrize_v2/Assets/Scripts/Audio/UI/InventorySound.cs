using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySound : MonoBehaviour
{
    private void OnEnable()
    {
        MarketSlot.OnSell += Market;
        InventoryUI.OnOpen += OpenCloseInventory;
        InventoryUI.OnClose += OpenCloseInventory;
        DragAndDrop.OnMoveItem += MoveItem;
    }
    private void OnDisable()
    {
        MarketSlot.OnSell -= Market;
        InventoryUI.OnOpen -= OpenCloseInventory;
        InventoryUI.OnClose -= OpenCloseInventory;
        DragAndDrop.OnMoveItem -= MoveItem;
    }
    void MoveItem()
    {
        AkSoundEngine.PostEvent("Item_Move", this.gameObject);
    }
    void OpenCloseInventory()
    {
        AkSoundEngine.PostEvent("UI_Click", this.gameObject);
    }
    void Market()
    {
        AkSoundEngine.PostEvent("Item_Sell", this.gameObject);
    }
}
