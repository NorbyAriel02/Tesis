using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDayNight : MonoBehaviour
{
    public int rollValue = 1;    
    public Image imgDayNight;
    public List<string> paths;
    DataFileController fileController = new DataFileController();
    private Sprite[] sheets;
    private bool IsDay = true;
    private int index = 0;
    private void OnEnable()
    {
        DiceController.OnRollDice += AddRoll;
        DayNightCicle.OnCycleChangesToDay += SetDay;
        DayNightCicle.OnCycleChangesToNight += SetNight;
        CityController.OnEnterCity += ResetDay;
    }
    private void OnDisable()
    {
        DiceController.OnRollDice -= AddRoll;
        DayNightCicle.OnCycleChangesToDay -= SetDay;
        DayNightCicle.OnCycleChangesToNight -= SetNight;
        CityController.OnEnterCity -= ResetDay;
    }


    void Start()
    {
        LoadSpriteSheets();
    }
    public void ResetDay()
    {
        index = 0;
        SetImg();
    }
    public void AddRoll(int diceValue)
    {        
        rollValue++;
        index++;
        SetImg();
    }
    void SetImg()
    {
        if (IsDay)
            imgDayNight.sprite = sheets[index];
        else
            imgDayNight.sprite = sheets[3 + index];        
    }
    void LoadSpriteSheets()
    {
        sheets = fileController.LoadSpriteSheet(paths[0]);
    }
    void SetDay()
    {
        IsDay = true;
        index = 0;
    }
    void SetNight()
    {
        IsDay = false;
        index = 0;
    }
}
