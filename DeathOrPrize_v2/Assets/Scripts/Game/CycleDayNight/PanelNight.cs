using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelNight : MonoBehaviour
{
    public GameObject panel;
    private void OnEnable()
    {
        DayNightCicle.OnCycleChangesToDay += SetDay;
        DayNightCicle.OnCycleChangesToNight += SetNight;
    }
    private void OnDisable()
    {
        DayNightCicle.OnCycleChangesToDay -= SetDay;
        DayNightCicle.OnCycleChangesToNight -= SetNight;
    }

    void SetDay()
    {
        panel.SetActive(false);
    }

    void SetNight()
    {
        panel.SetActive(true);
    }
}
