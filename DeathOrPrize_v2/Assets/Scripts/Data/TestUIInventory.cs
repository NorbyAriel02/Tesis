using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TestUIInventory : MonoBehaviour
{
    public string DataFile = "inventory";
    public Button btnAdd;
    public InventoryManager im;
    void Start()
    {
        btnAdd.onClick.AddListener(AddItem);
        im.UpdateView();
    }
    void AddItem()
    {
        ItemModel item = Utilitis.GetRandomItem(Random.Range(1,10), Owner.player, DataFile);
        DataHelper.AddItemInventory(item);
    }
}
