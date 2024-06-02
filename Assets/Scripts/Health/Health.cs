using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{   
    public static Action OnBossDead;
 
    [Header("Health")]
    [SerializeField] private float initialHealth = 10f;
    [SerializeField] private float maxHealth = 10f;

    [Header("Shield")] 
    [SerializeField] private float initialShield = 0f;
    [SerializeField] private float maxShield = 5f;

    [Header("Settings")] 
    [SerializeField] private bool destroyObject;

    private Character character;
    private CharController controller;
    private new Collider2D collider2D;
    private SpriteRenderer spriteRenderer;
    private EnemyHealth enemyHealth;
    private Loot loot;
    //private BossBaseShot bossBaseShot;

    private bool isPlayer;
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
        enemyHealth = GetComponent<EnemyHealth>();  
        loot = GetComponent<Loot>();
        //bossBaseShot = GetComponent<BossBaseShot>();

        spriteRenderer = GetComponent<SpriteRenderer>();

        CurrentHealth = initialHealth;
        CurrentShield = initialShield;

        if (character != null)
        {
            isPlayer = character.CharacterType == Character.CharacterTypes.Player;
        }
         
        UpdateCharacterHealth();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            TakeDamage(1);
        }
    }

    // Take the amount of damage we pass in parameters
    public void TakeDamage(float damage)
    {
        if (CurrentHealth <= 0)
        {
            return;
        }

        if (!shieldBroken && character != null && initialShield > 0)
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

    // Kills the game object
    private void Die()
    {
        if (character != null)
        {
            collider2D.enabled = false;
            spriteRenderer.enabled = false;

            character.enabled = false;
            controller.enabled = false;
        }
/* 
        if (bossBaseShot != null)
        {
            OnBossDead?.Invoke();
        }
 */
        if (destroyObject)
        {
            DestroyObject();
            if (loot != null) { loot.DropLoot();}
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
        }

        gameObject.SetActive(true);

        CurrentHealth = initialHealth;
        CurrentShield = initialShield;

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
	
    // If destroyObject is selected, we destroy this game object
    private void DestroyObject()
    {
        gameObject.SetActive(false);
    }

    private void UpdateCharacterHealth()
    {
        // Update Enemy health
       /*  if (enemyHealth != null && bossBaseShot == null)
        {
            enemyHealth.UpdateEnemyHealth(CurrentHealth, maxHealth);
        } */

        // Update Boss health
        /* if (bossBaseShot != null)
        {
            UIManager.Instance.UpdateBossHealth(CurrentHealth, maxHealth);
        }   */
      
        // Update Player health
        /* if (character != null && bossBaseShot == null)
        {
            UIManager.Instance.UpdateHealth(CurrentHealth, maxHealth, CurrentShield, maxShield, isPlayer);
        } */
    }   
} 