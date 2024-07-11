using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
 // Public variable to control movement speed
    public Buff currentBuff { get; set; }
    [SerializeField] private bool isHostile = true;
    [SerializeField] private bool shouldChasePlayer = false;
    [SerializeField] private float baseMoveSpeed = 30f;
    [SerializeField] private float reflectVariance = 0.2f;
    [SerializeField] private float speedVariance = 0.1f;
    public Transform rotatePart;

    private Rigidbody2D rb;
    private DetectPlayer detectPlayer;
    // Private variable to store the movement direction
    private float moveSpeed;
    private Vector2 moveDirection;
    //private float internalBumpTimer = 0f;
    private float maxSpeedMultiplier = 0.25f;
    private float initialY;
    // Start is called before the first frame update
    void Start()
    {   
        initialY = transform.position.y;
        moveSpeed = baseMoveSpeed;
        rb = GetComponent<Rigidbody2D>();

        if (isHostile)
        {
            detectPlayer = GetComponent<DetectPlayer>();
        }

        SetRandomDirection();

         
    }

    private void SetRandomDirection()
    {
        float moveX = Random.Range(-1f, 1f);
        float moveY = Random.Range(-1f, 1f);

        // Create a Vector3 for movement direction
        moveDirection = new Vector2(moveX, moveY);

        // Normalize the direction vector to ensure consistent movement speed
        if (moveDirection.magnitude > 1)
        {
            moveDirection.Normalize();
        }   
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rb.velocity.magnitude < maxSpeedMultiplier * moveSpeed) { MoveUnit(); }
        if (rotatePart != null) { RotateUnit(); }
    }

    private void MoveUnit()
    {
        //if (positionIndexX != LevelManager.Instance.currentX || positionIndexY != LevelManager.Instance.currentY) { return; }
        // Move the sprite by translating its position
        Vector3 tempMoveDirection = moveDirection;
        if (shouldChasePlayer) 
        { 
            if (detectPlayer.isPlayerInRange)
            {
                tempMoveDirection = GameManager.Instance.playerCharacter.transform.position - transform.position;

                // Normalize the direction vector to ensure consistent movement speed
                if (tempMoveDirection.magnitude > 1)
                {
                    tempMoveDirection.Normalize();
                }
            }
        }
        float buffMultiplier = 1f;
        if (currentBuff != null)
        {
            buffMultiplier = currentBuff.speedMultiplier;
            if (currentBuff.type == BuffType.Frost) { rb.velocity = Vector2.zero; }
        }
        rb.AddForce(100f * tempMoveDirection * moveSpeed * buffMultiplier * Time.fixedDeltaTime);
        //transform.Translate(moveDirection * moveSpeed * buffMultiplier * Time.deltaTime);
    }

    private void RotateUnit()
    {
        
        if (detectPlayer != null && detectPlayer.isPlayerInRange) 
        {
            rotatePart.rotation = Quaternion.Euler(new Vector3(0, 0, detectPlayer.angle+90f));
        }
        else
        {
            rotatePart.rotation = Quaternion.Euler(Utility.ConvertV2ToV3(moveDirection));
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Environment") || 
            collision.gameObject.layer == LayerMask.NameToLayer("Invisible") || 
            collision.gameObject.layer == LayerMask.NameToLayer("Boundary") )
        {
            // Get the contact point and normal
            ContactPoint2D contact = collision.contacts[0];
            Vector2 normal = contact.normal;

            // Reflect the direction based on the collision normal
            moveDirection = Vector2.Reflect(moveDirection, normal);
            
            /* if (internalBumpTimer <= 0f) 
            { 
                rb.AddForce(moveDirection * moveSpeed, ForceMode2D.Impulse);
                internalBumpTimer = 1f;
            } */

            //Add randomness
            moveDirection += new Vector2(Random.Range(-reflectVariance, reflectVariance), Random.Range(-reflectVariance, reflectVariance));
            moveSpeed = Random.Range(1 - speedVariance, 1 + speedVariance) * baseMoveSpeed;
        }
        
    }

    public void SetPositiveYMovement()
    {
        if (initialY - transform.position.y > 5f && moveDirection.y < 0) 
        {
            moveDirection = new Vector2(moveDirection.x, Mathf.Abs(moveDirection.y));
        }
        
    }
}
