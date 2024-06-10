using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
 // Public variable to control movement speed
    public float moveSpeed = 5f;

    // Private variable to store the movement direction
    private Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        // Generate a random direction
        float moveX = Random.Range(-1f, 1f);
        float moveY = Random.Range(-1f, 1f);

        // Create a Vector3 for movement direction
        moveDirection = new Vector3(moveX, moveY, 0);

        // Normalize the direction vector to ensure consistent movement speed
        if (moveDirection.magnitude > 1)
        {
            moveDirection.Normalize();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Move the sprite by translating its position
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    // This method is called when the collider enters a trigger collider
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collided object is on the "Environment" layer
        if (other.gameObject.layer == LayerMask.NameToLayer("Environment"))
        {
            // Destroy the sprite GameObject
            Destroy(gameObject);
        }
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            // Destroy the sprite GameObject
            Health health = other.gameObject.GetComponent<Health>();
            if ( health != null ) { health.TakeDamage(1f); }
            Destroy(gameObject);
        }
    }
}
