using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBattleHelper 
{
    static float oneSeg;
    public static float GetRealDamage(float defending, float damage)
    {
        float multi = 100 / (100 + defending);        
        return damage * multi;
    }

    public static float GetAttackSpeed(int level, float damage)
    {
        float multi = 1 / level;
        return damage * multi;
    }
}
