using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public Button btnInventory;
    public Image barHealth;
    public Text TextHealth;
    private InventoryManager inventory;
    void Start()
    {
        btnInventory.onClick.AddListener(Open);        
        inventory = GetScript.Type<InventoryManager>("Inventory");
        UpdateBarHealth();
    }
    public void UpdateBarHealth()
    {        
        float health = PlayerDataHelper.GetCurrentHealth();
        float maxHealth = PlayerDataHelper.GetMaxHealth();
        barHealth.fillAmount = health / maxHealth;
        TextHealth.text = health.ToString();
    }

    public void Heal()
    {
        PlayerDataHelper.Heal();
        UpdateBarHealth();
    }
    void Open()
    {
        inventory.OpenInventory();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
