using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
 // Public variable to control movement speed
    public Buff currentBuff { get; set; }
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float reflectVariance = 0.2f;
    [SerializeField] private float speedVariance = 0.1f;

    // Private variable to store the movement direction
    private Vector2 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        // Generate a random direction
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
    void Update()
    {
        //if (positionIndexX != LevelManager.Instance.currentX || positionIndexY != LevelManager.Instance.currentY) { return; }
        // Move the sprite by translating its position
        float buffMultiplier = 1f;
        if (currentBuff != null)
        {
            buffMultiplier = currentBuff.speedMultiplier;
        }
        transform.Translate(moveDirection * moveSpeed * buffMultiplier * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Environment"))
        {
            // Get the contact point and normal
            ContactPoint2D contact = collision.contacts[0];
            Vector2 normal = contact.normal;

            // Reflect the direction based on the collision normal
            moveDirection = Vector2.Reflect(moveDirection, normal);

            //Add randomness
            moveDirection += new Vector2(Random.Range(-reflectVariance, reflectVariance), Random.Range(-reflectVariance, reflectVariance));
            moveSpeed *= Random.Range(1-speedVariance, 1+speedVariance);
        }
        
    }
}
