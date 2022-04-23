using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public int maxValueDice = 6;
    public Button btnDado;
    public Text textDice;
    public Button btnInventory;
    public Image barHealth;
    public Text TextHealth;
    public Text TextKimgdom;
    private InventoryManager inventory;
    PlayerMove playerMove;
    void Start()
    {
        TextKimgdom.text = "Reino " + PlayerDataHelper.GetIdKingdom();
        playerMove = GetScript.Type<PlayerMove>("Player");
        btnDado.onClick.AddListener(GetNewValueDeci);
        btnInventory.onClick.AddListener(Open);
        inventory = GetScript.Type<InventoryManager>("Inventory");
        UpdateBarHealth();
    }

    public void UpdateBarHealth()
    {
        float health = PlayerDataHelper.GetCurrentHealth();
        float maxHealth = PlayerDataHelper.GetMaxHealth();
        barHealth.fillAmount = health / maxHealth;
        TextHealth.text = health.ToString("0");
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
    void SetInteractiveBtnDice()
    {
        if (playerMove.diceValue > 0)
            btnDado.interactable = false;
        else
            btnDado.interactable = true;
    }

    void UpdateTextValue()
    {
        textDice.text = playerMove.diceValue.ToString();
    }
    void GetNewValueDeci()
    {
        playerMove.diceValue = Random.Range(1, maxValueDice);
    }
    // Update is called once per frame
    void Update()
    {
        SetInteractiveBtnDice();

        UpdateTextValue();
    }
}
