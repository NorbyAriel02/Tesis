using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestController : MonoBehaviour
{
    public string questVersion = "Version1";
    public Button btnBack;
    public Button btnNext;
    public Button btnCancelar;
    public Button btnAceptar;
    public Button btnExit;
    public GameObject itemQuestTemplate;    
    public Text txtDialogQuest;
    public GameObject contentQuest;
    public GameObject panelQuest;
    public GameObject panelMenu;
    public List<Sprite> imgQuests;
    private int currentIndexText;
    private int currentIndexQuest;
    private List<QuestModel> quests;
    private List<GameObject> buttonsQuest;
    private void OnEnable()
    {
        if(buttonsQuest != null)
            LoadQuest();
    }
    void Start()
    {
        btnExit.onClick.AddListener(Exit);
        btnNext.onClick.AddListener(Next);
        btnBack.onClick.AddListener(Back);       
        btnAceptar.onClick.AddListener(Aceptar);
        btnCancelar.onClick.AddListener(Cancelar);
        DesactiveButtons();
        contentQuest.SetActive(false);
        buttonsQuest = new List<GameObject>();              
    }
    void DesactiveButtons()
    {
        btnNext.gameObject.SetActive(false);
        btnBack.gameObject.SetActive(false);
        btnAceptar.gameObject.SetActive(false);
        btnCancelar.gameObject.SetActive(false);
    }
    void Aceptar()
    {
        quests[currentIndexQuest].status = QuestStatus.activa;
        QuestHelper.Save(quests, questVersion);
        DesactiveButtons();
        Despedida();
        AssignImg(buttonsQuest[currentIndexQuest], 1);
    }    
    void Cancelar()
    {
        quests[currentIndexQuest].status = QuestStatus.esperando;
        QuestHelper.Save(quests, questVersion);
        DesactiveButtons();
        Despedida();
        AssignImg(buttonsQuest[currentIndexQuest], 0);
    }
    void Despedida()
    {
        txtDialogQuest.text = "Buen Viaje!!!";
    }
    void LoadQuest()
    {
        int index = 0;
        quests = QuestHelper.GetQuests(questVersion);
        foreach(QuestModel quest in quests)
        {
            if (quest.status == QuestStatus.completa)
                continue;

            if (quest.idkingdom != PlayerDataHelper.GetIdCurrentKingdom())
                continue;            

            GameObject item = Instantiate(itemQuestTemplate, panelQuest.transform);
            int id = index;
            item.GetComponent<Quest>().Index = id;
            item.GetComponentInChildren<Text>().text = quest.tittle;
            
            if (quest.status == QuestStatus.activa)
                AssignImg(item, 1);
                
            item.GetComponent<Button>().onClick.AddListener(delegate { ShowQuest(id); });
            buttonsQuest.Add(item);
            index++;
        }
    }

    void AssignImg(GameObject item, int index)
    {
        GameObject[] children = ChildrenController.GetChildren(item);
        foreach(GameObject child in children)
        {
            Image img = child.GetComponent<Image>();
            if(img != null)
            {
                img.sprite = imgQuests[index];
                break;
            }    
        }
    }
    void ShowQuest(int id)
    {
        currentIndexText = 0;
        currentIndexQuest = id;
        AssignText(currentIndexQuest, currentIndexText);
        ActiveButtons();
    }
    void AssignText(int quest, int dialog)
    {
        if (quests[quest].status == QuestStatus.esperando)
        {
            txtDialogQuest.text = quests[quest].initialMessage[dialog];
        }

        if (quests[quest].status == QuestStatus.activa)
        {
            txtDialogQuest.text = quests[quest].middleMessage[dialog];
        }

        if (quests[quest].status == QuestStatus.completa)
        {
            txtDialogQuest.text = quests[quest].finalMessage[dialog];
        }
    }
    void Next()
    {
        currentIndexText++;
        AssignText(currentIndexQuest, currentIndexText);
        ActiveButtons();
    }    
    void Back()
    {
        currentIndexText--;
        AssignText(currentIndexQuest, currentIndexText);
        ActiveButtons();
    }
    void ActiveButtons()
    {
        if (currentIndexText == 0)
            btnBack.gameObject.SetActive(false);
        else if (currentIndexText > 0)
            btnBack.gameObject.SetActive(true);

        if(quests[currentIndexQuest].status == QuestStatus.esperando)
        {
            if (currentIndexText < (quests[currentIndexQuest].initialMessage.Count - 1))
            {
                btnNext.gameObject.SetActive(true);
            }

            if (currentIndexText == (quests[currentIndexQuest].initialMessage.Count - 1))
            {
                btnNext.gameObject.SetActive(false);
                btnBack.gameObject.SetActive(false);
                btnCancelar.gameObject.SetActive(true);
                btnAceptar.gameObject.SetActive(true);
            }
        }
        else
        {
            if (currentIndexText < (quests[currentIndexQuest].middleMessage.Count - 1))
            {
                btnNext.gameObject.SetActive(true);
            }

            if (currentIndexText == (quests[currentIndexQuest].middleMessage.Count - 1))
            {
                btnNext.gameObject.SetActive(false);
                btnBack.gameObject.SetActive(false);
                btnCancelar.gameObject.SetActive(true);
                btnAceptar.gameObject.SetActive(true);
            }
        }
    }
    void Exit()
    {
        contentQuest.SetActive(false);
        panelMenu.SetActive(true);
    }
}
