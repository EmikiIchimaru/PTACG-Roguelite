using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AbilityCreator
{
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
                enemyHealth?.TakeDamage(damage);
                
            }
        }
    }

    public static void CirclePulse(Character character, Vector3 position, float damage, int bulletCount, GameObject bulletPrefab)
    {
        for (int i = 0; i < bulletCount; i++)
        {
            GameObject bulletGO = Object.Instantiate(bulletPrefab, position, Quaternion.identity);
            SpellProjectile projectile = bulletGO.GetComponent<SpellProjectile>();
            //projectile.EnableProjectile();
            projectile.ProjectileOwner = character;
            projectile.damage = damage;
            //Vector2 rotatedVector = Utility.RotateVector(new Vector2(0f,1f), (i/bulletCount) * 360f);
            //Debug.Log($"{Quaternion.Euler(rotatedVector)}");
            //projectile.SetDirection(rotatedVector.normalized);
            float angle = i*360f/bulletCount;
            //Debug.Log(angle.ToString());
            projectile.SetAngle(angle);
        }
    }
}
