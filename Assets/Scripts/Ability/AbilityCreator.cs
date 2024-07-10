using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AbilityCreator
{
    public static void ShootSP(Character character, Vector3 position, float damage, float angle, GameObject bulletPrefab)
    {
        GameObject bulletGO = Object.Instantiate(bulletPrefab, position, Quaternion.identity);
        Projectile projectile = bulletGO.GetComponent<Projectile>();
        projectile.ProjectileOwner = character;
        projectile.buffDealer = character?.GetComponent<BuffDealer>();
        projectile.damage = damage;
        projectile.SetAngle(angle);
    }

    public static void AreaDamage(Character character, Vector3 position, float damage, float radius)
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(position, radius);

        foreach (Collider2D hitCollider in hitColliders)
        {
            // Check if the collider has the "enemy" tag
            if (hitCollider.CompareTag("Enemy"))
            {
                // Get the enemy script attached to the collider
                EnemyHealth enemyHealth = hitCollider.GetComponent<EnemyHealth>();
                BuffDealer buffDealer = character.GetComponent<BuffDealer>();
                if (buffDealer != null)	
                {
                    if (buffDealer.playerBuffType != BuffType.None)
                    {
                        buffDealer.DealBuff(enemyHealth);
                        Debug.Log("applying debuff!");
                    }
                }
                enemyHealth?.TakeDamage(damage);
                
            }
        }
    }

    public static void CirclePulse(Character character, Vector3 position, float damage, int bulletCount, GameObject bulletPrefab)
    {
        for (int i = 0; i < bulletCount; i++)
        {
            float angle = i*360f/bulletCount;
            ShootSP(character, position, damage, angle, bulletPrefab);
        }
    }
    public static void MineField(Character character, Vector3 position, float damage, int fieldSize, GameObject minePrefab)
    {
        float spacing = 2f;
        for (int i = 0; i < fieldSize; i++)
        {
            for (int j = 0; j < fieldSize; j++)
            {
                float x = (i - 0.5f * fieldSize) * spacing;
                float y = (j - 0.5f * fieldSize) * spacing;
                Vector3 minePosition = new Vector3(x,y,0) + position;
                GameObject bulletGO = Object.Instantiate(minePrefab, minePosition, Quaternion.identity);
                Projectile projectile = bulletGO.GetComponent<Projectile>();
                projectile.ProjectileOwner = character;
                projectile.buffDealer = character?.GetComponent<BuffDealer>();
                projectile.damage = damage;
                TimedLife timedLife = bulletGO.GetComponent<TimedLife>();
                timedLife.lifetime = 10f;
            }
        }
    }
}
