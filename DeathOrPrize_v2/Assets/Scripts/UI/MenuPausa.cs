using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    public delegate void Open();
    public static Open OnMenuOpen;
    public delegate void Close();
    public static Close OnMenuClose;

    public KeyCode key;
    public Button btnResumen;
    public Button btnMenu;
    public Button btnQuit;
    public GameObject panelMenuPausa;
    public Animator animator;
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
        MenuOff();
    }
    void MenuOn()
    {        
        panelMenuPausa.SetActive(true);
        animator.SetBool("Close", false);
        OnMenuOpen?.Invoke();
    }
    void MenuOff()
    {
        animator.SetBool("Close", true);
        OnMenuClose?.Invoke();
    }
    void Update()
    {
        if (Input.GetKeyDown(key) && !panelMenuPausa.activeSelf)
            MenuOn();
        else if (Input.GetKeyDown(key) && panelMenuPausa.activeSelf)
            MenuOff();
    }
}
