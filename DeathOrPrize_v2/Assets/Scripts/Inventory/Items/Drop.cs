using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    public delegate void Pickup(GameObject item);
    public static Pickup OnPickupItem;
    public delegate void CantPickup();
    public static CantPickup OnCantPickupItem;
    public ItemModel item;    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    

    void OnMouseOver()
    {        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Input.GetKeyDown(KeyCode.Mouse0) && Physics.Raycast(ray, out hit))
        {
            if (hit.transform.name == gameObject.name)
            {
                int slots = DataHelper.GetSlotsNumberInventory();
                int current = DataHelper.GetListInventory().Count;
                if (current < slots)
                {
                    DataHelper.AddItemList(item);
                    OnPickupItem?.Invoke(gameObject);
                    Destroy(gameObject);                    
                }
                else
                    OnCantPickupItem?.Invoke();
            }
        }
    }
}
