using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBattleManager : MonoBehaviour
{
    public HUDController HUB;
    public Transform Content;
    public GameObject Enemies;    
    public PlayerStats playerStats;
    public GameObject prefabDropTemplate;    
    private bool inBattle = false;
    private Transform CameraTranform;
    private GameObject[] goEnemies;
    private int  currentGridIndex;
    private int currentEnemy;
    private List<EnemiesXcellModel> enemiesXcell;
    private List<float> enemiesTimerAttack;
    private List<float> enemiesAttackSpeed;
    private float timer;
    DataFileController fileController = new DataFileController();
    private EnemiesBarHealth enemiesBarHealth;
    void Start()
    {   
        SetParent();       
        playerStats = GetScript.Type<PlayerStats>("Player");
        DesactivePanel();
        goEnemies = ChildrenController.GetChildren(Enemies);
        enemiesBarHealth = GetComponent<EnemiesBarHealth>();
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
    public void StartBattle(int indexCell)
    {
        SetEnemies(indexCell);
        playerStats.SetStats();          
        ActivePanel();
        inBattle = true;
    }
    void SetEnemies(int index)
    {
        int kingdom = PlayerDataHelper.GetIdKingdom();
        currentGridIndex = index;
        enemiesXcell = fileController.GetEncryptedData<List<EnemiesXcellModel>>(PathHelper.EnemiesDataFile(kingdom));
        enemiesAttackSpeed = new List<float>();
        enemiesTimerAttack = new List<float>();
        for (int x = 0; x < goEnemies.Length; x++)
        {
            goEnemies[x].SetActive(false);
        }
        enemiesBarHealth.StartBars();
        for (int x = 0; x < enemiesXcell[index].enemies.Count; x++)
        {
            goEnemies[x].SetActive(true);
            enemiesBarHealth.AddMaxHealth(enemiesXcell[index].enemies[x].health);
            enemiesAttackSpeed.Add(enemiesXcell[index].enemies[x].attackSpeed);
            enemiesTimerAttack.Add(0f);
        }
        currentEnemy = 0;
    }
    void Battle()
    {
        timer += Time.deltaTime;
        int i = currentGridIndex;
        int index = 0;
        foreach(EnemyModel enemy in enemiesXcell[i].enemies)
        {
            float eTimerAttack = enemiesTimerAttack[index];
            if (enemy.health > 0)
                if (CanAttack(ref eTimerAttack, enemiesAttackSpeed[index]))
                {
                    EnemyAttack(index);
                    HUB.UpdateBarHealth();
                }

            enemiesTimerAttack[index] = eTimerAttack;
            
            index++;
        }

        if (playerStats.stats.currentHealth > 0)
            if (CanAttack(ref playerStats.attackSpeedTimer, playerStats.attackSpeed))
            {
                PlayerAttack();
            }
         
        if(enemiesXcell[i].enemies[currentEnemy].health <= 0)
        {
            goEnemies[currentEnemy].SetActive(false);
            currentEnemy++;            
        }

        if (currentEnemy >= enemiesXcell[i].enemies.Count)
        {
            RewardDrop(enemiesXcell[0].enemies[0].level);
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
        Vector3 playerPos = PlayerDataHelper.GetVectorPosition();
        go.transform.position = new Vector3(playerPos.x + 1, playerPos.y, playerPos.z);
    }    
    void EnemyAttack(int index)
    {
        float d = playerStats.stats.defending;
        float a = enemiesXcell[currentGridIndex].enemies[index].damage;        

        float damage = IdleBattleHelper.GetRealDamage(d, a);

        PlayerDataHelper.RestHealth(damage);

        playerStats.stats.currentHealth = PlayerDataHelper.GetCurrentHealth();        
    }
    void PlayerAttack()
    {
        float d = enemiesXcell[currentGridIndex].enemies[currentEnemy].defending;
        float a = playerStats.stats.damage;
        
        float damage = IdleBattleHelper.GetRealDamage(d, a);

        enemiesXcell[currentGridIndex].enemies[currentEnemy].health -= damage;
        enemiesBarHealth.UpdateHealth(currentEnemy, enemiesXcell[currentGridIndex].enemies[currentEnemy].health);
    }
    private void FixedUpdate()
    {
        if (inBattle)
            Battle();
    }
    bool CanAttack(ref float attackSpeedTimer, float speed)
    {
        if (PassASeg())
        {
            attackSpeedTimer += speed;
        }

        if (attackSpeedTimer >= 1)
        {
            attackSpeedTimer = 0;
            return true;
        }
        return false;
    }
    bool PassASeg()
    {
        timer += Time.deltaTime;

        if (timer >= 0.5f)
        {
            timer = 0;
            return true;
        }
        return false;
    }
}
