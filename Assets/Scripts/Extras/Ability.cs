using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Ability
{
    public static void CirclePulse(Vector3 position, GameObject bulletPrefab, int bulletCount)
    {
        for (int i = 0; i < bulletCount; i++)
        {
            GameObject bulletGO = Object.Instantiate(bulletPrefab, position, Quaternion.identity);
            Projectile projectile = bulletGO.GetComponent<Projectile>();
            projectile.EnableProjectile();
            //projectile.ProjectileOwner = WeaponOwner;
            //projectile.damageX = damageX;
            Vector2 rotatedVector = Utility.RotateVector(new Vector2(0f,0f), (i * 360f) / bulletCount);
            //Debug.Log($"{Quaternion.Euler(rotatedVector)}");
            projectile.SetDirection(rotatedVector.normalized);
        }
    }
}
