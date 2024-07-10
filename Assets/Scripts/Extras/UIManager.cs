using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider experienceBar;
    [SerializeField] private Slider abilityBar;
    [SerializeField] private Text currentHealthText;
    [SerializeField] private Text currentExperienceText;
    [SerializeField] private Slider bossHealthBar;
    [SerializeField] private Image bossHealthFillBar;

    [SerializeField] private Image flashImage;
    [SerializeField] private GameObject victoryScreen;
    [SerializeField] private GameObject defeatScreen;
    [SerializeField] private GameObject defaultHUD;
    [SerializeField] private GameObject bossHUD;


    private float playerCurrentHealth;
    private float playerMaxHealth;
    private float playerCurrentExperience;
    private float playerMaxExperience;
    private float playerAbilityCooltimePercent;
    private float bossCurrentHealth;
    private float bossMaxHealth;
    private bool isBossHUDActive;
    //private bool isPlayer;
    private Animator animVictory;

    protected override void Awake()
    {
        base.Awake();
        animVictory = victoryScreen.GetComponent<Animator>();
    }

    void Start()
    {
        isBossHUDActive = false;
    }

    void Update()
    {
        healthBar.value = Mathf.Lerp(healthBar.value, playerCurrentHealth / playerMaxHealth, 5f * Time.deltaTime);
        currentHealthText.text = Mathf.Round(playerCurrentHealth).ToString() + " / " + Mathf.Round(playerMaxHealth).ToString(); 

        experienceBar.value = Mathf.Lerp(experienceBar.value, playerCurrentExperience / playerMaxExperience, 5f * Time.deltaTime);
        currentExperienceText.text = Mathf.Round(playerCurrentExperience).ToString() + " / " + playerMaxExperience.ToString();

        abilityBar.value = playerAbilityCooltimePercent;

        if (isBossHUDActive)
        {
            bossHealthBar.value = Mathf.Lerp(bossHealthBar.value, bossCurrentHealth / bossMaxHealth, 5f * Time.deltaTime);
            float hue = 200f/360f * bossHealthBar.value;
            Color newColor = Color.HSVToRGB(hue, 0.8f, 1f);
            bossHealthFillBar.color = newColor;
        }

        if (flashImage.color.a > 0f) 
        { 
            flashImage.color = new Color(flashImage.color.r, flashImage.color.g, flashImage.color.b, flashImage.color.a - 1.5f * Time.deltaTime);
        }
    }

    public void UpdateHealth(float currentHealth, float maxHealth)//, bool isThisMyPlayer)
    { 
        playerCurrentHealth = currentHealth;
        playerMaxHealth = maxHealth;
        //isPlayer = isThisMyPlayer;       
    }

    public void UpdateExperience(float currentExperience, float maxExperience)//, bool isThisMyPlayer)
    { 
        playerCurrentExperience = currentExperience;
        playerMaxExperience = maxExperience;      
    }

    public void UpdateAbilityCooltime(float newCooltime)
    {
        playerAbilityCooltimePercent = newCooltime;   
    }

    public void ShowBossHUD()
    {
        isBossHUDActive = true;
        bossHUD.SetActive(true);
    }

    public void UpdateBossHealth(float currentHealth, float maxHealth)
    { 
        bossCurrentHealth = currentHealth;
        bossMaxHealth = maxHealth;
    }

    public void ShowVictoryScreen()
    {
        if (victoryScreen != null)
        {
            HideHUD();
            GameManager.isPlayerControlEnabled = false;
            victoryScreen.SetActive(true);
            animVictory.SetTrigger("StartLoad");
            Debug.Log("victory");
        }
    }

    public void ShowDefeatScreen()
    {
        if (defeatScreen != null)
        {
            HideHUD();
            GameManager.isPlayerControlEnabled = false;
            defeatScreen.SetActive(true);
            Debug.Log("defeat");
        }
    }

    public void HideHUD()
    {
        defaultHUD.SetActive(false);
        isBossHUDActive = false;
        bossHUD.SetActive(false);
    }

    public void Flash()
    {
        flashImage.color = new Color(flashImage.color.r, flashImage.color.g, flashImage.color.b, 0.6f);
    }
}
