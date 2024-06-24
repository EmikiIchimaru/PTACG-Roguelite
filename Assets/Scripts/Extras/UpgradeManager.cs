using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : Singleton<UpgradeManager>
{
    [SerializeField] private GameObject upgradeCanvas;

    [SerializeField] private Character character;
    
    [SerializeField] private UpgradeButton button0;
    [SerializeField] private UpgradeButton button1;
    [SerializeField] private UpgradeButton button2;

    [SerializeField] private List<UpgradeSO> upgradesWeapon1 = new List<UpgradeSO>();
    [SerializeField] private List<UpgradeSO> upgradesStats1 = new List<UpgradeSO>();

    private CharacterWeapon characterWeapon;
    private CharacterStats characterStats;

    private UpgradeSO upgrade0;
    private UpgradeSO upgrade1;
    private UpgradeSO upgrade2;

    private bool isUpgrading;

    protected override void Awake()
    {
        base.Awake();
        characterWeapon = character.GetComponent<CharacterWeapon>();
        characterStats = character.GetComponent<CharacterStats>();
    }

    public void ShowCanvas()
    {
        upgradeCanvas.SetActive(true);
        isUpgrading = true;
        Invoke("UpgradePause",1f);
        upgradeCanvas.GetComponent<Animator>().SetTrigger("StartLoad");
        //Cursor.visible = true;
        
        //Time.timeScale = 0f;
        SetupThreeUpgrades(upgradesStats1);
    }

    public void HideCanvas()
    {
        upgradeCanvas.SetActive(false);
        isUpgrading = false;
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
    }

    private void SetupThreeUpgrades(List<UpgradeSO> upgrades)
    {
        List<UpgradeSO> tempUpgrades = Utility.ShuffleUpgradeList(upgrades);
        upgrade0 = tempUpgrades[0];
        upgrade1 = tempUpgrades[1];
        upgrade2 = tempUpgrades[2];
        button0.LoadUpgradeInformation(tempUpgrades[0]);
        button1.LoadUpgradeInformation(tempUpgrades[1]);
        button2.LoadUpgradeInformation(tempUpgrades[2]);
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
            case UpgradeType.Stats:
            {
                characterStats.AddStats(upgrade.newStats);
                Debug.Log("new stats equipped");
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
