using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sr;
    public float damage;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Shield")) { Destroy(gameObject); }
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // Destroy the sprite GameObject
            Health health = other.gameObject.GetComponent<Health>();
            if ( health != null ) 
            { 
                health.TakeDamage(damage); 
            }
            VFXManager.Instance.SpellHit(transform.position, 1f, sr.color);
            Destroy(gameObject);
        }
    }
}
