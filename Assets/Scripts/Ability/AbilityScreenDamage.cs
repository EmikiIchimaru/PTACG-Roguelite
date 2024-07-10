using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityScreenDamage : Ability
{
    [SerializeField] private float baseDamage;
    protected override void CastAbility()
    {
        Camera playerCamera = Camera.main;
        // Get the bounds of the camera's viewport
        Vector3 bottomLeft = playerCamera.ViewportToWorldPoint(new Vector3(0, 0, playerCamera.nearClipPlane));
        Vector3 topRight = playerCamera.ViewportToWorldPoint(new Vector3(1, 1, playerCamera.nearClipPlane));
        
        // Define the rectangle representing the screen bounds
        Rect screenBounds = new Rect(bottomLeft.x, bottomLeft.y, topRight.x - bottomLeft.x, topRight.y - bottomLeft.y);

        // Find all colliders within the screen bounds
        Collider2D[] hitColliders = Physics2D.OverlapAreaAll(new Vector2(screenBounds.xMin, screenBounds.yMin), new Vector2(screenBounds.xMax, screenBounds.yMax));

        foreach (Collider2D hitCollider in hitColliders)
        {
            // Check if the collider has the "enemy" tag
            if (hitCollider.CompareTag("Enemy"))
            {
                // Get the enemy script attached to the collider
                EnemyHealth enemyHealth = hitCollider.GetComponent<EnemyHealth>();
                BuffDealer buffDealer = AbilityOwner.GetComponent<BuffDealer>();

                if (buffDealer != null)	
                {
                    if (buffDealer.playerBuffType != BuffType.None)
                    {
                        buffDealer.DealBuff(enemyHealth);
                        Debug.Log("applying debuff!");
                    }
                }

                if (enemyHealth != null)
                {
                    // Call the TakeDamage function on the enemy
                    enemyHealth.TakeDamage(baseDamage * (1f + 0.05f * stats.abilityPowerFinal));
                }
            }
        }
        UIManager.Instance.Flash();
    }
}
