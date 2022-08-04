using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossQuest : MonoBehaviour
{
    public delegate void DamageThePlayer(float damage);
    public static DamageThePlayer OnDamageThePlayer;
    public delegate void BattleStart();
    public static BattleStart OnBattleStart;
    public delegate void End();
    public static End OnBattleEnd;


    public Transform Content;
    public GameObject Bosses;        
    public PlayerStats playerStats;
    public GameObject prefabDropTemplate;
    public Animator animator;    
    public int itenDropCount = 1;
    public int ExpForEnemy = 500;    
    public string DataFile = "inventory";
    public string IdQuest = "Boss";

    private bool inBattle = false;
    private Transform CameraTranform;
    private GameObject[] goEnemies;
    private int currentGridIndex;
    private int currentBoss;
    private List<EnemyModel> bossesData;
    private float bossTimerAttack;
    private float bossAttackSpeed;
    private float timer;
    DataFileController fileController = new DataFileController();
    private EnemiesBarHealth enemiesBarHealth;    
    private LevelSystem levelSystem;    
    
    void Start()
    {        
        SetParent();        
        playerStats = GetScript.Type<PlayerStats>("Player");
        DesactivePanel();
        goEnemies = ChildrenController.GetChildren(Bosses);
        enemiesBarHealth = GetComponent<EnemiesBarHealth>();
    }
    public void UpdateKingdom()
    {
        
    }
    private void OnEnable()
    {
        LevelController.StartLevelSystem += SetLevelSystem;
    }
    private void OnDisable()
    {
        LevelController.StartLevelSystem -= SetLevelSystem;
    }
    public void SetLevelSystem(LevelSystem levelSystem)
    {
        this.levelSystem = levelSystem;
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
    public void StartBattle(int idBoss)
    {        
        ActivePanel();
        SetBossQuest(idBoss);
        OnBattleStart?.Invoke();
    }
    public void InBattle()
    {
        inBattle = true;
    }
    void SetBossQuest(int IdBoss)
    {   
        bossesData = fileController.GetEncryptedData<List<EnemyModel>>(PathHelper.BossesDataFile);        
        for (int x = 0; x < goEnemies.Length; x++)
        {
            goEnemies[x].SetActive(false);
        }
        goEnemies[IdBoss-1].SetActive(true);
        enemiesBarHealth.StartBars();
        enemiesBarHealth.AddMaxHealthBoss(bossesData[IdBoss-1].health);
        bossAttackSpeed = bossesData[IdBoss-1].attackSpeed;
        bossTimerAttack = 0f;
        currentBoss = IdBoss-1;
    }
    void Battle()
    {
        timer += Time.deltaTime;
        if (bossesData[currentBoss].health > 0)
            if (CanAttack(ref bossTimerAttack, bossAttackSpeed))
            {
                BossAttack();
            }

        if (playerStats.stats.currentHealth > 0)
            if (CanAttack(ref playerStats.stats.equipment.attackSpeedTimer, playerStats.stats.equipment.attackSpeed))
            {
                PlayerAttack();
            }

        if (bossesData[currentBoss].health <= 0)
        {
            levelSystem.AddExperience(ExpForEnemy * bossesData[currentBoss].level);
            RewardDrop(bossesData[currentBoss].level);
            goEnemies[currentBoss].SetActive(false);
            QuestHelper.CompletedBossQuests(IdQuest, currentBoss);
            DesactivePanel();
        }

        if (playerStats.stats.currentHealth <= 0)
            DesactivePanel();
    }
    void RewardDrop(int level)
    {
        GameObject go = Instantiate(prefabDropTemplate);
        RewardModel rf = QuestHelper.GetRewardQuests(IdQuest, currentBoss);
        Drop reward = go.GetComponent<Drop>();
        reward.item = rf.reward;
        Vector3 playerPos = PlayerDataHelper.GetVectorPosition();
        go.transform.position = new Vector3(playerPos.x + 1, playerPos.y, playerPos.z);
    }
    void BossAttack()
    {
        float d = playerStats.stats.equipment.armor;
        float a = bossesData[currentBoss].damage;

        float damage = IdleBattleHelper.GetRealDamage(d, a);

        PlayerDataHelper.RestHealth(damage);
        playerStats.stats.currentHealth = PlayerDataHelper.GetCurrentHealth();

        OnDamageThePlayer?.Invoke(damage);
    }
    void PlayerAttack()
    {
        float d = bossesData[currentBoss].defending;
        float a = playerStats.stats.equipment.damage;

        float damage = IdleBattleHelper.GetRealDamage(d, a);

        bossesData[currentBoss].health -= damage;
        enemiesBarHealth.UpdateHealthBoss(currentBoss, bossesData[currentBoss].health);
    }
    private void FixedUpdate()
    {
        if (inBattle)
            Battle();
    }
    void EndBattle()
    {
        OnBattleEnd?.Invoke();
        //animator.SetBool("Close", true);
        inBattle = false;
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
