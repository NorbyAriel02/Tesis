using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBattleManager : MonoBehaviour
{
    public delegate void DamageThePlayer(float damage);
    public static DamageThePlayer OnDamageThePlayer;
    public delegate void BattleStart();
    public static BattleStart OnBattleStart;
    public delegate void End();
    public static End OnBattleEnd;

    public Transform Content;
    public GameObject Enemies;    
    public PlayerStats playerStats;
    public GameObject prefabDropTemplate;
    public Animator animator;
    public int itenDropCount = 1;
    public int ExpForEnemy = 10;
    public string DataFile = "inventory";

    private bool inBattle = false;
    private Transform CameraTranform;
    private GameObject[] goEnemies;
    private int  currentGridIndex;
    private int currentEnemy;
    private List<EnemiesXcellModel> enemiesXcell;
    private List<float> enemiesTimerAttack;
    private List<float> enemiesAttackSpeed;
    private float timer;
    private LevelSystem levelSystem;
    DataFileController fileController = new DataFileController();
    private EnemiesBarHealth enemiesBarHealth;
    private SpriteEnemiesController spriteEC;
    void Start()
    {
        spriteEC = GetComponent<SpriteEnemiesController>();
        SetParent();       
        playerStats = GetScript.Type<PlayerStats>("Player");
        DesactivePanel();
        goEnemies = ChildrenController.GetChildren(Enemies);
        enemiesBarHealth = GetComponent<EnemiesBarHealth>();
        UpdateKingdom();
    }
    public void UpdateKingdom()
    {
        int kingdom = DataHelper.GetIdCurrentKingdom();
        enemiesXcell = fileController.GetEncryptedData<List<EnemiesXcellModel>>(PathHelper.EnemiesDataFile(kingdom));
    }
    private void OnEnable()
    {
        LevelController.StartLevelSystem += SetLevelSystem;
        LoadMaps.OnLoadMap += UpdateKingdom;
    }
    private void OnDisable()
    {
        LevelController.StartLevelSystem -= SetLevelSystem;
        LoadMaps.OnLoadMap += UpdateKingdom;
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
        animator.SetBool("Close", false);
    }
    void DesactivePanel()
    {        
        Content.gameObject.SetActive(false);
        inBattle = false; 
    }
    public void StartBattle(int indexCell)
    {
        ActivePanel();
        SetEnemies(indexCell);
        spriteEC.SetSprites(enemiesXcell[indexCell].enemies, goEnemies);
        OnBattleStart?.Invoke();
    }
    public void InBattle()
    {
        inBattle = true;        
    }
    void SetEnemies(int index)
    {
        currentGridIndex = index;
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
        foreach (EnemyModel enemy in enemiesXcell[i].enemies)
        {
            float eTimerAttack = enemiesTimerAttack[index];
            if (enemy.health > 0)
                if (CanAttack(ref eTimerAttack, enemiesAttackSpeed[index]))
                {
                    EnemyAttack(index);
                }

            enemiesTimerAttack[index] = eTimerAttack;

            index++;
        }

        if (playerStats.stats.currentHealth > 0)
            if (CanAttack(ref playerStats.stats.equipment.attackSpeedTimer, playerStats.stats.equipment.attackSpeed))
            {
                PlayerAttack();
            }

        if (enemiesXcell[i].enemies[currentEnemy].health <= 0)
        {
            levelSystem.AddExperience(ExpForEnemy * enemiesXcell[currentGridIndex].enemies[currentEnemy].level);
            goEnemies[currentEnemy].SetActive(false);
            currentEnemy++;
        }

        if (currentEnemy >= enemiesXcell[i].enemies.Count)
        {
            DropRewards(enemiesXcell[0].enemies[0].level);
            EndBattle();
        }

        if (playerStats.stats.currentHealth <= 0)
        {
            AkSoundEngine.PostEvent("Player_Dead", this.gameObject);            
            EndBattle();
        }
    }
    void EndBattle()
    {
        OnBattleEnd?.Invoke();
        animator.SetBool("Close", true);
        inBattle = false;
    }
    void DropRewards(int level)
    {
        for(int x = 0; x < itenDropCount; x++)
        {
            RewardDrop(level);
        }
    }
    void RewardDrop(int level)
    {
        GameObject go = Instantiate(prefabDropTemplate);
        Drop reward = go.GetComponent<Drop>();
        reward.item = Utilitis.GetRandomItem(level, Owner.player, DataFile);
        Vector3 playerPos = PlayerDataHelper.GetVectorPosition();
        go.transform.position = new Vector3(playerPos.x + 1, playerPos.y, playerPos.z);
    }    
    void EnemyAttack(int index)
    {        
        AkSoundEngine.PostEvent("Player_GetClawsDamage", this.gameObject);
        
        float d = playerStats.stats.equipment.armor;
        float a = enemiesXcell[currentGridIndex].enemies[index].damage;        

        float damage = IdleBattleHelper.GetRealDamage(d, a);

        PlayerDataHelper.RestHealth(damage);

        playerStats.stats.currentHealth = PlayerDataHelper.GetCurrentHealth();
        
        OnDamageThePlayer?.Invoke(damage);
    }
    void PlayerAttack()
    {        
        AkSoundEngine.PostEvent("Hit_Sword_Enemy", this.gameObject);
        AkSoundEngine.PostEvent("Pain_Bear", this.gameObject);
        float d = enemiesXcell[currentGridIndex].enemies[currentEnemy].defending;
        float a = playerStats.stats.equipment.damage;
        
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

    public void SetLevelSystem(LevelSystem levelSystem)
    {
        this.levelSystem = levelSystem;        
    }

}
