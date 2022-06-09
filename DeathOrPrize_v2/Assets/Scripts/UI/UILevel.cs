using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UILevel : MonoBehaviour
{
    public Text textLevel;
    public Image experienceBarImage;
    private LevelSystem levelSystem;
    private void OnEnable()
    {
        LevelController.StartLevelSystem += SetLevelSystem;
    }
    private void OnDisable()
    {
        LevelController.StartLevelSystem -= SetLevelSystem;
    }
    private void SetExperienceBarSize(float experienceNormalized)
    {
        experienceBarImage.fillAmount = experienceNormalized;
    }

    private void SetLevelNumber(int level)
    {
        textLevel.text = "Level " + level;
    }

    public void SetLevelSystem(LevelSystem levelSystem)
    {
        this.levelSystem = levelSystem;

        SetLevelNumber(this.levelSystem.LevelNumber);
        SetExperienceBarSize(this.levelSystem.ExperienceNormalized);

        this.levelSystem.OnLevelChanged += LevelSystem_OnLevelChanged;
        this.levelSystem.OnExperienceChanged += LevelSystem_OnExperienceChanged;
    }

    private void LevelSystem_OnExperienceChanged(object sender, System.EventArgs e)
    {
        SetExperienceBarSize(levelSystem.ExperienceNormalized);
    }
    private void LevelSystem_OnLevelChanged(object sender, System.EventArgs e)
    {
        SetLevelNumber(levelSystem.LevelNumber);
    }
}
