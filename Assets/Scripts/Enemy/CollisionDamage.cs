using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    public float damage;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Shield")) { DeathDestroy(); }
        if (other.CompareTag("Player"))
        {
            // Destroy the sprite GameObject
            Health health = other.gameObject.GetComponent<Health>();
            if ( health != null ) 
            { 
                health.TakeDamage(damage); 
            }
            DeathDestroy();
        }
    }

    private void DeathDestroy()
    {
        Death death = GetComponent<Death>();
        if ( death != null)
        {
            death.DestroyWithFX();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
