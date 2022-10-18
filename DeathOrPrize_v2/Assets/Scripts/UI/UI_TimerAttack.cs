using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_TimerAttack : MonoBehaviour
{
    public Image imgTimer;
    private void OnEnable()
    {
        AttackController ac = GetComponent<AttackController>();
        ac.OnTimerChange += ChangeTimer;
    }
    private void OnDisable()
    {
        AttackController ac = GetComponent<AttackController>();
        ac.OnTimerChange -= ChangeTimer;
    }
    void ChangeTimer(float timer, float attackSpeed)
    {
        imgTimer.fillAmount = timer / attackSpeed;
    }
}
