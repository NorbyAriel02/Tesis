using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public Transform fatherMaster;
    public Canvas canvasInventario;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    public bool HaveParent = false;
    public Transform PadreActual;
    
    void Start()
    {
        fatherMaster = GetFatherMaster();
        rectTransform = GetComponent<RectTransform>();
        canvasInventario = FindObjectOfType<Canvas>();
        canvasGroup = GetComponentInParent<CanvasGroup>();
    }
    Transform GetFatherMaster()
    {
        Transform father = transform.parent;
        for(int x = 0; x < 4; x++)
        {
            father = father.parent;
        }
        return father;
    }

    private void Update()
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
        PadreActual = transform.parent;
        if(transform.parent.gameObject.GetComponent<Slot>() != null)
            transform.parent.gameObject.GetComponent<Slot>().empty = true;

        transform.SetParent(fatherMaster);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        if (PadreActual == transform.parent || fatherMaster == transform.parent)
        {
            transform.SetParent(PadreActual);
            transform.position = PadreActual.position;
            //si vuelve al mismo padre porque se solto mal, se vuelve a marcar como no vacioS            
            if (transform.parent.gameObject.GetComponent<Slot>() != null)
                transform.parent.gameObject.GetComponent<Slot>().empty = false;            
        }
            
    }
    public void OnDrag(PointerEventData eventData)
    {        
        rectTransform.anchoredPosition += eventData.delta/canvasInventario.scaleFactor;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        //gameObject.SetActive(false);
    }
}
