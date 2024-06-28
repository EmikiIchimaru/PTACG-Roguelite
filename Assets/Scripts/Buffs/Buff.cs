
using UnityEngine;
public class Buff : MonoBehaviour
{
    public BuffType type;
    public float duration;
    public float speedMultiplier;
    public float damagePerSecond;
    
    private EnemyHealth enemy;
    private BasicMovement enemyMovement;

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
        enemyMovement = newEnemy.GetComponent<BasicMovement>();
        if (enemyMovement != null) { enemyMovement.currentBuff = this; }
        InvokeRepeating("BuffDealDamage", 0f, 0.5f);
    }

    private void BuffDealDamage()
    {
        enemy.TakeDamage(0.5f * damagePerSecond);
    }

    public void RemoveBuff()
    {
       
        enemy.currentBuff = null;
        Destroy(gameObject);
    }
} 
