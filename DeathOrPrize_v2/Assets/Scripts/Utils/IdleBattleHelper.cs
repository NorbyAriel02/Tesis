using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBattleHelper 
{
    static float oneSeg;
    public static float GetRealDamage(float armor, float damage)
    {
        float multi = 100 / (100 + armor);        
        return damage * multi;
    }

    public static float GetAttackSpeed(int level, float damage)
    {
        float multi = 1 / level;
        return damage * multi;
    }
}
