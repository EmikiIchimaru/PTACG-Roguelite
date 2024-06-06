using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float bulletDamage;
    [SerializeField] private float speed = 100f;
    [SerializeField] private float acceleration = 0f;

    // Returns the direction of this projectile    
    public Vector2 Direction { get; set; }
    
    // Returns if the projectile is facing right   
    //public bool FacingRight { get; set; }

    // Returns the speed of the projectile    
    public float  Speed { get; set; }

    public Character ProjectileOwner { get; set; }
    
    // Internal
    private Rigidbody2D myRigidbody2D;
    private new Collider2D collider2D;
    private SpriteRenderer spriteRenderer;
	private Vector2 movement;
	private bool canMove;
    
    private void Awake()
    {
        Speed = speed;
        canMove = true;
		                
        myRigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<Collider2D>();
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
        movement = Direction * (Speed / 10f ) * Time.fixedDeltaTime;
        myRigidbody2D.MovePosition(myRigidbody2D.position + movement);

        Speed += acceleration * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))		
        {
			other.gameObject.GetComponent<EnemyHealth>().TakeDamage(bulletDamage);		
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
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void DisableProjectile()
    {
        canMove = false;
        spriteRenderer.enabled = false;
        collider2D.enabled = false;
    }

    public void EnableProjectile()
    {
        canMove = true;
        spriteRenderer.enabled = true;
        collider2D.enabled = true;
    }
}