using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellProjectile : MonoBehaviour
{
    public float damage;
    [SerializeField] private float speed = 100f;
    [SerializeField] private float acceleration = 0f;

    // Returns the direction of this projectile    
    public Vector2 Direction { get; set; }
    
    // Returns if the projectile is facing right   
    //public bool FacingRight { get; set; }

    // Returns the speed of the projectile    
    public float Speed { get; set; }
    public Character ProjectileOwner { get; set; }

    public bool hasTimedLife;
    private float bulletDuration = 0.5f;
    
    // Internal
    private Rigidbody2D myRigidbody2D;
	private Vector2 movement;
	private bool canMove;
    private float internalTimer;
    
    private void Awake()
    {
        Speed = speed;
        canMove = true;
		internalTimer = bulletDuration;                
        myRigidbody2D = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        if (hasTimedLife && canMove)
        {
            internalTimer -= Time.deltaTime;
            if (internalTimer < 0) { DestroyProjectile(); }
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
            DestroyProjectile();
        }
        if (other.CompareTag("Wall"))		
        {
			//other.gameObject.GetComponent<Health>().TakeDamage(bulletDamage);		
            //fx
            DestroyProjectile();
        }
    }
  
/*     // Set the direction and rotation in order to move  
    public void SetDirection(Vector2 newDirection) //, bool isFacingRight = true)
    {
        Direction = newDirection.normalized;
         // Calculate the angle in degrees from the direction vector
        float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;

        // Set the rotation of the transform
        transform.rotation = Quaternion.Euler(0, 0, angle);
    } */

    public void SetAngle(float angle) // Angle in degrees
    {
        // Convert the angle from degrees to radians
        float angleRad = angle * Mathf.Deg2Rad;

        // Calculate the direction vector using cosine and sine
        Vector2 direction = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));

        // Normalize the direction vector
        Direction = direction.normalized;

        // Set the rotation of the transform
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void DestroyProjectile()
    {
        Destroy(gameObject);
    }

}
