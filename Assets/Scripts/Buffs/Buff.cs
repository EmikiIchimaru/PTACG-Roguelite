
using UnityEngine;
public class Buff : MonoBehaviour
{
    public BuffType type;
    public float duration;
    public float speedMultiplier;
    public float damagePerTick;
    public float damageInterval;
    
    private EnemyHealth enemy;
    private EnemyMovement enemyMovement;
    private EnemyWeapon enemyWeapon;

    void Update()
    {
        duration -= Time.deltaTime;

        if (duration <= 0f || enemy == null) 
        {
            RemoveBuff();
        }
    }

    public void SetupBuff(EnemyHealth newEnemy)
    {
        enemy = newEnemy;
        enemyMovement = newEnemy.GetComponent<EnemyMovement>();
        if (enemyMovement != null) { enemyMovement.currentBuff = this; }
        enemyWeapon = newEnemy.GetComponent<EnemyWeapon>();
        if (enemyWeapon != null) { enemyWeapon.currentBuff = this; }
        InvokeRepeating("BuffDealDamage", damageInterval, damageInterval);
    }

    private void BuffDealDamage()
    {
        enemy.TakeDamage(damagePerTick);
    }

    public void RemoveBuff()
    {
        enemy.currentBuff = null;
        Destroy(gameObject);
    }
} 
