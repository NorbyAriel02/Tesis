using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreemGameOver : MonoBehaviour
{
    public Button btnContinue;
    public Button btnMenu;
    // Start is called before the first frame update
    void Start()
    {
        btnContinue.onClick.AddListener(Continue);
        btnMenu.onClick.AddListener(Menu);
    }
    void Menu()
    {
        //aca cargar escena de menu
        PlayerDataHelper.Heal();
        SceneManager.LoadScene("Menu");
    }
    void Continue()
    {
        //reposicionar player a la ciudad inicial y cargar level
        PlayerDataHelper.Heal();
        SceneManager.LoadScene("Level");
    }
}