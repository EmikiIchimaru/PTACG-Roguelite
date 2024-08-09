using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public static int maxLevel = 9;

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

    private float scaleBase = 0.95f;
    private float scalePerLevel = 0.05f;
    private float moveSpeedBase = 1f;
    private float moveSpeedPerLevel = 0f;


    [Header("public ReadOnly")]

    public float healthFinal;
    public float attackDamageFinal;
    public float attackSpeedFinal;
    public float abilityHasteFinal;
    public float abilityPowerFinal;
    public int level; //property
    public int experience;
    private int xpToNextLevel;
    public float scaleFinal;
    public float moveSpeedFinal;

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

    public float scaleBaseBonus;
    public float scalePercentScaling;
    public float moveSpeedBaseBonus;
    public float moveSpeedPercentScaling;



    [Header("Components")]
    private CharacterMovement characterMovement;
    private CharacterWeapon characterWeapon;
    private Health health;

    void Awake()
    {
        
        characterMovement = GetComponent<CharacterMovement>();
        characterWeapon = GetComponent<CharacterWeapon>();
        health = GetComponent<Health>();
    }

    void Start()
    {
        //level = 0;
        LevelUp();
        experience = 0;
        ResetBonusStats();
        xpToNextLevel = requiredXPBase + level * requiredXPLevelMultiplier;
        UIManager.Instance.UpdateExperience(experience, xpToNextLevel);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V) && GameManager.isCheatingAllowed)
        {
            AddExperience(50);
        }
    }

    public void AddExperience(int xpGained)
    {
        if (level >= maxLevel) { return; }
        experience += xpGained;
        if (experience >= xpToNextLevel)
        {
            experience -= xpToNextLevel;
            LevelUp();
        }
        UIManager.Instance.UpdateExperience(experience, xpToNextLevel);
    }

    private void LevelUp()
    {
        level++;
        RecalculateStats();
        if (level < maxLevel) 
        {
            xpToNextLevel = requiredXPBase + (level-1) * requiredXPLevelMultiplier;
        }
        else
        {
            experience = xpToNextLevel;
            UIManager.Instance.UpdateExperience(experience, xpToNextLevel);
        }
        if (level > 1) 
        { 
            UpgradeManager.Instance.ShowCanvas();
            AudioManager.Instance.Play("Level Up"); 
        }
    }

    public void AddStats(StatsSO statBonus)
    {
        healthBaseBonus += statBonus.healthBaseBonus;
        attackDamageBaseBonus += statBonus.attackDamageBaseBonus;
        attackSpeedBaseBonus += statBonus.attackSpeedBaseBonus;
        abilityHasteBaseBonus += statBonus.abilityHasteBaseBonus;
        abilityPowerBaseBonus += statBonus.abilityPowerBaseBonus;
        RecalculateStats();
    }

    public void SetScalingStats(StatsSO statBonus)
    {
        //
        healthPercentBonus = statBonus.healthPercentBonus;
        attackDamagePercentBonus = statBonus.attackDamagePercentBonus;
        attackSpeedPercentBonus = statBonus.attackSpeedPercentBonus;
        abilityHastePercentBonus = statBonus.abilityHastePercentBonus;
        abilityPowerPercentBonus = statBonus.abilityPowerPercentBonus;
        scalePercentScaling = statBonus.scalePercentScaling;
        moveSpeedPercentScaling = statBonus.moveSpeedPercentScaling;
        RecalculateStats();
    }
    
    private void RecalculateStats()
    {
        scaleFinal = CalculateFinalStat(scaleBase, scalePerLevel, scaleBaseBonus, scalePercentScaling);
        moveSpeedFinal = CalculateFinalStat(moveSpeedBase, moveSpeedPerLevel, moveSpeedBaseBonus, moveSpeedPercentScaling);
        
        healthFinal = CalculateFinalStat(healthBase, healthPerLevel, healthBaseBonus, healthPercentBonus);
        
        attackDamageFinal = CalculateFinalStat(attackDamageBase, attackDamagePerLevel, attackDamageBaseBonus, attackDamagePercentBonus);
        attackSpeedFinal = CalculateFinalStat(attackSpeedBase, attackSpeedPerLevel, attackSpeedBaseBonus, attackSpeedPercentBonus);
        abilityHasteFinal = CalculateFinalStat(abilityHasteBase, abilityHastePerLevel, abilityHasteBaseBonus, abilityHastePercentBonus);
        abilityPowerFinal = CalculateFinalStat(abilityPowerBase, abilityPowerPerLevel, abilityPowerBaseBonus, abilityPowerPercentBonus);
        
        characterMovement.SetMoveSpeed(moveSpeedFinal);
        characterWeapon.CurrentWeapon.transform.localScale = new Vector3(scaleFinal, scaleFinal, 1f);
        health.SetNewMaxHealth(healthFinal);
        
        
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
