using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    public KeyCode key;
    public Button btnResumen;
    public Button btnMenu;
    public Button btnQuit;
    public GameObject panelMenuPausa;
    void Start()
    {
        btnMenu.onClick.AddListener(Menu);
        btnQuit.onClick.AddListener(Quit);
        btnResumen.onClick.AddListener(Resumen);
        panelMenuPausa.SetActive(false);
    }
    void Quit()
    {
        Application.Quit();
    }
    void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    void Resumen()
    {
        panelMenuPausa.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key))
            panelMenuPausa.SetActive(!panelMenuPausa.activeSelf);
    }
}
