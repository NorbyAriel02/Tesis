using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDayNight : MonoBehaviour
{
    public int rollValue = 1;
    public DayNightCicle dayNightCicle;
    public Image imgDayNight;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void ResetDay()
    {
        dayNightCicle.SetDay();        
        SetImg();
    }
    public void AddRoll()
    {
        dayNightCicle.AddRollNumber(rollValue);
        SetImg();
    }

    void SetImg()
    {
        imgDayNight.sprite = dayNightCicle.GetSpriteDayNight(dayNightCicle.diceRollNumber);
    }
}
