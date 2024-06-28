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
    [SerializeField] private float attackDamageBase;
    [SerializeField] private float attackDamagePerLevel;
    [SerializeField] private float attackSpeedBase;
    [SerializeField] private float attackSpeedPerLevel;
    [SerializeField] private float abilityHasteBase;
    [SerializeField] private float abilityHastePerLevel;
    [SerializeField] private float abilityPowerBase;
    [SerializeField] private float abilityPowerPerLevel;

    private float scaleBase = 1f;
    private float scalePerLevel = 0.1f;


    [Header("public ReadOnly")]

    public float healthFinal;
    public float attackDamageFinal;
    public float attackSpeedFinal;
    public float abilityHasteFinal;
    public float abilityPowerFinal;
    public int level; //property
    public int experience;
    private int xpToNextLevel;
    private int maxLevel = 10;
    
    public float scaleFinal;
    public float healthBaseBonus;
    public float healthPercentBonus;
    public float attackDamageBaseBonus;
    public float attackDamagePercentBonus;
    public float attackSpeedBaseBonus;
    public float attackSpeedPercentBonus;
    public float abilityHasteBaseBonus;
    public float abilityHastePercentBonus;
    public float abilityPowerBaseBonus;
    public float abilityPowerPercentBonus;


    [Header("Components")]
    private Health health;

    void Awake()
    {
        health = GetComponent<Health>();
    }

    void Start()
    {
        //level = 0;
        LevelUp();
        experience = 0;
        ResetBonusStats();
        xpToNextLevel = requiredXPBase;
        UIManager.Instance.UpdateExperience(experience, xpToNextLevel);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            LevelUp();
        }
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
        
        if (level > 1 && UpgradeManager.Instance.upgradesRemaining >= 0) { UpgradeManager.Instance.ShowCanvas(); }
    }

    private void RecalculateStats()
    {
        scaleFinal = CalculateFinalStat(scaleBase, scalePerLevel, 0f,0f);
        transform.localScale = new Vector3(scaleFinal, scaleFinal, 1f);
        healthFinal = CalculateFinalStat(healthBase, healthPerLevel, healthBaseBonus, healthPercentBonus);
        health.SetNewMaxHealth(healthFinal);
        attackDamageFinal = CalculateFinalStat(attackDamageBase, attackDamagePerLevel, attackDamageBaseBonus, attackDamagePercentBonus);
        attackSpeedFinal = CalculateFinalStat(attackSpeedBase, attackSpeedPerLevel, attackSpeedBaseBonus, attackSpeedPercentBonus);
        abilityHasteFinal = CalculateFinalStat(abilityHasteBase, abilityHastePerLevel, abilityHasteBaseBonus, abilityHastePercentBonus);
        abilityPowerFinal = CalculateFinalStat(abilityPowerBase, abilityPowerPerLevel, abilityPowerBaseBonus, abilityPowerPercentBonus);
    }

    public void AddStats(StatsSO statBonus)
    {
        healthBaseBonus += statBonus.healthBaseBonus;
        attackDamageBaseBonus += statBonus.attackDamageBaseBonus;
        attackSpeedBaseBonus += statBonus.attackSpeedBaseBonus;
        abilityHasteBaseBonus += statBonus.abilityHasteBaseBonus;
        abilityPowerBaseBonus += statBonus.abilityPowerBaseBonus;
        healthPercentBonus += statBonus.healthPercentBonus;
        attackDamagePercentBonus += statBonus.attackDamagePercentBonus;
        attackSpeedPercentBonus += statBonus.attackSpeedPercentBonus;
        abilityHastePercentBonus += statBonus.abilityHastePercentBonus;
        abilityPowerPercentBonus += statBonus.abilityPowerPercentBonus;
        RecalculateStats();
    }

    private float CalculateFinalStat(float baseStat, float perlevelStat, float baseBonus, float percentBonus)
    {
        return ((1f + 0.01f * percentBonus) * (baseStat + (level-1) * perlevelStat + baseBonus));
    }

    private void ResetBonusStats()
    {
        healthBaseBonus = 0f;
        attackDamageBaseBonus = 0f;
        attackSpeedBaseBonus = 0f;
        abilityHasteBaseBonus = 0f;
        abilityPowerBaseBonus = 0f;
        healthPercentBonus = 0f;
        attackDamagePercentBonus = 0f;
        attackSpeedPercentBonus = 0f;
        abilityHastePercentBonus = 0f;
        abilityPowerPercentBonus = 0f;
    }

}
