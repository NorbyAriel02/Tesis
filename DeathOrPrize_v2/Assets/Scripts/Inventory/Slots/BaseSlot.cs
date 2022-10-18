using UnityEngine;
using UnityEngine.EventSystems;

public class BaseSlot : MonoBehaviour, IDropHandler
{
    
    public delegate void MoveItemFail();
    public static MoveItemFail OnMoveItemFail;
    public delegate void EventBuy();
    public static EventBuy OnBuy;
    public delegate void EventCantBuy();
    public static EventCantBuy OnCantBuy;

    public GameObject Item;
    public TypeSlot typeSlot;
    public int ID;
    public string description;
    public Sprite icon;
    public bool empty;
    public bool cancelar;
    private RenderTexture rt;
    public string DataFiles = "default";
    public float SalesCommission = 0.35f;
    private void Start()
    {
        IsEmptySlot();
    }
    void IsEmptySlot()
    {
        GameObject child = ChildrenController.GetChild(gameObject);
        if (child != null)
            empty = false;
        else
            empty = true;
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && IsEnabled)
        {
            Item i = eventData.pointerDrag.GetComponent<Item>();
            if (i == null)
                return;

            if (ActionSlot(i.item))
            {
                UpdateData(i.item);
                PositionItem(eventData.pointerDrag.transform);
            }
        }
    }
    void UpdateData(ItemModel item)
    {
        item.DataFile = this.DataFiles;
        item.IndexSlot = this.ID;
        this.empty = false;        
    }
    public void PositionItem(Transform item)
    {
        item.SetParent(transform);
        item.position = transform.position;
    }
    public bool IsEnabled
    {
        get
        {
            if(this.empty)
                if (typeSlot != TypeSlot.NoDropSlot)
                    return true;

            return false;
        }
    }
    public virtual bool ActionSlot(ItemModel item)
    {

        return true;
    }
    public bool Buy(ItemModel item)
    {
        int current = DataHelper.GetCoins();
        if (current >= item.value)
        {
            DataHelper.AddCoins(-item.value);
            item.owner = Owner.player;
            OnBuy.Invoke();
            return true;
        }
        else
            OnCantBuy?.Invoke();

        return false;
    }
    public void Sell(ItemModel item)
    {
        int price = Mathf.RoundToInt(item.value - (item.value * SalesCommission));        
        DataHelper.AddCoins(price);
        item.owner = Owner.seller;
    }
}
