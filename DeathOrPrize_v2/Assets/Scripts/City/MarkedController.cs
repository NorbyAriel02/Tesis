using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarkedController : MonoBehaviour
{
    public Button btnExit;
    public GameObject panelMarked;
    public GameObject panelMenu;
    void Start()
    {
        btnExit.onClick.AddListener(Exit);
    }
    void Exit()
    {
        panelMarked.SetActive(false);
        panelMenu.SetActive(true);
    }
}
