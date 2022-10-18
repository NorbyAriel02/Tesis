using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{    
    private GameObject goToolTip;
    private Item item;
    ToolTipValues toolTipValues;
    
    private void OnEnable()
    {
        AssignVars();
    }
    void AssignVars()
    {
        goToolTip = GameObject.FindGameObjectWithTag("ToolTip");
        toolTipValues = goToolTip.GetComponent<ToolTipValues>();        
        item = GetComponent<Item>();
    }
    void AsignarValueToolTip()
    {
        toolTipValues.Assign(item.item);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (goToolTip == null)
            AssignVars();
        
        AsignarValueToolTip();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (goToolTip == null)
            AssignVars();

        toolTipValues.ClearText();
    }
}
