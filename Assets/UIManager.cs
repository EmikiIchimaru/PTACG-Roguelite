using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider experienceBar;
    [SerializeField] private GameObject defeatScreen;
    [SerializeField] private Text currentHealthText;
    [SerializeField] private Text currentExperienceText;

    private float playerCurrentHealth;
    private float playerMaxHealth;

    private float playerCurrentExperience;
    private float playerMaxExperience;
    //private bool isPlayer;
    void Start()
    {
        Health.OnPlayerDeath += HandleOnPlayerDeath;
    }

    void Update()
    {
             
        healthBar.value = Mathf.Lerp(healthBar.value, playerCurrentHealth / playerMaxHealth, 5f * Time.deltaTime);
        currentHealthText.text = Mathf.Round(playerCurrentHealth).ToString() + "/" + Mathf.Round(playerMaxHealth).ToString(); 

        experienceBar.value = Mathf.Lerp(experienceBar.value, playerCurrentExperience / playerMaxExperience, 5f * Time.deltaTime);
        currentExperienceText.text = Mathf.Round(playerCurrentExperience).ToString() + "/" + playerMaxExperience.ToString();
    }

    public void UpdateHealth(float currentHealth, float maxHealth)//, bool isThisMyPlayer)//, bool isThisMyPlayer)
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
    void HandleOnPlayerDeath()
    {
        ShowDefeatScreen();
    }

    void ShowDefeatScreen()
    {
        defeatScreen.SetActive(true);
    }
}
