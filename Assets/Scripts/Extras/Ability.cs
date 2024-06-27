using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Ability
{
    public static void CirclePulse(Character character, Vector3 position, GameObject bulletPrefab, float damage, int bulletCount)
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
