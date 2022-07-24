using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerStart : MonoBehaviour
{
    public int SlotsInventoryStart = 5;
    void Start()
    {        
        DataHelper.NewGame();
        DataHelper.UpdateSlotsNumberInventory(SlotsInventoryStart);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
