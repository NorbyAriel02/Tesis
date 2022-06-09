using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public delegate void SetLevelSystem(LevelSystem levelSystem);
    public static SetLevelSystem StartLevelSystem;
    public List<int> ExpForLevel;
    private PlayerStatsModel stats;
    private LevelSystem levelSystem;
    
    void Start()
    {
        
        stats = PlayerDataHelper.GetStats();
        if (stats != null)
            levelSystem = new LevelSystem(stats.level, stats.experience, ExpForLevel[stats.level]);
        else
            levelSystem = new LevelSystem(1, 0, ExpForLevel[1]);

        StartLevelSystem?.Invoke(levelSystem);
    }

    
}
