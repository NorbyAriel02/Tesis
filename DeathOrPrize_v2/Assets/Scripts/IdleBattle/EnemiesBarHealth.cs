using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesBarHealth : MonoBehaviour
{
    public List<Image> health;
    public List<float> maxHealth;
    private void Start()
    {
        maxHealth = new List<float>();
    }
    public void StartBars()
    {
        maxHealth = new List<float>();
    }
    public void AddMaxHealth(float value)
    {
        maxHealth.Add(value);
        health[maxHealth.Count - 1].fillAmount = 1;
    }

    public void UpdateHealth(int index, float currentHealth)
    {
        health[index].fillAmount = currentHealth / maxHealth[index];
    }
}
