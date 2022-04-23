using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBossFile : MonoBehaviour
{
    public bool forceCreation;
    public int numberBosses;
    public float health;
    public int damage;
    public int defending;
    void Start()
    {
        DataFileController df = new DataFileController();
        if(forceCreation || !df.Exists(PathHelper.BossesDataFile))
            GenerateBossesFile();
    }
        
    public void GenerateBossesFile()
    {
        List<EnemyModel> bosses = new List<EnemyModel>();
        for(int index = 1; index <= numberBosses; index++)
        {
            EnemyModel boss = new EnemyModel();
            boss.level = index;
            boss.health = health * index;
            boss.damage = damage * index;
            boss.defending = defending * index;
            boss.attackSpeed = Utilitis.AttackSpeed(index);
            bosses.Add(boss);
        }
        DataFileController df = new DataFileController();
        df.SaveEncrypted<List<EnemyModel>>(bosses, PathHelper.BossesDataFile);
    }

}
