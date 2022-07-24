using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityController : MonoBehaviour
{
    public delegate void EnterCity();
    public static EnterCity OnEnterCity;
    public delegate void ExitCity(float x, float y);
    public static ExitCity OnExitCity;
    public delegate void OpenMarket();
    public static OpenMarket OnOpenMarket;

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
    public HUDController hud;
    private List<Vector3> doors;
    
    List<CellModel> dataMap = new List<CellModel>();
    DataFileController fileController = new DataFileController();
    private void OnEnable()
    {
        CityCell.ClicOnDoorCity += Enter;
    }
    private void OnDisable()
    {
        CityCell.ClicOnDoorCity -= Enter;
    }
    void Start()
    {        
        AsignEventButtons();    
        panelCity.SetActive(false);
    }
    void AsignEventButtons()
    {
        //ExitNorth door 0
        //ExitEast door 1
        //ExitSouth door 2
        //ExitWest door 3
        btnDoorNorth.onClick.AddListener(delegate { Exit(0); });
        btnDoorEast.onClick.AddListener(delegate { Exit(1); });
        btnDoorSouth.onClick.AddListener(delegate { Exit(2); });
        btnDoorWest.onClick.AddListener(delegate { Exit(3); });
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
        OnOpenMarket?.Invoke();
    }
    void OpenQuestPanel()
    {
        panelSmithy.SetActive(false);
        panelMarked.SetActive(false);
        panelQuest.SetActive(true);
    }
    void Exit(int door)
    {        
        panelCity.SetActive(false);
        OnExitCity?.Invoke(doors[door].x, doors[door].y);
    }            
    public void Enter(float x, float y, int subTypeId)
    {
        hud.EnterCity();
        SetExits(x, y, subTypeId);        
        panelCity.SetActive(true);
        OnEnterCity?.Invoke();
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
