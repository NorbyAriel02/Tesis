using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public delegate void MoveItem();
    public static MoveItem OnMoveItem;

    public Transform fatherMaster;
    public Canvas canvasInventario;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public bool HaveParent = false;
    public Transform PadreActual;
    public Item item;
    void Start()
    {
        fatherMaster = GetFatherMaster(transform);
        rectTransform = GetComponent<RectTransform>();
        canvasInventario = fatherMaster.GetComponent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        item = GetComponent<Item>();
    }
    Transform GetFatherMaster(Transform child)
    {
        Transform father = child.parent;

        if (father != null)
            return GetFatherMaster(father);

        return child;
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
        PadreActual = transform.parent;
        if (transform.parent.gameObject.GetComponent<BaseSlot>() != null)
            transform.parent.gameObject.GetComponent<BaseSlot>().empty = true;

        transform.SetParent(fatherMaster);

        RemoveItemFromList(item.item);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        if (PadreActual == transform.parent || fatherMaster == transform.parent)
        {
            transform.SetParent(PadreActual);
            transform.position = PadreActual.position;
            //si vuelve al mismo padre porque se solto mal, se vuelve a marcar como no vacio
            if (transform.parent.gameObject.GetComponent<BaseSlot>() != null)
                transform.parent.gameObject.GetComponent<BaseSlot>().empty = false;
        }
        AddItemToList(item.item);
        OnMoveItem?.Invoke();
    }
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvasInventario.scaleFactor;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        //gameObject.SetActive(false);
    }
    void RemoveItemFromList(ItemModel item)
    {
        DataHelper.RemoveItemList(item);
    }
    void AddItemToList(ItemModel item)
    {
        DataHelper.AddItemList(item);
    }
}
