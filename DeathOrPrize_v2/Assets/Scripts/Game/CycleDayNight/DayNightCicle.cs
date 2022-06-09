using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCicle : MonoBehaviour
{
    public delegate void SetDay();
    public static SetDay OnCycleChangesToDay;
    public delegate void SetNihgt();
    public static SetNihgt OnCycleChangesToNight;
    public delegate int GetDiceRollNumber();
    public static GetDiceRollNumber OnDiceRollNumberChanged;

    public List<string> paths;
    public int RollNumberForDay = 3;
    public int RollNumberForNight = 2;
    public int diceRollNumber;
    public bool IsDay = true;

    DataFileController fileController = new DataFileController();
    private Sprite[] sheets;
    private void OnEnable()
    {
        DiceController.OnRollDice += AddRollNumber;
        CityController.OnEnterCity += ChangeDay;
    }
    private void OnDisable()
    {
        DiceController.OnRollDice -= AddRollNumber;
        CityController.OnEnterCity -= ChangeDay;
    }
    void Start()
    {        

    }
    public void AddRollNumber(int value)
    {
        diceRollNumber++;

        if (diceRollNumber >= RollNumberForDay && IsDay)
        {
            ChangeNight();
        }            
        else if(diceRollNumber >= RollNumberForNight && !IsDay)
        {
            ChangeDay();            
        }            
    }
    public void ChangeNight()
    {
        diceRollNumber = 0;
        IsDay = false;
        
        OnCycleChangesToNight?.Invoke();
    }
    public void ChangeDay()
    {
        diceRollNumber = 0;
        IsDay = true;

        OnCycleChangesToDay?.Invoke();
    }
    

}
