using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
//using UnityEngine.UI;

public class UpgradeManager : Singleton<UpgradeManager>
{
    public int upgradesRemaining { get { return GetUpgradesRemaining(); } }
    [SerializeField] private GameObject upgradeCanvas;
    [SerializeField] private Character character;
    //[SerializeField] private Text upgradeTooltip;
    [SerializeField] private UpgradeButton button0;
    [SerializeField] private UpgradeButton button1;
    //[SerializeField] private UpgradeButton button2;

    [SerializeField] private List<UpgradeSO> upgradesWeapon1 = new List<UpgradeSO>();
    [SerializeField] private List<UpgradeSO> upgradesWeapon2 = new List<UpgradeSO>();
    [SerializeField] private List<UpgradeSO> upgradesWeapon3 = new List<UpgradeSO>();
    [SerializeField] private List<UpgradeSO> upgradesStats1 = new List<UpgradeSO>();
    [SerializeField] private List<UpgradeSO> upgradesStats2 = new List<UpgradeSO>();
    [SerializeField] private List<UpgradeSO> upgradesStats3 = new List<UpgradeSO>();
    [SerializeField] private List<UpgradeSO> upgradesAbilities1 = new List<UpgradeSO>();
    [SerializeField] private List<UpgradeSO> upgradesAbilities2 = new List<UpgradeSO>();
    [SerializeField] private List<UpgradeSO> upgradesBuffs = new List<UpgradeSO>();


    private List<List<UpgradeSO>> upgradesMegaList = new List<List<UpgradeSO>>();
    private List<ElementType> elementsPlayer = new List<ElementType>();
    private List<ElementType> elementsRunPool = new List<ElementType>();

    private int playerUpgradeTierIndex;

    private UpgradeSO upgrade0;
    private UpgradeSO upgrade1;
    //private UpgradeSO upgrade2;

    private bool isUpgrading;

    
    private CharacterAbility characterAbility;
    private CharacterWeapon characterWeapon;
    private CharacterStats characterStats;
    private BuffDealer buffDealer;

    private ElementType playerWeaponElement;
    public ElementType playerAbilityElement;

    protected override void Awake()
    {
        base.Awake();
        characterAbility = character.GetComponent<CharacterAbility>();
        characterWeapon = character.GetComponent<CharacterWeapon>();
        characterStats = character.GetComponent<CharacterStats>();
        buffDealer = character.GetComponent<BuffDealer>();
    }

    void Start()
    {
        SetupElementsRunPool();
        playerUpgradeTierIndex = 1;
        ExpandUpgradeList();
    }

    public void ShowCanvas()
    {
        upgradeCanvas.SetActive(true);
        
        GameManager.isPlayerControlEnabled = false;
        Invoke("SetIsUpgrading",1f);
        upgradeCanvas.GetComponent<Animator>().SetTrigger("StartLoad");
        //Cursor.visible = true;
        
        //Time.timeScale = 0f;
        //playerUpgradeTierIndex = ;
        SetupTwoUpgrades(upgradesMegaList[characterStats.level-2]);
    }

    public void HideCanvas()
    {
        upgradeCanvas.SetActive(false);
        isUpgrading = false;
        GameManager.isPlayerControlEnabled = true;
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
        if (upgradesRemaining <= 0)
        {
            ExpandUpgradeList();
        }
    }

    private void SetupTwoUpgrades(List<UpgradeSO> upgrades)
    {
        List<UpgradeSO> tempUpgrades = Utility.ShuffleUpgrades(upgrades);
        upgrade0 = tempUpgrades[0];
        upgrade1 = tempUpgrades[1];
        //upgrade2 = tempUpgrades[2];
        button0.LoadUpgradeInformation(tempUpgrades[0]);
        button1.LoadUpgradeInformation(tempUpgrades[1]);
        //button2.LoadUpgradeInformation(tempUpgrades[2]);
    }

    private void ApplyUpgrade(UpgradeSO upgrade)
    {
        switch (upgrade.upgradeType)
        {
            case UpgradeType.Weapon:
            {
                characterStats.SetScalingStats(upgrade.newStats);
                characterWeapon.EquipWeapon(upgrade.newWeapon);
                playerWeaponElement = upgrade.elementType;
                
                Debug.Log("new weapon equipped");
                break;
            }
            case UpgradeType.Stats:
            {
                characterStats.AddStats(upgrade.newStats);
                Debug.Log("new stats equipped");
                break;
            }
            case UpgradeType.Ability:
            {
                characterAbility.EquipAbility(upgrade.newAbility);
                playerAbilityElement = upgrade.elementType;
                Debug.Log("new ability equipped");
                break;
            }
            case UpgradeType.Buff:
            {
                buffDealer.playerBuffType = upgrade.newBuffType;
                Debug.Log("new buff equipped");
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
                Debug.Log("expand 1");
                break;
            case 2:
                InitializeUpgradeList2();
                Debug.Log("expand 2");
                break;
            case 3:
                InitializeUpgradeList3();
                Debug.Log("expand 3");
                break;
        }
    }

    private void InitializeUpgradeList1()
    {
        List<List<UpgradeSO>> tempList = new List<List<UpgradeSO>>();
        upgradesWeapon1 = upgradesWeapon1.Where(upgrade => elementsRunPool.Contains(upgrade.elementType)).ToList();
        upgradesStats1 = upgradesStats1.Where(upgrade => elementsRunPool.Contains(upgrade.elementType)).ToList();
        upgradesAbilities1 = upgradesAbilities1.Where(upgrade => elementsRunPool.Contains(upgrade.elementType)).ToList();

        tempList.Add(upgradesStats1);
        tempList.Add(upgradesWeapon1);
        tempList.Add(upgradesAbilities1);

        //Utility.ShuffleUpgradeLists(tempList);
        upgradesMegaList.AddRange(tempList);
        playerUpgradeTierIndex++;
    }

    private void InitializeUpgradeList2()
    {
        List<List<UpgradeSO>> tempList = new List<List<UpgradeSO>>();

        RemoveRandomElementNotInList(elementsRunPool, elementsPlayer);
        //if (elementsPlayer.Count > 1) { elementsPlayer.Remove(ElementType.None); }
        upgradesWeapon2 = upgradesWeapon2.Where(upgrade => elementsPlayer.Contains(upgrade.elementType)).ToList();
        upgradesStats2 = upgradesStats2.Where(upgrade => elementsRunPool.Contains(upgrade.elementType)).ToList();
        upgradesBuffs = upgradesBuffs.Where(upgrade => elementsRunPool.Contains(upgrade.elementType)).ToList();
        
        tempList.Add(upgradesWeapon2);
        tempList.Add(upgradesStats2);
        tempList.Add(upgradesBuffs);

        Utility.ShuffleUpgradeLists(tempList);
        upgradesMegaList.AddRange(tempList);
        playerUpgradeTierIndex++;
    }
    private void InitializeUpgradeList3()
    {
        List<UpgradeSO> tempUpgrades = new List<UpgradeSO>();

        RemoveRandomElementNotInList(elementsRunPool, elementsPlayer);

        upgradesWeapon3 = upgradesWeapon3.Where(upgrade => upgrade.elementType == playerWeaponElement).ToList();
        upgradesAbilities2 = upgradesAbilities2.Where(upgrade => elementsPlayer.Contains(upgrade.elementType)).ToList();
        upgradesStats3 = upgradesStats3.Where(upgrade => elementsPlayer.Contains(upgrade.elementType)).ToList();

        

        tempUpgrades.AddRange(upgradesAbilities2);
        tempUpgrades.AddRange(upgradesStats3);

        //Utility.ShuffleUpgradeLists(tempList);
        
        upgradesMegaList.Add(tempUpgrades);
        upgradesMegaList.Add(upgradesWeapon3);

        playerUpgradeTierIndex++;
    }

    private void SetupElementsRunPool()
    {
        elementsRunPool = new List<ElementType>{ ElementType.Red, ElementType.Yellow, ElementType.Green, ElementType.Blue, ElementType.Pink};
        int randomBan = Random.Range(0, elementsRunPool.Count);
        elementsRunPool.RemoveAt(randomBan);
    }

    private void RemoveRandomElementNotInList(List<ElementType> sourceList, List<ElementType> exclusionList)
    {
        // Filter the source list to exclude elements in the exclusion list
        var filteredList = sourceList.Except(exclusionList).ToList();

        // Check if there are any elements left to remove
        if (filteredList.Count == 0)
        {
            //Debug.LogWarning("No elements available to remove that are not in the exclusion list.");
            return;
        }

        // Select a random element from the filtered list
        System.Random random = new System.Random();
        ElementType elementToRemove = filteredList[random.Next(filteredList.Count)];

        // Remove the chosen element from the original list
        sourceList.Remove(elementToRemove);
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
        //Debug.Log("upgrade 0");
        if (!isUpgrading) { return; }
        ApplyUpgrade(upgrade0);
        HideCanvas();
    }
    
    public void SelectUpgrade1()
    {
        //Debug.Log("upgrade 1");
        if (!isUpgrading) { return; }
        ApplyUpgrade(upgrade1);
        HideCanvas();
    }
    
    /* public void SelectUpgrade2()
    {
        //Debug.Log("upgrade 2");
        ApplyUpgrade(upgrade2);
        HideCanvas();
    } */

    private int GetUpgradesRemaining()
    {
        Debug.Log("upgrades remaining: " + (upgradesMegaList.Count - characterStats.level + 1).ToString());
        return upgradesMegaList.Count - characterStats.level + 1;
    }
    
    private void SetIsUpgrading()
    {
        if (!isUpgrading) 
        {
            isUpgrading = true;
            Time.timeScale = 0f;
        }
    }

}
