using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private GameObject healthBarPrefab;
    [SerializeField] private Vector3 offSet = new Vector3(0f, 1f, 0f);
    //[SerializeField] private int damageToApply = 1;
    
    private Health health;
    private Image healthBar;
    private GameObject enemyBar;
    
    private float enemyCurrentHealth;
    private float enemyMaxHealth;

    private void Start()
    {
        health = GetComponent<Health>();
        if (healthBarPrefab != null)
        {
            enemyBar = Instantiate(healthBarPrefab, transform.position + offSet, Quaternion.identity);
            enemyBar.transform.parent = transform;
            healthBar = enemyBar.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
        }
    }

    private void Update()
    {
        UpdateHealth();
    }

    public void TakeDamage(float damage)
    {
        health.TakeDamage(damage);
    }

    private void UpdateHealth()
    {
        if (healthBar != null)
        {
            healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, enemyCurrentHealth / enemyMaxHealth, 10f * Time.deltaTime);
        }
    }
    
    public void UpdateEnemyHealth(float currentHealth, float maxHealth)
    {
        enemyCurrentHealth = currentHealth;
        enemyMaxHealth = maxHealth;
    }
}