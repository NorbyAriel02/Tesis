using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryTest : MonoBehaviour
{
    public Button btnAddItem;
    public Button btnAddItemStack;
    public Button btnAddItemApple;
    public Button btnAddSet;
    public Button btnUplevel;
    public GameObject dropeditem;
    private LevelSystem levelSystem;
    void Start()
    {
        btnAddItem.onClick.AddListener(Drop);
        btnAddItemStack.onClick.AddListener(DropStack);
        btnAddItemApple.onClick.AddListener(DropApple);
        btnAddSet.onClick.AddListener(Set);                
        btnUplevel.onClick.AddListener(Uplevel);
    }
    public void SetLevelSystem(LevelSystem levelSystem)
    {
        this.levelSystem = levelSystem;
    }
    private void OnEnable()
    {
        LevelController.StartLevelSystem += SetLevelSystem;
    }
    void Uplevel()
    {
        levelSystem.AddExperience(100);
    }
    void Set()
    {
        int level = Random.Range(0, 16);
        GameObject goItem = Instantiate(dropeditem, new Vector3(), Quaternion.identity);
        Drop drop = goItem.GetComponent<Drop>();
        drop.item = Utilitis.GetWeapon(level, Owner.player, "inventory");

        GameObject goItem1 = Instantiate(dropeditem, new Vector3(), Quaternion.identity);
        Drop drop1 = goItem1.GetComponent<Drop>();
        drop1.item = Utilitis.GetItemArmor<ItemArmor>(level, Owner.player, "inventory");

        GameObject goItem2 = Instantiate(dropeditem, new Vector3(), Quaternion.identity);
        Drop drop2 = goItem2.GetComponent<Drop>();
        drop2.item = Utilitis.GetItemArmor<ItemBoots>(level, Owner.player, "inventory");
        
        GameObject goItem3 = Instantiate(dropeditem, new Vector3(), Quaternion.identity);
        Drop drop3 = goItem3.GetComponent<Drop>();
        drop3.item = Utilitis.GetItemArmor<ItemGloves>(level, Owner.player, "inventory");
        
        GameObject goItem4 = Instantiate(dropeditem, new Vector3(), Quaternion.identity);
        Drop drop4 = goItem4.GetComponent<Drop>();
        drop4.item = Utilitis.GetItem<ItemRing>(level, Owner.player, "inventory");

        GameObject goItem5 = Instantiate(dropeditem, new Vector3(), Quaternion.identity);
        Drop drop5 = goItem5.GetComponent<Drop>();
        drop5.item = Utilitis.GetItem<ItemAmulet>(level, Owner.player, "inventory");

        GameObject goItem6 = Instantiate(dropeditem, new Vector3(), Quaternion.identity);
        Drop drop6 = goItem6.GetComponent<Drop>();
        drop6.item = Utilitis.GetItemArmor<ItemCape>(level, Owner.player, "inventory");
        
        GameObject goItem7 = Instantiate(dropeditem, new Vector3(), Quaternion.identity);
        Drop drop7 = goItem7.GetComponent<Drop>();
        drop7.item = Utilitis.GetItemArmor<ItemShield>(level, Owner.player, "inventory");

        GameObject goItem8 = Instantiate(dropeditem, new Vector3(), Quaternion.identity);
        Drop drop8 = goItem8.GetComponent<Drop>();
        drop8.item = Utilitis.GetItemArmor<ItemHelmet>(level, Owner.player, "inventory");
    }
    void Drop()
    {
        GameObject goItem = Instantiate(dropeditem, new Vector3(), Quaternion.identity);
        Drop drop = goItem.GetComponent<Drop>();
        drop.item = Utilitis.GetRandomItem(1, Owner.player, "inventory");
    }
    void DropStack()
    {
        GameObject goItem = Instantiate(dropeditem, new Vector3(), Quaternion.identity);
        Drop drop = goItem.GetComponent<Drop>();
        drop.item = Utilitis.GetRandomItemStack(1, Owner.player, "inventory");
    }
    void DropApple()
    {
        GameObject goItem = Instantiate(dropeditem, new Vector3(), Quaternion.identity);
        Drop drop = goItem.GetComponent<Drop>();
        drop.item = Utilitis.GetApple(1, Owner.player, "inventory");
    }
}
