using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestController : MonoBehaviour
{
    public Button btnExit;
    public GameObject panelQuest;
    public GameObject panelMenu;
    void Start()
    {
        btnExit.onClick.AddListener(Exit);
    }
    void Exit()
    {
        panelQuest.SetActive(false);
        panelMenu.SetActive(true);
    }
}
