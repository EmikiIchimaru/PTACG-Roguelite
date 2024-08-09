using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectile : MonoBehaviour
{
    public bool hasPool = true;
    public bool hasTimedLife = false;
    public float damage;
    [SerializeField] private float speed = 100f;
    [SerializeField] private float acceleration = 0f;
    [SerializeField] private bool canPierce = false;
    [SerializeField] private bool isPersistent = false;
    private float bulletDuration = 0.5f;

    // Returns the direction of this projectile    
    public Vector2 Direction { get; set; }
    // Returns the speed of the projectile    
    public float Speed { get; set; }

    public Character ProjectileOwner { get; set; }
    public BuffDealer buffDealer { get; set; }
    // Internal
    private Rigidbody2D myRigidbody2D;
    private new Collider2D collider2D;
    private SpriteRenderer spriteRenderer;
    //private UnityEngine.Rendering.Universal.Light2D light2D;
	private Vector2 movement;
	private bool canMove;
    private float internalTimer;

    private List<EnemyHealth> hitList = new List<EnemyHealth>();
    
    private void Awake()
    {
        Speed = speed;
        canMove = true;
		                
        myRigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<Collider2D>();
        //light2D = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
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
        if (other.CompareTag("Enemy") && ProjectileOwner.CharacterType == Character.CharacterTypes.Player)		
        {
            EnemyHealth enemy = other.gameObject.GetComponent<EnemyHealth>();
			
            if ((canPierce && !hitList.Contains(enemy)) || !canPierce)
            {
                if (buffDealer != null)	
                {
                    if (buffDealer.playerBuffType != BuffType.None)
                    {
                        buffDealer.DealBuff(enemy);
                        Debug.Log("applying debuff!");
                    }
                }

                enemy.TakeDamage(damage);
                hitList.Add(enemy);
            }	
            //fx
            if (!canPierce) { DisableProjectile(); }
        }
        else if (other.CompareTag("Player") && 
            (ProjectileOwner.CharacterType == Character.CharacterTypes.AI ||
            ProjectileOwner.CharacterType == Character.CharacterTypes.Boss))	
        {
            Health health = other.gameObject.GetComponent<Health>();
		
            health.TakeDamage(damage);	
            //fx
            AudioManager.Instance.Play("Damage Taken");
            if (!canPierce) { DisableProjectile(); }
        }
        else if (other.CompareTag("Shield") && 
            ProjectileOwner.CharacterType != other.gameObject.transform.parent.GetComponent<Character>().CharacterType)		
        {
			//other.gameObject.GetComponent<Health>().TakeDamage(bulletDamage);		
            //fx
            DisableProjectile();
        }
        else if (other.CompareTag("Mirror") && 
            ProjectileOwner.CharacterType != other.gameObject.transform.parent.GetComponent<Character>().CharacterType)		
        {
			//other.gameObject.GetComponent<Health>().TakeDamage(bulletDamage);		
            //fx
            damage = 5f * damage;
            ProjectileOwner = other.gameObject.transform.parent.GetComponent<Character>();
            Direction = new Vector2(-Direction.x, -Direction.y);
        }
        else if (other.CompareTag("Wall"))		
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

    public void SetAngle(float angle) // Angle in degrees
    {
        // Convert the angle from degrees to radians
        float angleRad = angle * Mathf.Deg2Rad;

        // Calculate the direction vector using cosine and sine
        Vector2 direction = new Vector2(Mathf.Cos(angleRad), Mathf.Sin(angleRad));

        // Normalize the direction vector
        Direction = direction.normalized;

        // Set the rotation of the transform
        transform.rotation = Quaternion.Euler(0, 0, angle-90f);
    }

    public void DisableProjectile()
    {
        
        if (isPersistent) { return; }
        if (hitList.Count > 0) { hitList.Clear(); }
        VFXManager.Instance.BulletHit(transform, spriteRenderer.color);
        if (hasPool)
        {
            canMove = false;
            spriteRenderer.enabled = false;
            collider2D.enabled = false;
            //light2D.enabled = false;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    public void EnableProjectile()
    {
        canMove = true;
        spriteRenderer.enabled = true;
        collider2D.enabled = true;
        //light2D.enabled = true;
        if (hasTimedLife) { internalTimer = bulletDuration; }
    }
}