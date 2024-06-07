using UnityEngine;

public class BasicAnimation : MonoBehaviour
{
    [Header("Hue")]
    private SpriteRenderer spriteRenderer;
    private float hue;
    [SerializeField] private float hueChangeSpeed; // Adjust this to change the speed of the hue cycle

    [Header("Scale")]
    private Vector3 originalScale;
    [SerializeField] private float scaleChangeSpeed; // Adjust this to change the speed of the breathing effect
    [SerializeField] private float scaleAmount; // Adjust this to change the amplitude of the breathing effect

    //[Header("Rotate")]
    //[SerializeField] private float rotationSpeed;

    void Start()
    {
        // Get the SpriteRenderer component attached to the GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Store the original scale of the GameObject
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (hueChangeSpeed > 0)
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
        }
        //transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);

        // Calculate the new scale using a sine wave for the breathing effect
        float scale = 1f + Mathf.Sin(Time.time * scaleChangeSpeed) * scaleAmount;
        transform.localScale = originalScale * scale;
        //Debug.Log($"{transform.rotation}");
    }
}
