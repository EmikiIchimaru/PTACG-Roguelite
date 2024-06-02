using UnityEngine;

public class RainbowSprite : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private float hue;
    [SerializeField] private float hueChangeSpeed; // Adjust this to change the speed of the hue cycle

    private Vector3 originalScale;
    [SerializeField] private float scaleChangeSpeed; // Adjust this to change the speed of the breathing effect
    [SerializeField] private float scaleAmount; // Adjust this to change the amplitude of the breathing effect

    void Start()
    {
        // Get the SpriteRenderer component attached to the GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Store the original scale of the GameObject
        originalScale = transform.localScale;
    }

    void Update()
    {
        // Increment the hue value over time
        hue += hueChangeSpeed * Time.deltaTime;

        // Ensure hue stays within 0 and 1
        if (hue > 1f)
        {
            hue -= 1f;
        }

        // Convert hue to a color and apply it to the sprite renderer
        Color newColor = Color.HSVToRGB(hue, 1f, 1f);
        spriteRenderer.color = newColor;

        // Calculate the new scale using a sine wave for the breathing effect
        float scale = 1f + Mathf.Sin(Time.time * scaleChangeSpeed) * scaleAmount;
        transform.localScale = originalScale * scale;
    }
}
