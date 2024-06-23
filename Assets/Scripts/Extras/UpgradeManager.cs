using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : Singleton<UpgradeManager>
{
    [SerializeField] private GameObject upgradeCanvas;
    [SerializeField] private CharacterWeapon characterWeapon;

    [SerializeField] private List<UpgradeSO> upgrades = new List<UpgradeSO>();

    private UpgradeSO upgrade1;
    private UpgradeSO upgrade2;
    private UpgradeSO upgrade3;

    public void ShowCanvas()
    {
        upgradeCanvas.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0f;
        SetupThreeUpgrades();
    }

    public void HideCanvas()
    {
        upgradeCanvas.SetActive(false);
        Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
    }

    private void SetupThreeUpgrades()
    {
        upgrade1 = upgrades[0];
    }

    private void ApplyUpgrade(UpgradeSO upgrade)
    {
        switch (upgrade.upgradeType)
        {
            case UpgradeType.Weapon:
            {
                characterWeapon.EquipWeapon(upgrade.newWeapon);
                Debug.Log("new weapon equipped");
                break;
            }
        }
    }

    public void SelectUpgrade1()
    {
        Debug.Log("upgrade 1");
        ApplyUpgrade(upgrade1);
        HideCanvas();
    }
    
    public void SelectUpgrade2()
    {
        Debug.Log("upgrade 2");
        ApplyUpgrade(upgrade2);
        HideCanvas();
    }
    
    public void SelectUpgrade3()
    {
        Debug.Log("upgrade 3");
        ApplyUpgrade(upgrade3);
        HideCanvas();
    }
    

}
