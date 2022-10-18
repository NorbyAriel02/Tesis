using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DOP_AttackController : AttackController
{
    public bool inBattle = false;
    public float timer;
    public float attackSpeed;
    public float Damage;
    public override void Attack()
    {
        if (attacks.Target != null)
        {   
            OnAttack?.Invoke(this.attacks);
        }
        else
            AttackFail();
    }
    private void FixedUpdate()
    {
        if (inBattle)
            Battle();
    }
    void Battle()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            timer = attackSpeed;
            this.Attack();
        }

        OnTimerChange?.Invoke(timer, attackSpeed);
    }
}
