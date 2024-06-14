using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [Header("Seerice Field")]
    
    [SerializeField] private int requiredXPBase;
    [SerializeField] private int requiredXPLevelMultiplier;
    [SerializeField] private float healthBase;
    [SerializeField] private float healthPerLevel;

    [Header("public ReadOnly")]
    public float healthFinal;
    public int level; //property
    public int experience;
    private int xpToNextLevel;
    private int maxLevel = 10;

    [Header("Components")]
    private Health health;

    void Awake()
    {
        health = GetComponent<Health>();
    }

    void Start()
    {
        level = 0;
        LevelUp();
        experience = 0;
        xpToNextLevel = requiredXPBase;
        UIManager.Instance.UpdateExperience(experience, xpToNextLevel);
    }

    public void AddExperience(int xpGained)
    {

        experience += xpGained;
        if (experience >= xpToNextLevel && level < maxLevel)
        {
            experience -= xpToNextLevel;
            LevelUp();
        }
        UIManager.Instance.UpdateExperience(experience, xpToNextLevel);
    }

    private void LevelUp()
    {
        level++;
        xpToNextLevel += level * requiredXPLevelMultiplier;
        RecalculateStats();
        Debug.Log("level up!");
    }

    public void RecalculateStats()
    {
        healthFinal = CalculateFinalStat(healthBase, healthPerLevel);
        health.SetNewMaxHealth(healthFinal);
        
    }

    private float CalculateFinalStat(float baseStat, float perlevelStat, float baseBonus = 0f, float percentBonus = 0f)
    {
        return ((1f + 0.01f * percentBonus) * (baseStat + level * perlevelStat + baseBonus));
    }


}
