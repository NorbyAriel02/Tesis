using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{
    public ItemProperties item;    
    private InventoryManager inventory;
    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventoryManager>();
    }

    // Update is called once per frame
    void Update()
    {
        AddInventory();
    }

    public void AddInventory()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetKeyDown(KeyCode.Mouse0) && Physics.Raycast(ray, out hit))
        {
            if (hit.transform.name == gameObject.name)
            {
                if (inventory.AddItem(item))
                    Destroy(gameObject);
                else
                    CantGetUp();
            }            
        }
    }
    public void CantGetUp()
    {
        Debug.Log("Inventario llego");
    }
}
