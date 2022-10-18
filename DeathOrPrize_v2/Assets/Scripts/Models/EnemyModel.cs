using System;
[Serializable]
public class EnemyModel 
{
    public EnemiesType type;
    public int x;
    public int y;
    public float attackSpeed = 5;
    public float damage = 5;
    public float armor = 5;
    public float currentHealth = 25;
    public float maxHealth = 25;
    public int level = 1;
}
