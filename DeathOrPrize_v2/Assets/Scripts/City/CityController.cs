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
    private List<Vector3> doors;
    private PlayerMove playerMov;
    List<CellModel> dataMap = new List<CellModel>();
    DataFileController fileController = new DataFileController();
    // Start is called before the first frame update
    void Start()
    {
        playerMov = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
        panelCity.SetActive(false);
        AsignEventButtons();
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
        playerMov.SetPosition(doors[2].x, doors[2].y);
        panelCity.SetActive(false);
        LoadKingdom.SetActive(true);
    }
    void ExitWest()
    {
        playerMov.SetPosition(doors[3].x, doors[3].y);
        panelCity.SetActive(false);
        LoadKingdom.SetActive(true);
    }
    void ExitEast()
    {
        playerMov.SetPosition(doors[1].x, doors[1].y);
        panelCity.SetActive(false);
        LoadKingdom.SetActive(true);
    }
    void ExitNorth()
    {
        playerMov.SetPosition(doors[0].x, doors[0].y);
        panelCity.SetActive(false);
        LoadKingdom.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void Enter(float x, float y, int subTypeId)
    {
        SetExits(x, y, subTypeId);
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
