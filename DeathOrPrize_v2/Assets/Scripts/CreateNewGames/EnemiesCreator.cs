using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesCreator : MonoBehaviour
{
    public List<EnemiesXcellModel> enemiesXcell;
    public WorldCreator worldCreator;
    public int sizeZone = 5;
    public float baseAttackSpeed = 5;
    public float balanceAttackSpeed = 0.5f;
    public float baseDamage = 5;
    public float balanceDamage = 2;
    public float baseDefending = 5;
    public float balanceDefending = 2;
    public float baseHealth = 25;
    public float balanceHealth = 2;
    private int numberOfKingdom = 9;
    private int sizeKingdom = 55;
    private int[,] matrizDifficulty;
    DataFileController fileController = new DataFileController();
    void Start()
    {
        numberOfKingdom = worldCreator.numberOfKingdom;
        sizeKingdom = worldCreator.sizeKingdom;
        matrizDifficulty = GridDifficulty(sizeKingdom, sizeKingdom, GetMaxDificulty(sizeKingdom, sizeZone));
        Create();
    }
    void Create()
    {
        for (int x = 0; x < numberOfKingdom; x++)
        {
            CreateEnemyKingdom(x);
        }
    }
    void CreateEnemyKingdom(int idKingdom)
    {
        enemiesXcell = GetDataEnemiesKingdom(idKingdom);
        string path = PathHelper.EnemiesDataFile(idKingdom);

        fileController.Save<List<EnemiesXcellModel>>(enemiesXcell, path);
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
        switch (matrizDifficulty[x, y])
        {
            case 0:
            case 1:
                enemiesCount = Random.Range(1, 2);
                break;
            case 2:
            case 3:
                enemiesCount = Random.Range(3, 4);
                break;
            case 4:
            case 5:
                enemiesCount = Random.Range(4, 5);
                break;
            case 6:
            case 7:
                enemiesCount = 6;
                break;
        }
        for (int j = 0; j < enemiesCount; j++)
        {
            EnemiesType type = new EnemiesType();
            type.id = 1;
            type.type = "Default";
            EnemyModel enemyModel = new EnemyModel();
            enemyModel.type = type;
            enemyModel = AssignDiffisulted(enemyModel, x, y, idkindom);
            enemies.Add(enemyModel);
        }
        return enemies;
    }
    EnemyModel AssignDiffisulted(EnemyModel enemy, int x, int y, int idKindom)
    {
        enemy.difficultyIndex = matrizDifficulty[x, y];
        enemy.health = baseHealth + (enemy.difficultyIndex + idKindom) * balanceHealth;
        enemy.defending = baseDefending + (enemy.difficultyIndex + idKindom) * balanceDefending;
        enemy.attackSpeed = baseAttackSpeed - (enemy.difficultyIndex + idKindom) * balanceAttackSpeed;
        enemy.damage = baseDamage + (enemy.difficultyIndex + idKindom) * baseDamage;

        return enemy;
    }

    int[,] GridDifficulty(int columnas, int filas, int maxDiff)
    {
        bool derecha = true, izquierda = false, abajo = false;
        int[,] matrizc = new int[filas, columnas];
        int x = 0, y = -1;
        int zoneConut = 0;
        for (int k = 1; k <= columnas * filas; k++)
        {
            if (izquierda)
            {
                y--;
                if (y == -1)
                {
                    y = 0; x--;
                    izquierda = false;
                }
                else if (matrizc[x, y] != 0)
                {
                    y++; x--;
                    izquierda = false;
                }
            }
            else if (derecha)
            {
                y++;
                if (y == columnas)
                {
                    y = columnas - 1; x++;
                    derecha = false;
                    abajo = true;
                }
                else if (matrizc[x, y] != 0)
                {
                    y--; x++;
                    derecha = false;
                    abajo = true;
                }
                if (!derecha)
                    zoneConut++;
            }
            else if (abajo)
            {
                x++;
                if (x == filas)
                {
                    x = filas - 1; y--;
                    abajo = false;
                    izquierda = true;
                }
                else if (matrizc[x, y] != 0)
                {
                    y--; x--;
                    abajo = false;
                    izquierda = true;
                }
            }
            else
            {
                x--;
                if (x == -1 || matrizc[x, y] != 0)
                {
                    x++; y++;
                    derecha = true;
                }
            }
            if (zoneConut >= sizeZone)
            {
                zoneConut = 0;
                maxDiff--;
            }
            matrizc[x, y] = maxDiff;
        }
        return matrizc;
    }
    int GetMaxDificulty(int sizeGrid, int sizeZone)
    {
        return System.Convert.ToInt32(System.Math.Floor((decimal)((sizeGrid / 2) / sizeZone)));
    }
}
