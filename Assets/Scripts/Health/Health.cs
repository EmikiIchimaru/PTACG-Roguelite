using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{   
    public static event Action OnBossDeath;
    public static event Action OnPlayerDeath;
 
    [SerializeField] private float maxHealth = 10f;
    [SerializeField] private float maxShield = 5f;

    [Header("Settings")] 
    [SerializeField] private bool destroyObject;

    private Character character;
    private CharController controller;
    private new Collider2D collider2D;
    private CharacterWeapon weapon;
    private CharacterAbility ability;
    private SpriteRenderer spriteRenderer;
    private EnemyHealth enemyHealth;
    private Loot loot;
    //private BossBaseShot bossBaseShot;

    private bool isPlayer;

    private bool isBoss;
    private bool shieldBroken;
    // Controls the current health of the object    
    public float CurrentHealth { get; set; }

    // Returns the current health of this character
    public float CurrentShield { get; set; }
    
    private void Awake()
    {
        character = GetComponent<Character>();
        controller = GetComponent<CharController>();
        collider2D = GetComponent<Collider2D>();      
        weapon = GetComponent<CharacterWeapon>();
        ability = GetComponent<CharacterAbility>();

        enemyHealth = GetComponent<EnemyHealth>();  
        loot = GetComponent<Loot>();
        //bossBaseShot = GetComponent<BossBaseShot>();

        spriteRenderer = GetComponent<SpriteRenderer>();

        CurrentHealth = maxHealth;
        CurrentShield = maxShield;

        if (character != null)
        {
            isPlayer = character.CharacterType == Character.CharacterTypes.Player;
            isBoss = character.CharacterType == Character.CharacterTypes.Boss;
        }
         
        
    }

    void Start()
    {
        UpdateCharacterHealth();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (isPlayer){ TakeDamage(1); }
        }
    }

    // Take the amount of damage we pass in parameters
    public void TakeDamage(float damage)
    {
        if (CurrentHealth <= 0)
        {
            return;
        }

        if (!shieldBroken && character != null && CurrentShield > 0)
        {
            CurrentShield -= damage;

            UpdateCharacterHealth();

            if (CurrentShield <= 0)
            {
                shieldBroken = true;
            }
            return;
        }
        
        CurrentHealth -= damage;

        UpdateCharacterHealth();

        if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float healAmount)
    {
        CurrentHealth += healAmount;

        UpdateCharacterHealth();
    }

    // Kills the game object
    private void Die()
    {
        
        if (isPlayer)
        {
            //weapon.enabled = false;
            //ability.enabled = false;
            OnPlayerDeath?.Invoke();
            gameObject.SetActive(false);
            return;
        }

        

        if (isBoss)
        {
            OnBossDeath?.Invoke();
        }

        if (destroyObject)
        {
            DestroyObject();
            if (loot != null) { loot.DropLoot(); }
        }
        else
        {
            if (character != null)
            {
                gameObject.SetActive(false);
                //collider2D.enabled = false;
                //spriteRenderer?.enabled = false;
                //character.enabled = false;
                //controller.enabled = false;
                //GetComponent<BasicMovement>() = false;
                
            }
        }
    }
    
    // Revive this game object    
    public void Revive()
    {
        if (character != null)
        {
            collider2D.enabled = true;
            spriteRenderer.enabled = true;

            character.enabled = true;
            controller.enabled = true;
            weapon.enabled = true;
            ability.enabled = true;
        }

        gameObject.SetActive(true);

        CurrentHealth = maxHealth;
        CurrentShield = maxShield;

        shieldBroken = false;
       
        UpdateCharacterHealth();
    }

    public void GainHealth(int amount)
    {
        CurrentHealth = Mathf.Min(CurrentHealth + amount, maxHealth);
        UpdateCharacterHealth();
    }
	
    public void GainShield(int amount)
    {
        CurrentShield = Mathf.Min(CurrentShield + amount, maxShield);
        UpdateCharacterHealth();
    }

    public void SetNewMaxHealth(float newMaxHealth)
    {
        float percent = CurrentHealth/maxHealth;
        maxHealth = newMaxHealth;
        if ((newMaxHealth * percent) > CurrentHealth) { CurrentHealth = (newMaxHealth * percent); }
        UIManager.Instance.UpdateHealth(CurrentHealth, maxHealth);
    }
	
    // If destroyObject is selected, we destroy this game object
    private void DestroyObject()
    {
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }

    private void UpdateCharacterHealth()
    {
        // Update Enemy health
        if (enemyHealth != null)
        {
            enemyHealth.UpdateEnemyHealth(CurrentHealth, maxHealth);
        } 

        // Update Boss health
        if (isBoss)
        {
            UIManager.Instance.UpdateBossHealth(CurrentHealth, maxHealth);
        }  
      
        // Update Player health
        if (isPlayer)
        {
            UIManager.Instance.UpdateHealth(CurrentHealth, maxHealth);
        }
    }   
} 