using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    public delegate void Timer(float timer, float attackspeed);
    public Timer OnTimerChange;
    public delegate void ExecuteAttack(Attacks attacks);
    public static ExecuteAttack OnAttack;

    public Attacks attacks;    
    
    private void Start()
    {
        attacks = new Attacks();
    }
    public virtual void Attack()
    {
    }
    public virtual void AttackFail()
    {        
        Debug.Log("Fallo");
    }
    public virtual void AssignTarget(string idTaget)
    {        
        this.attacks.Target = idTaget;
    }
    public virtual void TargetOff()
    {
        this.attacks.Target = null;
    }
}
