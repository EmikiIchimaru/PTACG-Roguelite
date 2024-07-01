using UnityEngine;

[CreateAssetMenu(fileName = "New Stats", menuName = "Stats")]
public class StatsSO : ScriptableObject
{
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
    //
    public float scaleBaseBonus;
    public float scalePercentScaling;
    public float moveSpeedBaseBonus;
    public float moveSpeedPercentScaling;
}