using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AbilityCreator
{
    public static void ShootSP(Character character, Vector3 position, float damage, float angle, GameObject bulletPrefab)
    {
        GameObject bulletGO = Object.Instantiate(bulletPrefab, position, Quaternion.identity);
        SpellProjectile projectile = bulletGO.GetComponent<SpellProjectile>();
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
}
