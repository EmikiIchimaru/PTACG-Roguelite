using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    //[SerializeField] private Image upgradeTypeIcon; 
    [SerializeField] private Image imageBackground;
    [SerializeField] private Text upgradeNameText;  
    [SerializeField] private Text upgradeDescriptText;  
    public void LoadUpgradeInformation(UpgradeSO upgrade)
    {
        imageBackground.color = Color.red; // change to upgrade color
        upgradeNameText.text = upgrade.upgradeName;
        upgradeDescriptText.text = upgrade.upgradeDescript;
    }
}
