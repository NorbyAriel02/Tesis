using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem 
{
    public event EventHandler OnExperienceChanged;
    public event EventHandler OnLevelChanged;
    private int level;
    private int experience;
    private int experienceToNextLevel;

    public LevelSystem(int level, int experience, int experienceToNextLevel)
    {
        this.level = level;
        this.experience = experience;
        this.experienceToNextLevel = experienceToNextLevel;
    }

    public void AddExperience(int amount)
    {
        experience += amount;
        while(experience >= experienceToNextLevel)
        {
            level++;
            experience -= experienceToNextLevel;
            if (OnLevelChanged != null) OnLevelChanged(this, EventArgs.Empty);
        }

        if (OnExperienceChanged != null) OnExperienceChanged(this, EventArgs.Empty);
    }

    public int LevelNumber
    {
        get {
            return level;
        }        
    }
    public int Experience
    {
        get
        {
            return experience;
        }
    }

    public int ExperienceToNextLevel
    {
        get
        {
            return experienceToNextLevel;
        }

        set
        {
            experienceToNextLevel = value;
        }
    }
    public float ExperienceNormalized
    {
        get {
            return (float)experience / experienceToNextLevel;
        }        
    }
}
