using System;
[Serializable]
public class PlayerStatsModel : StatsModel
{    
    public int experience = 1;  
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
[Serializable]
public class StatsModel
{
    public string id = "none";
    public int level = 1;
    public float currentHealth = 25;
    public float maxHealth = 25;
}