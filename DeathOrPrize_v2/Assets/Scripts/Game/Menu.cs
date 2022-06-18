using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Menu : MonoBehaviour
{
    public Button btnNew;
    public Button btnContinue;
    public Button btnExit;
    
    void Start()
    {
        
        btnNew.onClick.AddListener(New);
        btnContinue.onClick.AddListener(Continue);
        btnExit.onClick.AddListener(Exit);
        
        btnContinue.interactable = ValidarContinue();
    }
    void Continue()
    {
        SceneHelper.Load("Level");
    }
    void New()
    {
        SceneHelper.Load("CreateType");
    }
    void Exit()
    {
        Application.Quit();
    }   
    bool ValidarContinue()
    {
        DataFileController fileController = new DataFileController();
        if (fileController.Exists(PathHelper.WolrdDataFile(1)))
            return true;

        return false;
    }

    bool IsMouseOverUI()
    {
        return EventSystem.current.IsPointerOverGameObject();
    }

    private void Update()
    {
        
    }
    public void MouseOver()
    {
        AkSoundEngine.PostEvent("UI_ButtonHover", this.gameObject);
    }
}
