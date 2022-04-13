using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityController : MonoBehaviour
{
    public Button btnDoorEast;
    public Button btnDoorWest;
    public Button btnDoorNorth;
    public Button btnDoorSouth;
    public Button btnMarked;
    public Button btnSmithy;
    public Button btnQuests;
    public GameObject panelCity;
    public GameObject panelMenu;
    public GameObject panelSmithy;
    public GameObject panelMarked;
    public GameObject panelQuest;
    public GameObject LoadKingdom;
    public GameObject prefabItemTemplate;
    public GameObject inventoryPanel;
    public GameObject EquipmentPanel;
    public GameObject[] SlotsInventory;
    public GameObject[] SlotsEquipment;
    public List<ItemProperties> itemsInventory;
    public List<ItemProperties> itemsEquipment;
    public HUDController hud;
    private List<Vector3> doors;
    private PlayerMove playerMov;
    List<CellModel> dataMap = new List<CellModel>();
    DataFileController fileController = new DataFileController();
    // Start is called before the first frame update
    void Start()
    {
        playerMov = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
        AsignEventButtons();
        LoadItems();
        panelCity.SetActive(false);
    }
    void LoadItems()
    {
        itemsInventory = InventoryHelper.GetListItemsFromFile(PathHelper.InventoryDataFile);
        itemsEquipment = InventoryHelper.GetListItemsFromFile(PathHelper.EquipmentDataFile);
        GetSlotsEquipment();
        GetSlotsInventory();
        LoadInventory();
        LoadEquipment();
    }
    void GetSlotsInventory()
    {
        SlotsInventory = ChildrenController.GetChildren(inventoryPanel);
        for (int x = 0; x < SlotsInventory.Length; x++)
        {
            SlotsInventory[x].GetComponent<Slot>().ID = x;
            SlotsInventory[x].GetComponent<Slot>().empty = true;
        }
    }
    void GetSlotsEquipment()
    {        
        SlotsEquipment = ChildrenController.GetChildren(EquipmentPanel);
        for (int x = 0; x < SlotsEquipment.Length; x++)
        {
            SlotsEquipment[x].GetComponent<Slot>().ID = x;
            SlotsEquipment[x].GetComponent<Slot>().empty = true;
        }
    }
    void LoadInventory()
    {
        InventoryHelper.ShowItems(SlotsInventory, prefabItemTemplate, PathHelper.InventoryDataFile);
    }
    void LoadEquipment()
    {
        InventoryHelper.ShowItems(SlotsEquipment, prefabItemTemplate, PathHelper.EquipmentDataFile);
    }    
    void AsignEventButtons()
    {
        btnDoorEast.onClick.AddListener(ExitEast);
        btnDoorWest.onClick.AddListener(ExitWest);
        btnDoorNorth.onClick.AddListener(ExitNorth);
        btnDoorSouth.onClick.AddListener(ExitSouth);
        btnSmithy.onClick.AddListener(OpenSmithy);
        btnMarked.onClick.AddListener(OpenMarked);
        btnQuests.onClick.AddListener(OpenQuestPanel);
    }
    void OpenSmithy()
    {
        panelSmithy.SetActive(true);
        panelMarked.SetActive(false);
        panelQuest.SetActive(false);
    }
    void OpenMarked()
    {
        panelSmithy.SetActive(false);
        panelMarked.SetActive(true);
        panelQuest.SetActive(false);
    }
    void OpenQuestPanel()
    {
        panelSmithy.SetActive(false);
        panelMarked.SetActive(false);
        panelQuest.SetActive(true);
    }
    void ExitSouth()
    {
        UpdateData();
        playerMov.SetPosition(doors[2].x, doors[2].y);
        panelCity.SetActive(false);
        LoadKingdom.SetActive(true);
    }
    void ExitWest()
    {
        UpdateData();
        playerMov.SetPosition(doors[3].x, doors[3].y);
        panelCity.SetActive(false);
        LoadKingdom.SetActive(true);
    }
    void ExitEast()
    {
        UpdateData();
        playerMov.SetPosition(doors[1].x, doors[1].y);
        panelCity.SetActive(false);
        LoadKingdom.SetActive(true);
    }
    void ExitNorth()
    {
        UpdateData();
        playerMov.SetPosition(doors[0].x, doors[0].y);
        panelCity.SetActive(false);
        LoadKingdom.SetActive(true);
    }    
    void UpdateData()
    {
        itemsInventory = InventoryHelper.GetListItemsFromPanel(SlotsInventory);
        InventoryHelper.Save(itemsInventory, PathHelper.InventoryDataFile);
        itemsEquipment = InventoryHelper.GetListItemsFromPanel(SlotsEquipment);
        InventoryHelper.Save(itemsEquipment, PathHelper.EquipmentDataFile);
    }
    public void Enter(float x, float y, int subTypeId)
    {
        hud.Heal();
        SetExits(x, y, subTypeId);
        LoadItems();
        panelCity.SetActive(true);
    }
    private void SetExits(float x, float y, int subType)
    {
        //door 0 is north
        //door 1 is east
        //door 2 is South
        //door 3 is west
        doors = new List<Vector3>();
        if (subType == 1)//ingresa por el norte
        {
            doors.Add(new Vector3(x, y + 1, 0));
            doors.Add(new Vector3(x + 2, y - 1, 0));
            doors.Add(new Vector3(x, y - 3, 0));
            doors.Add(new Vector3(x - 2, y - 1, 0));
        }
        else if (subType == 3)//ingresa por el este
        {
            doors.Add(new Vector3(x - 1, y + 2, 0));
            doors.Add(new Vector3(x + 1, y, 0));
            doors.Add(new Vector3(x - 1, y - 2, 0));
            doors.Add(new Vector3(x - 3, y, 0));
        }
        else if (subType == 5)//ingresa por el sur
        {
            doors.Add(new Vector3(x, y + 3, 0));
            doors.Add(new Vector3(x + 2, y + 1, 0));
            doors.Add(new Vector3(x, y - 1, 0));
            doors.Add(new Vector3(x - 2, y + 1, 0));
        }
        else if (subType == 7)//ingresa por el oeste
        {
            doors.Add(new Vector3(x + 1, y + 2, 0));
            doors.Add(new Vector3(x + 3, y, 0));
            doors.Add(new Vector3(x + 1, y - 2, 0));
            doors.Add(new Vector3(x - 1, y, 0));
        }
    }
}
