using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{    
    private GameObject goToolTip;
    private Item item;
    ToolTipValues toolTipValues;
    ActiveToolTip activeToolTip;
    
    private void OnEnable()
    {
        AssignVars();
    }
    void AssignVars()
    {
        goToolTip = GameObject.FindGameObjectWithTag("ToolTip");
        toolTipValues = goToolTip.GetComponent<ToolTipValues>();
        activeToolTip = goToolTip.GetComponent<ActiveToolTip>();
        item = GetComponent<Item>();
    }
    void AsignarValueToolTip()
    {
        toolTipValues.Assign(item.properties);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (goToolTip == null)
            AssignVars();

        activeToolTip.Active();
        AsignarValueToolTip();
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (goToolTip == null)
            AssignVars();

        activeToolTip.Deactive();
    }
}
