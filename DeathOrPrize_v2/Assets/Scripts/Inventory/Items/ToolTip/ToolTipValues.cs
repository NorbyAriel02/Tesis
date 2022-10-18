using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTipValues : MonoBehaviour
{
    public Text[] texts;
    string[] campos = { "level", "damage", "attackSpeed", "armor", "value" };
    private void Start()
    {
        ClearText();
    }
    public void Assign(ItemModel item)
    {
        ClearText();
        int countText = 0;
        for (int x = 0; x < campos.Length; x++)
        {
            if(item.GetType().GetField(campos[x]) != null)
            {
                texts[countText].enabled = true;                
                texts[countText].text = campos[x].ToUpper() + " " + item.GetType().GetField(campos[x]).GetValue(item);
                countText++;
            }
        }
    }
    public void ClearText()
    {
        for (int x = 0; x < texts.Length; x++)
        {
            texts[x].enabled = false;
        }
    }
}
