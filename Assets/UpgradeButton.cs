using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    [SerializeField] private Text upgradeNameText;  
    public void LoadUpgradeInformation(UpgradeSO upgrade)
    {
        upgradeNameText.text = upgrade.upgradeName;
    }
}
