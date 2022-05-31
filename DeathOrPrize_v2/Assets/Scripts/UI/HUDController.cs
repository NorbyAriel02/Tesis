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
    public Texture2D[] cursos;
    private InventoryManager inventory;
    PlayerMove playerMove;
    UIDayNight uIDayNight;
    void Start()
    {
        TextKimgdom.text = "Reino " + PlayerDataHelper.GetIdCurrentKingdom();
        playerMove = GetScript.Type<PlayerMove>("Player");
        btnDado.onClick.AddListener(RollDice);
        btnInventory.onClick.AddListener(Open);
        inventory = GetScript.Type<InventoryManager>("Inventory");
        uIDayNight = GetComponent<UIDayNight>();
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
    public void EnterCity()
    {
        Heal();
        uIDayNight.ResetDay();
    }
    void Open()
    {        
        inventory.OpenInventory();
    }
    void SetInteractiveBtnDice()
    {
        if (playerMove.diceValue > 0)
        {
            btnDado.interactable = false;
            Cursor.SetCursor(cursos[0], new Vector2(5, 5), CursorMode.Auto);
        }            
        else
        {
            btnDado.interactable = true;
            Cursor.SetCursor(cursos[1], new Vector2(5, 5), CursorMode.Auto);
        }
            
    }

    void UpdateTextValue()
    {
        textDice.text = playerMove.diceValue.ToString();
    }
    void RollDice()
    {
        uIDayNight.AddRoll();
        GetNewValueDeci();
    }
    void GetNewValueDeci()
    {        
        AkSoundEngine.PostEvent("Throw_Dice", this.gameObject);
        playerMove.diceValue = Random.Range(1, maxValueDice);
    }    
    void Update()
    {
        SetInteractiveBtnDice();

        UpdateTextValue();
    }
}
