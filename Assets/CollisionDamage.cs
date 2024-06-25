using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // Destroy the sprite GameObject
            Health health = other.gameObject.GetComponent<Health>();
            if ( health != null ) 
            { 
                health.TakeDamage(1f); 
            }
            Destroy(gameObject);
        }
    }
}
