using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectile : MonoBehaviour
{
    public float damage;
    [SerializeField] private float speed = 100f;
    [SerializeField] private float acceleration = 0f;
    private float bulletDuration = 0.4f;

    // Returns the direction of this projectile    
    public Vector2 Direction { get; set; }
    
    // Returns if the projectile is facing right   
    //public bool FacingRight { get; set; }

    // Returns the speed of the projectile    
    public float Speed { get; set; }

    public Character ProjectileOwner { get; set; }

    public bool hasTimedLife;
    
    
    // Internal
    private Rigidbody2D myRigidbody2D;
    private new Collider2D collider2D;
    private SpriteRenderer spriteRenderer;
    private UnityEngine.Rendering.Universal.Light2D light2D;
	private Vector2 movement;
	private bool canMove;
    private float internalTimer;
    
    private void Awake()
    {
        Speed = speed;
        canMove = true;
		                
        myRigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<Collider2D>();
        light2D = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
    }

    void Update()
    {
        if (hasTimedLife && canMove)
        {
            internalTimer -= Time.deltaTime;
            if (internalTimer < 0) { DisableProjectile(); }
        }
    }

    private void FixedUpdate()
    {       
        if (canMove)
        {
            MoveProjectile();
        }
    }
    
    // Moves this projectile  
    public void MoveProjectile()
    {
        movement = Direction * Speed * Time.fixedDeltaTime;
        myRigidbody2D.MovePosition(myRigidbody2D.position + movement);

        Speed += acceleration * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))		
        {
			other.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);		
            //fx
            DisableProjectile();
        }
        if (other.CompareTag("Wall"))		
        {
			//other.gameObject.GetComponent<Health>().TakeDamage(bulletDamage);		
            //fx
            DisableProjectile();
        }
    }

   
    // Flips this projectile   
    /* public void FlipProjectile()
    {   
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    } */
  
    // Set the direction and rotation in order to move  
    public void SetDirection(Vector2 newDirection) //, bool isFacingRight = true)
    {
        Direction = newDirection.normalized;
         // Calculate the angle in degrees from the direction vector
        float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;

        // Set the rotation of the transform
        transform.rotation = Quaternion.Euler(0, 0, angle-90f);
    }

    public void DisableProjectile()
    {
        canMove = false;
        spriteRenderer.enabled = false;
        collider2D.enabled = false;
        light2D.enabled = false;
    }

    public void EnableProjectile()
    {
        canMove = true;
        spriteRenderer.enabled = true;
        collider2D.enabled = true;
        light2D.enabled = true;
        if (hasTimedLife) { internalTimer = bulletDuration; }
    }
}