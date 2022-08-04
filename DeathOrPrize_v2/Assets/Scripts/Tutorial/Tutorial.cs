using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public delegate void StartDialogue(int indexDialogue);
    public static StartDialogue OnStartDialogue;
    public delegate void Next();
    public static Next OnNextAction;

    public Button btnSiguiente;
    public Button btnFinTutorial;
    public Text textMsj;
    public int StepTutorial = 0;
    public Transform playerTarget;
    public Transform playerT;
    public GameObject itemDrop;
    public GameObject panel;    
    public GameObject[] signs;
    private void OnEnable()
    {
        Dialogue.OnEndDialogue += DisablePanel;
        CityCellTuto.OnEnterCity += StartDialogueTutorial;
        DiceController.OnRollDice += DeactivateSignDice;
        Drop.OnPickupItem += ActivateSignInventory;
        HUDController.OnInventoryOpenOrClose += DesactiveSignInventory;
        EquipSlot.OnEquiped += DesactiveSignWeapon;
    }
    private void OnDisable()
    {
        Dialogue.OnEndDialogue -= DisablePanel;
        CityCellTuto.OnEnterCity -= StartDialogueTutorial;
        DiceController.OnRollDice -= DeactivateSignDice;
        Drop.OnPickupItem -= ActivateSignInventory;
        HUDController.OnInventoryOpenOrClose -= DesactiveSignInventory;
    }
    private void Awake()
    {
        
    }
    void Start()
    {
        btnSiguiente.onClick.AddListener(NextDialogue);
        btnFinTutorial.onClick.AddListener(LoadLevel);
        StartDialogueTutorial(StepTutorial);
        StepTutorial = 1;
    }
    void DisablePanel()
    {
        AccionTutorial();

        panel.SetActive(false);
    }
    void NextDialogue()
    {
        AkSoundEngine.PostEvent("UI_Click", this.gameObject);
        OnNextAction?.Invoke();
    }
    void StartDialogueTutorial(int index)
    {        
        //print("Invoca con el id " + index);
        panel.SetActive(true);
        if(index == 2)
        {            
            StepTutorial = 5;
            playerTarget.position = PlayerDataHelper.GetStartPosition();
            playerT.position = playerTarget.position;
        }        
        OnStartDialogue?.Invoke(index);
    }
    void LoadLevel()
    {        
        SceneManager.LoadScene("Level");
    }
    void AddStep()
    {
        StepTutorial++;
        //print("Step " + StepTutorial);
    }
    void DeactivateSignDice(int diceValue)
    {
        textMsj.text = "Tienen " + diceValue + " movimientos";
        signs[0].SetActive(false);
        DeactivateSignDay();
    }
    void ActivateSignDice()
    {
        StepTutorial = 2;
        signs[0].SetActive(true);
    }

    public void ActivateSignDay()
    {
        textMsj.text = "El ciclo de día noche se actualiza con el lanzamiento del dado, el dia dura 3 lanzamiento y la noche 2";
        signs[1].SetActive(true);
        signs[2].SetActive(false);
    }
    public void ActivateDropItem()
    {
        itemDrop.SetActive(true);
        textMsj.text = "¡¡Mira adelante!! ¿puedes ver ese objeto? ¡Recogelo!";        
    }
    public void DesactiveSignWeapon()
    {
        if(StepTutorial == 3)
        {
            textMsj.text = "Cierra el inventario y sigue adelante";
            signs[5].SetActive(false);
            StepTutorial = 4;
        }        
    }
    public void ActivateSignInventory(GameObject item)
    {
        if (StepTutorial > 2)
            return;

        signs[3].SetActive(true);
        signs[4].SetActive(false);
        textMsj.text = "Revisa tu inventario para ver que recogiste";
    }
    public void StartBattle()
    {
        signs[6].SetActive(false);
        signs[7].SetActive(false);
        signs[8].SetActive(false);
        signs[9].SetActive(false);
    }
    void DesactiveSignInventory()
    {
        if (StepTutorial == 2)
        {
            signs[3].SetActive(false);
            signs[5].SetActive(true);
            textMsj.text = "Necesitas equiparte esa espada para poder defenderte, Haz clic sobre ella y arrastra hasta el cuadro resaltado";
            StepTutorial = 3; 
        }
        else if(StepTutorial == 4)
        {
            textMsj.text = "Estamos listos para avanzar";
            StepTutorial = 5;
        }
        else if(StepTutorial == 3)
        {
            textMsj.text = "Sin equipar la espada no tendrás cómo defenderte";
            signs[5].SetActive(false);
        }
    }
    void DeactivateSignDay()
    {
        signs[1].SetActive(false);
    }
    void AccionTutorial()
    {
        if (StepTutorial == 1)
            ActivateSignDice();

        if (StepTutorial == 5)
            LoadLevel();

        //AddStep();
    }
}
