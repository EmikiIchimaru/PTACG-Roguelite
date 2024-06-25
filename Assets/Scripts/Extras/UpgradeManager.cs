using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UpgradeManager : Singleton<UpgradeManager>
{
    [SerializeField] private GameObject upgradeCanvas;

    [SerializeField] private Character character;
    
    [SerializeField] private UpgradeButton button0;
    [SerializeField] private UpgradeButton button1;
    [SerializeField] private UpgradeButton button2;

    [SerializeField] private List<UpgradeSO> upgradesWeapon1 = new List<UpgradeSO>();
    [SerializeField] private List<UpgradeSO> upgradesWeapon2 = new List<UpgradeSO>();
    [SerializeField] private List<UpgradeSO> upgradesWeapon3 = new List<UpgradeSO>();
    [SerializeField] private List<UpgradeSO> upgradesStats1 = new List<UpgradeSO>();
    [SerializeField] private List<UpgradeSO> upgradesStats2 = new List<UpgradeSO>();

    private CharacterWeapon characterWeapon;
    private CharacterStats characterStats;

    private List<List<UpgradeSO>> upgradesMegaList = new List<List<UpgradeSO>>();
    private List<ElementType> elementsPlayer = new List<ElementType>();

    private int playerUpgradeTierIndex;

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

    void Start()
    {
        playerUpgradeTierIndex = 1;
        elementsPlayer.Add(ElementType.None);
        ExpandUpgradeList();
    }

    public void ShowCanvas()
    {
        upgradeCanvas.SetActive(true);
        isUpgrading = true;
        Invoke("UpgradePause",1f);
        upgradeCanvas.GetComponent<Animator>().SetTrigger("StartLoad");
        //Cursor.visible = true;
        
        //Time.timeScale = 0f;
        playerUpgradeTierIndex = characterStats.level -2;
        SetupThreeUpgrades(upgradesMegaList[playerUpgradeTierIndex]);
    }

    public void HideCanvas()
    {
        upgradeCanvas.SetActive(false);
        isUpgrading = false;
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        if (playerUpgradeTierIndex >= upgradesMegaList.Count)
        {
            ExpandUpgradeList();
        }
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
        AddElement(upgrade.elementType);
    }

    private void ExpandUpgradeList()
    {
        switch(playerUpgradeTierIndex)
        {
            case 1:
                InitializeUpgradeList1();
                break;
            case 2:
                InitializeUpgradeList2();
                break;
            case 3:
                InitializeUpgradeList3();
                break;
        }
    }

    private void InitializeUpgradeList1()
    {
        upgradesMegaList.Add(upgradesWeapon1);
        upgradesMegaList.Add(upgradesStats1);
        upgradesMegaList.Add(upgradesStats1);
        playerUpgradeTierIndex++;
    }

    private void InitializeUpgradeList2()
    {
        if (elementsPlayer.Count > 1) { elementsPlayer.Remove(ElementType.None); }
        upgradesWeapon2 = upgradesWeapon2.Where(upgrade => elementsPlayer.Contains(upgrade.elementType)).ToList();
        upgradesMegaList.Add(upgradesWeapon2);
        upgradesMegaList.Add(upgradesStats2);
        upgradesMegaList.Add(upgradesStats2);
        playerUpgradeTierIndex++;
    }
    private void InitializeUpgradeList3()
    {
        upgradesWeapon3 = upgradesWeapon3.Where(upgrade => elementsPlayer.Contains(upgrade.elementType)).ToList();
        upgradesMegaList.Add(upgradesWeapon3);
    }

    private void AddElement(ElementType elementType)
    {
        if (!elementsPlayer.Contains(elementType) && elementsPlayer.Count < 3)
        {
            elementsPlayer.Add(elementType);
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
