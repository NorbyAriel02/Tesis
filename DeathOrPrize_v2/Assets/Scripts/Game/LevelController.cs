using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public delegate void SetLevelSystem(LevelSystem levelSystem);
    public static SetLevelSystem StartLevelSystem;
    public delegate void LevelChange();
    public static LevelChange OnLevelChange;
    public float deltaHealth = 10;
    public List<int> ExpForLevel;
    private PlayerStatsModel stats;
    private LevelSystem levelSystem;
    
    void Start()
    {        
        
    }
    void SetStart()
    {
        stats = PlayerDataHelper.GetStats();
        if (stats != null)
            levelSystem = new LevelSystem(stats.level, stats.experience, ExpForLevel[stats.level]);
        else
            levelSystem = new LevelSystem(1, 0, ExpForLevel[1]);

        StartLevelSystem?.Invoke(levelSystem);
    }
    private void OnEnable()
    {
        SetStart();
        this.levelSystem.OnLevelChanged += ChangeLevel;
        this.levelSystem.OnExperienceChanged += ChangeExp;
    }
    private void OnDisable()
    {
        this.levelSystem.OnLevelChanged -= ChangeLevel;
        this.levelSystem.OnExperienceChanged -= ChangeExp;
    }
    private void OnDestroy()
    {
        DataHelper.SaveExp(stats);
    }
    void ChangeExp(object sender, System.EventArgs e)
    {
        stats.experience = levelSystem.Experience;
    }
    void ChangeLevel(object sender, System.EventArgs e)
    {
        stats = DataHelper.GetStats();
        stats.level = levelSystem.LevelNumber;
        stats.maxHealth += deltaHealth;
        stats.currentHealth = stats.maxHealth;
        DataHelper.SaveStats(stats);
        OnLevelChange?.Invoke();
    }
}
