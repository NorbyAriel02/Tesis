using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossQuest : MonoBehaviour
{
    public HUDController HUB;
    public Transform Content;
    public GameObject Bosses;    
    public PlayerStats playerStats;
    public GameObject prefabDropTemplate;
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
    void Start()
    {        
        SetParent();        
        playerStats = GetScript.Type<PlayerStats>("Player");
        DesactivePanel();
        goEnemies = ChildrenController.GetChildren(Bosses);
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
    public void StartBattle(int idBoss)
    {
        SetBossQuest(idBoss);
        playerStats.SetStats();
        ActivePanel();
        inBattle = true;
    }
    void SetBossQuest(int IdBoss)
    {   
        bossesData = fileController.GetEncryptedData<List<EnemyModel>>(PathHelper.BossesDataFile);        
        for (int x = 0; x < goEnemies.Length; x++)
        {
            goEnemies[x].SetActive(false);
        }
        goEnemies[IdBoss].SetActive(true);
        enemiesBarHealth.StartBars();
        enemiesBarHealth.AddMaxHealth(bossesData[IdBoss].health);
        bossAttackSpeed = bossesData[IdBoss].attackSpeed;
        bossTimerAttack = 0f;
        currentBoss = IdBoss;
    }
    void Battle()
    {
        timer += Time.deltaTime;
        if (bossesData[currentBoss].health > 0)
            if (CanAttack(ref bossTimerAttack, bossAttackSpeed))
            {
                BossAttack();
                HUB.UpdateBarHealth();
            }

        if (playerStats.stats.currentHealth > 0)
            if (CanAttack(ref playerStats.attackSpeedTimer, playerStats.attackSpeed))
            {
                PlayerAttack();
            }

        if (bossesData[currentBoss].health <= 0)
        {
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
        float d = playerStats.stats.defending;
        float a = bossesData[currentBoss].damage;

        float damage = IdleBattleHelper.GetRealDamage(d, a);

        PlayerDataHelper.RestHealth(damage);

        playerStats.stats.currentHealth = PlayerDataHelper.GetCurrentHealth();        
    }
    void PlayerAttack()
    {
        float d = bossesData[currentBoss].defending;
        float a = playerStats.stats.damage;

        float damage = IdleBattleHelper.GetRealDamage(d, a);

        bossesData[currentBoss].health -= damage;
        enemiesBarHealth.UpdateHealth(currentBoss, bossesData[currentGridIndex].health);
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
