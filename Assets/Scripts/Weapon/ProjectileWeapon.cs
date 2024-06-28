using System; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ProjectileWeapon : Weapon
{
    [SerializeField] private Vector3 projectileSpawnPosition;
    [SerializeField] private Vector3 projectileSpread;

    // Controls the position of our projectile spawn
    public Vector3 ProjectileSpawnPosition { get; set; }

    // Returns the reference to the pooler in this GameObject
    public ObjectPooler Pooler { get; set; }

    private Vector3 projectileSpawnValue;
    private Vector3 randomProjectileSpread;

    protected override void Awake()
    {
	    base.Awake();
		
        projectileSpawnValue = projectileSpawnPosition;
        projectileSpawnValue.y = -projectileSpawnPosition.y; 

        Pooler = GetComponent<ObjectPooler>();
    }

    protected override void RequestShot()
    {
        base.RequestShot();
        if (!CanShoot) { return; }
        foreach (Transform child in transform)
        {
            //EvaluateProjectileSpawnPosition();
            if (child.gameObject.activeInHierarchy && (child.gameObject.tag == "WeaponPart"))
            {
                SpawnProjectile(child.position, child.localEulerAngles.z);
            }
        }
        internalCooldown = finalAttackCooltime;
    }

    // Spawns a projectile from the pool, setting it's new direction based on the character's direction (WeaponOwner)
    private void SpawnProjectile(Vector2 spawnPosition, float angle)
    {
        // Get Object from the pool
        GameObject projectilePooled = Pooler.GetObjectFromPool();
        projectilePooled.transform.position = spawnPosition;
        projectilePooled.SetActive(true);

        // Get reference to the projectile
        Projectile projectile = projectilePooled.GetComponent<Projectile>();
        projectile.EnableProjectile();
		projectile.ProjectileOwner = WeaponOwner;
        projectile.buffDealer = buffDealer;
        projectile.damage = baseDamage * damageX;
/* 
        // Spread
        randomProjectileSpread.z = Random.Range(-projectileSpread.z, projectileSpread.z);
        Quaternion spread = Quaternion.Euler(randomProjectileSpread); */
        //Debug.Log($"{angle}");
        //Debug.Log($"{Quaternion.Euler(rotatedVector)}");
        Vector2 rotatedVector = Utility.RotateVector(weaponFacing.normalized, angle);
        projectile.SetDirection(rotatedVector.normalized);
        

    }

    // Calculates the position where our projectile is going to be fired
    private void EvaluateProjectileSpawnPosition()
    {
        ProjectileSpawnPosition = transform.position;   
    }

    private void OnDrawGizmosSelected()
    {
        EvaluateProjectileSpawnPosition();

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(ProjectileSpawnPosition, 0.1f);
    }
}