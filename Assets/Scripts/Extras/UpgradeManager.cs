using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : Singleton<UpgradeManager>
{
    [SerializeField] private GameObject upgradeCanvas;
    [SerializeField] private CharacterWeapon characterWeapon;

    [SerializeField] private UpgradeButton button0;
    [SerializeField] private UpgradeButton button1;
    [SerializeField] private UpgradeButton button2;

    [SerializeField] private List<UpgradeSO> upgrades = new List<UpgradeSO>();

    private UpgradeSO upgrade0;
    private UpgradeSO upgrade1;
    private UpgradeSO upgrade2;

    private bool isUpgrading;

    public void ShowCanvas()
    {
        upgradeCanvas.SetActive(true);
        isUpgrading = true;
        Invoke("UpgradePause",1f);
        upgradeCanvas.GetComponent<Animator>().SetTrigger("StartLoad");
        //Cursor.visible = true;
        
        //Time.timeScale = 0f;
        SetupThreeUpgrades();
    }

    public void HideCanvas()
    {
        upgradeCanvas.SetActive(false);
        isUpgrading = false;
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
    }

    private void SetupThreeUpgrades()
    {
        UpgradeSO[] upgradesArray = upgrades.ToArray();
        upgrade0 = upgrades[0];
        upgrade1 = upgrades[1];
        upgrade2 = upgrades[2];
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

    public void SelectUpgrade0()
    {
        Debug.Log("upgrade 0");
        ApplyUpgrade(upgrade0);
        HideCanvas();
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
    
    private void UpgradePause()
    {
        if (isUpgrading) {Time.timeScale = 0f;}
    }

}
