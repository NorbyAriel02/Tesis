using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesCreator : MonoBehaviour
{
    public List<EnemiesXcellModel> enemiesXcell;
    public List<BalanceModel> Balance;
    public WorldCreator worldCreator;
    public int incrementoNivel = 1;
    public int sizeZone = 5;
    private int numberOfKingdom = 9;
    private int sizeKingdom = 55;
    
    DataFileController fileController = new DataFileController();
    void Start()
    {
        numberOfKingdom = worldCreator.numberOfKingdom;
        sizeKingdom = worldCreator.sizeKingdom;        
        Create();
    }
    void Create()
    {
        for (int x = 1; x <= numberOfKingdom; x++)
        {
            CreateEnemyKingdom(x);
        }
    }
    void CreateEnemyKingdom(int idKingdom)
    {
        enemiesXcell = GetDataEnemiesKingdom(idKingdom);
        string path = PathHelper.EnemiesDataFile(idKingdom);

        fileController.SaveEncrypted<List<EnemiesXcellModel>>(enemiesXcell, path);
    }
    List<EnemiesXcellModel> GetDataEnemiesKingdom(int idKingdom)
    {
        enemiesXcell = new List<EnemiesXcellModel>();
        CreateEnemies(0, idKingdom);        
        return enemiesXcell;
    }
    void CreateEnemies(int biome, int idKingdom)
    {
        for (int x = 0; x < sizeKingdom; x++)
        {
            for (int y = 0; y < sizeKingdom; y++)
            {
                EnemiesXcellModel eXcell = new EnemiesXcellModel();
                eXcell.IDkingdom = idKingdom;
                eXcell.index = enemiesXcell.Count;
                eXcell.x = x;
                eXcell.y = y;
                List<EnemyModel> enemies = GetListEnemies(biome, x, y, idKingdom);
                eXcell.enemies = enemies;
                enemiesXcell.Add(eXcell);
            }
        }
    }
    List<EnemyModel> GetListEnemies(int biome, int x, int y, int idkindom)
    {
        List<EnemyModel> enemies = new List<EnemyModel>();
        int level = Random.Range(idkindom, (idkindom + incrementoNivel));
        int _type = -1;
        switch(biome)
        {
            case 0:
                _type = Random.Range(0, 3);
                break;
            case 1:
                _type = Random.Range(3, 6);
                break;
            case 2:
                _type = Random.Range(6, 9);
                break;
        }
        int enemiesCount = 1;
        switch (level)
        {
            case 0:
            case 1:
                enemiesCount = Random.Range(1, 3);
                break;
            case 2:
            case 3:
                enemiesCount = Random.Range(2, 4);
                break;
            case 4:
            case 5:
                enemiesCount = Random.Range(3, 5);
                break;
            case 6:
            case 7:
                enemiesCount = Random.Range(4, 6);
                break;
            default:
                enemiesCount = 6;
                break;
        }
        for (int j = 0; j < enemiesCount; j++)
        {
            EnemiesType type = new EnemiesType();
            type.id = idkindom;
            type.type = "Enemigo tipo " + _type;
            EnemyModel enemyModel = new EnemyModel();
            enemyModel.type = type;
            enemyModel = AssignDiffisulted(enemyModel, x, y, idkindom);
            enemies.Add(enemyModel);
        }
        return enemies;
    }
    EnemyModel AssignDiffisulted(EnemyModel enemy, int x, int y, int level)
    {
        enemy.level = level;
        enemy.health = Balance[0].baseValue + level * Balance[0].balanceValue;
        enemy.defending = Balance[1].baseValue + level * Balance[1].balanceValue;
        enemy.attackSpeed = Utilitis.AttackSpeed(level);
        enemy.damage = Balance[3].baseValue + level * Balance[3].balanceValue;

        return enemy;
    }
}
