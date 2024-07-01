using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade")]
public class UpgradeSO : ScriptableObject
{
    public int upgradeLevel;
    public ElementType elementType;
    public UpgradeType upgradeType;
    public string upgradeName;
    public string upgradeDescript;
    public Weapon newWeapon;
    public StatsSO newStats;
    public Ability newAbility;
    public BuffType newBuffType;

}