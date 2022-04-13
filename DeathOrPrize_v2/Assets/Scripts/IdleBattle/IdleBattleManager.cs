using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBattleManager : MonoBehaviour
{
    public Inventory HUB;
    public float TimerTest = 3;
    public Transform Content;
    public GameObject Enemies;
    public PlayerMove playerMove;
    public PlayerPosition playerPosition;
    public PlayerStats playerStats;
    public GameObject prefabDropTemplate;
    //private bool flag = false;
    private bool inBattle = false;
    private Transform CameraTranform;
    private GameObject[] goEnemies;
    private int  currentGridIndex;
    private int currentEnemy;
    private List<EnemiesXcellModel> enemiesXcell;
    private List<float> enemiesTimerAttack;
    DataFileController fileController = new DataFileController();
    void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPosition>();
        SetParent();
        playerMove = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMove>();
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        DesactivePanel();
        goEnemies = ChildrenController.GetChildren(Enemies);
    }
    private void OnEnable()
    {
        
    }
    void SetParent()
    {
        CameraTranform = GameObject.FindGameObjectWithTag("MainCamera").transform;
        transform.SetParent(CameraTranform);
        transform.localPosition = new Vector3(0, 0, transform.localPosition.z);
    }
    void ActivePanel()
    {
        Content.gameObject.SetActive(true);
    }
    void DesactivePanel()
    {
        Content.gameObject.SetActive(false);
        inBattle = false; 
    }
    public void StartBattle(int indexCell, int kingdomID)
    {
        SetEnemies(indexCell, kingdomID);
        playerStats.SetStats();
        TimerTest = 6;       
        ActivePanel();
        inBattle = true;
    }
    void SetEnemies(int index, int kingdom)
    {
        currentGridIndex = index;
        enemiesXcell = fileController.GetData<List<EnemiesXcellModel>>(PathHelper.EnemiesDataFile(kingdom));
        enemiesTimerAttack = new List<float>();
        for (int x = 0; x < goEnemies.Length; x++)
        {
            goEnemies[x].SetActive(false);
        }
        for (int x = 0; x < enemiesXcell[index].enemies.Count; x++)
        {
            goEnemies[x].SetActive(true);
            enemiesTimerAttack.Add(enemiesXcell[index].enemies[x].attackSpeed);           
        }
        currentEnemy = 0;
    }
    void Battle()
    {
        int i = currentGridIndex;
        int index = 0;
        foreach(EnemyModel enemy in enemiesXcell[i].enemies)
        {
            if(enemy.health > 0)
                if (enemiesTimerAttack[index] <= 0)
                {
                    EnemyAttack(index);
                    HUB.UpdateBarHealth();
                }
                    

            enemiesTimerAttack[index] = enemiesTimerAttack[index] - Time.deltaTime;
            index++;
        }
        playerStats.attackSpeedTimer = playerStats.attackSpeedTimer - Time.deltaTime;
        if (playerStats.stats.currentHealth > 0)
            if (playerStats.attackSpeedTimer <= 0)
            {
                PlayerAttack();
            }
         
        if(enemiesXcell[i].enemies[currentEnemy].health <= 0)
        {
            currentEnemy++;
            goEnemies[currentEnemy].SetActive(false);
        }

        if (currentEnemy >= enemiesXcell[i].enemies.Count)
        {
            RewardDrop(enemiesXcell[0].enemies[0].difficultyIndex);
            DesactivePanel();
        }            

        if (playerStats.stats.currentHealth <= 0)
            DesactivePanel();
    }
    void RewardDrop(int level)
    {
        GameObject go = Instantiate(prefabDropTemplate);
        Drop reward = go.GetComponent<Drop>();
        reward.item = Utilitis.GetRandomItem(level, Owner.player);
        go.transform.position = new Vector3(playerPosition.transform.position.x + 1, playerPosition.transform.position.y, playerPosition.transform.position.z);
    }    
    void EnemyAttack(int index)
    {
        float d = playerStats.stats.defending;
        float a = enemiesXcell[currentGridIndex].enemies[index].damage;
        float h = playerStats.stats.currentHealth;

        if (a > d)
        {
            PlayerDataHelper.RestHealth((a - d));
            //playerStats.stats.currentHealth = h - (a - d);
        }
        else
        {
            PlayerDataHelper.RestHealth(1);
            //playerStats.stats.currentHealth = h - 1; 
        }


        enemiesTimerAttack[index] = enemiesXcell[currentGridIndex].enemies[index].attackSpeed;
    }
    void PlayerAttack()
    {
        float d = enemiesXcell[currentGridIndex].enemies[currentEnemy].defending;
        float a = playerStats.stats.damage;
        float h = enemiesXcell[currentGridIndex].enemies[currentEnemy].health;
        
        if(a > d)
            enemiesXcell[currentGridIndex].enemies[currentEnemy].health = h - (a - d);
        else
            enemiesXcell[currentGridIndex].enemies[currentEnemy].health = h - 1;

        playerStats.attackSpeedTimer = playerStats.stats.attackSpeed;
    }
    private void FixedUpdate()
    {
        if (inBattle)
            Battle();
    }
    void Update()
    {
        if (TimerTest < 0)
            DesactivePanel();
        
        //if (playerMove.diceValue > 0)
        //    flag = true;

        //TimerTest -= Time.deltaTime;        
    }
}
