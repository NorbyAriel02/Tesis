using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCicle : MonoBehaviour
{
    public List<string> paths;
    public int RollNumberForDay = 3;
    public int RollNumberForNight = 2;
    public int diceRollNumber;
    public bool IsDay = true;
    public GameObject panelNihgt;
    PlayerMove playerMove;
    DataFileController fileController = new DataFileController();
    private Sprite[] sheets;
    void Start()
    {
        panelNihgt.SetActive(false);
        LoadSpriteSheets();
        playerMove = GetScript.Type<PlayerMove>("Player");
    }
    public void AddRollNumber(int value)
    {
        diceRollNumber += value;

        if (diceRollNumber >= RollNumberForDay && IsDay)
        {
            SetNight();
        }            
        else if(diceRollNumber >= RollNumberForNight && !IsDay)
        {
            SetDay();            
        }            
    }
    public void SetNight()
    {
        diceRollNumber = 0;
        IsDay = false;
        panelNihgt.SetActive(true);
    }
    public void SetDay()
    {
        diceRollNumber = 0;
        panelNihgt.SetActive(false);
        IsDay = true;
    }
    public Sprite GetSpriteDayNight(int index)
    {
        if (IsDay)
            return sheets[index];
        else
            return sheets[3 + index];
    }
    void LoadSpriteSheets()
    {
        sheets = fileController.LoadSpriteSheet(paths[0]);        
    }

}
