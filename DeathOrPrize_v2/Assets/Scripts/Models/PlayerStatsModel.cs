using System;
[Serializable]
public class PlayerStatsModel 
{
    public int level = 1;
    public int experience = 1;     
    public float currentHealth = 25;
    public float maxHealth = 25;
    public Equipment equipment;
}
[Serializable]
public class Equipment
{
    public float attackSpeed = 0.01f;
    public float attackSpeedTimer = 0;
    public float damage = 1;
    public float armor = 1;
}
