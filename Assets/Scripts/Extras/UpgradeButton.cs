using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    //[SerializeField] private Image upgradeTypeIcon; 
    [SerializeField] private Image imageBackground;
    [SerializeField] private Text upgradeTypeText;  
    [SerializeField] private Text upgradeNameText;  
    [SerializeField] private Text upgradeDescriptText;  
    public void LoadUpgradeInformation(UpgradeSO upgrade)
    {
        imageBackground.color = Colour.ElementToColour(upgrade.elementType); // change to upgrade color
        upgradeTypeText.text = upgrade.upgradeType.ToString() + " Upgrade";
        upgradeNameText.text = upgrade.upgradeName;
        upgradeDescriptText.text = upgrade.upgradeDescript;
    }
}
