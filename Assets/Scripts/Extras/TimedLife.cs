using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedLife : MonoBehaviour
{
    // Duration of the timer in seconds
    public float lifetime;

    // Internal timer
    private float timer;

    void Start()
    {
        // Initialize the timer with the set lifetime
        timer = lifetime;
    }

    void Update()
    {
        // Decrease the timer by the time passed since the last frame
        timer -= Time.deltaTime;

        // Check if the timer has reached zero or less
        if (timer <= 0f)
        {
            // Perform the action (e.g., destroy the GameObject)
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
}
