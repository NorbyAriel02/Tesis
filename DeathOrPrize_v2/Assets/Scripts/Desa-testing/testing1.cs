using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testing1 : MonoBehaviour
{
    public GameObject drop;

    void Start()
    {
    
    }
    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            DataHelper.Heal();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            DataHelper.UpdateIdCurrentKingdom(9);
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            DataHelper.UpdateSlotsNumberInventory(18);
            ItemModel w = Utilitis.GetWeapon(1, Owner.player, "inventory");
            GameObject d = Instantiate(drop, new Vector3(0,0,0), Quaternion.identity);
            d.GetComponent<Drop>().item = w;
        }
    }

    
}
