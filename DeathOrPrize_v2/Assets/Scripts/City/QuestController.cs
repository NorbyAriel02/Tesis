using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestController : MonoBehaviour
{
    public Button btnExit;
    public GameObject itemQuestTemplate;
    public GameObject panelQuest;
    public GameObject panelMenu;
    void Start()
    {
        btnExit.onClick.AddListener(Exit);
        panelQuest.SetActive(false);
    }
    void Exit()
    {
        panelQuest.SetActive(false);
        panelMenu.SetActive(true);
    }
}
