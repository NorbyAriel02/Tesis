using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public delegate void Inventory();
    public static Inventory OnInventoryOpenOrClose;
    public Button btnInventory;
    public Image barHealth;
    public Text TextHealth;
    public Text TextKimgdom;    
    private InventoryManager inventory;
    PlayerMove playerMove;
    UIDayNight uIDayNight;
    private void OnEnable()
    {
        IdleBattleManager.OnDamageThePlayer += UpdateBarHealth;
        BossQuest.OnDamageThePlayer += UpdateBarHealth;
    }
    private void OnDisable()
    {
        IdleBattleManager.OnDamageThePlayer -= UpdateBarHealth;
        BossQuest.OnDamageThePlayer -= UpdateBarHealth;
    }
    void Start()
    {
        TextKimgdom.text = "Reino " + PlayerDataHelper.GetIdCurrentKingdom();
        playerMove = GetScript.Type<PlayerMove>("Player");
        btnInventory.onClick.AddListener(Open);
        inventory = GetScript.Type<InventoryManager>("Inventory");
        uIDayNight = GetComponent<UIDayNight>();
        UpdateBarHealth(0);
    }

    public void UpdateBarHealth(float damege)
    {
        float health = PlayerDataHelper.GetCurrentHealth();
        float maxHealth = PlayerDataHelper.GetMaxHealth();
        barHealth.fillAmount = health / maxHealth;
        TextHealth.text = health.ToString("0");
    }

    public void Heal()
    {
        PlayerDataHelper.Heal();
        UpdateBarHealth(0);
    }
    public void EnterCity()
    {
        Heal();
    }
    void Open()
    {        
        inventory.OpenOrClose();
        OnInventoryOpenOrClose?.Invoke();
    }
    void RollDice()
    {
        uIDayNight.AddRoll(1);
    }
}
