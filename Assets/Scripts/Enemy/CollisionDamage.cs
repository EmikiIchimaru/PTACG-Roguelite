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
        if (other.CompareTag("Player"))
        {
            // Destroy the sprite GameObject
            Health health = other.gameObject.GetComponent<Health>();
            if ( health != null ) 
            { 
                health.TakeDamage(damage); 
            }
            VFXManager.Instance.SpellHit(transform.position, transform.localScale.x, sr.color);
            Destroy(gameObject);
        }
    }
}
