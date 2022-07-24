using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    public delegate void Pickup(GameObject item);
    public static Pickup OnPickupItem;
    public ItemProperties item;    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void CantGetUp()
    {
        AkSoundEngine.PostEvent("Field_Error", this.gameObject);
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
                    Destroy(gameObject);
                    OnPickupItem?.Invoke(gameObject);
                }
                else
                    CantGetUp();

                //if (inventory.AddItem(item))
                //{
                //    AkSoundEngine.PostEvent("UI_Click", this.gameObject);
                //    Destroy(gameObject);
                //}
                //else
                //    CantGetUp();
            }
        }
    }
}
