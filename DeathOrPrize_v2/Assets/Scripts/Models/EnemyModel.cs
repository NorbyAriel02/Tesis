using System;
[Serializable]
public class EnemyModel 
{
    public EnemiesType type;
    public int x;
    public int y;
    public float attackSpeed = 5;
    public float damage = 5;
    public float defending = 5;
    public float health = 25;
    public int level = 1;
}
