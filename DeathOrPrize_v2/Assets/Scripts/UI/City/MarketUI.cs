using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MarketUI : MonoBehaviour
{   
    public delegate void Close();
    public static Close OnClose;

    public GameObject panelMarket;
    public GameObject panelMenu;
    public Button btnExit;
    public Text textCoins;
    public bool IsClose;
    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {
       
    }
    void Start()
    {
        btnExit.onClick.AddListener(Exit);
        panelMarket.SetActive(false);
    }
    void Exit()
    {                
        IsClose = true;
        panelMarket.SetActive(false);
        panelMenu.SetActive(true);
        OnClose?.Invoke();
    }
}
