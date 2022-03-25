using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmithyController : MonoBehaviour
{
    public Button btnExit;
    public GameObject panelSmithy;
    public GameObject panelMenu;
    void Start()
    {
        btnExit.onClick.AddListener(Exit);
    }
    void Exit()
    {
        panelSmithy.SetActive(false);
        panelMenu.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
