using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public Buff currentBuff;
    //[SerializeField] private int damageToApply = 1;
    private Health health;
    private Image healthBar;
    private GameObject enemyBar;
    
    private float enemyCurrentHealth;
    private float enemyMaxHealth;
    [SerializeField] private GameObject healthBarPrefab;
    [SerializeField] private float offSetScale = 1f;

    private void Start()
    {
        health = GetComponent<Health>();
    }

    private void Update()
    {
        UpdateHealth();
    }

    public void TakeDamage(float damage)
    {
        Debug.Log($"damage taken: {damage}");
        health.TakeDamage(damage);
        if (enemyBar == null)
        {
            if (healthBarPrefab != null)
            {
                enemyBar = Instantiate(healthBarPrefab, new Vector2(0f, offSetScale), Quaternion.identity);
                enemyBar.transform.localScale *= offSetScale;
                //enemyBar.transform.localScale = new Vector2(offSetScale, offSetScale);
                enemyBar.transform.SetParent(transform, false);
                healthBar = enemyBar.transform.GetChild(0).GetComponent<Image>();
            }
        }
    }

    private void UpdateHealth()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, enemyCurrentHealth / enemyMaxHealth, 10f * Time.deltaTime);
            float hue = 125f/360f * healthBar.fillAmount;
            Color newColor = Color.HSVToRGB(hue, 1f, 1f);
            healthBar.color = newColor;
        }
    }
    
    public void UpdateEnemyHealth(float currentHealth, float maxHealth)
    {
        enemyCurrentHealth = currentHealth;
        enemyMaxHealth = maxHealth;
    }
}