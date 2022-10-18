using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StackableItem : Item
{    
    public Text txtCount;
    public GameObject panelItemPartition;
    public override void SetItem(ItemModel value)
    {
        if (item.name != value.name)
            item = value;
        else
            item.Stack++;

        txtCount.text = item.Stack.ToString();
        image.sprite = SpriteList.GetSprite(item.sprite, item.level);
    }
}
