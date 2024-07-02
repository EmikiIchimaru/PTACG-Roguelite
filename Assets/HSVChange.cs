using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HSVChange : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private float value;
    [SerializeField] private float valueChangeSpeed = 0.1f; // Adjust this to change the speed of the value cycle
    [SerializeField] private float minValue = 0.1f; // Minimum value for the cycle
    [SerializeField] private float maxValue = 0.3f; // Maximum value for the cycle

    private float originalHue;
    private float originalSaturation;
    private bool increasing = true; // Boolean to track whether the value is increasing or decreasing

    void Start()
    {
        // Get the SpriteRenderer component attached to the GameObject
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Get the original color of the sprite
        Color.RGBToHSV(spriteRenderer.color, out originalHue, out originalSaturation, out value);
    }

    void Update()
    {
        if (valueChangeSpeed > 0)
        {
            // Increment or decrement the value based on the increasing boolean
            if (increasing)
            {
                value += valueChangeSpeed * Time.deltaTime;
                if (value >= maxValue)
                {
                    value = maxValue;
                    increasing = false;
                }
            }
            else
            {
                value -= valueChangeSpeed * Time.deltaTime;
                if (value <= minValue)
                {
                    value = minValue;
                    increasing = true;
                }
            }

            // Convert value to a color and apply it to the sprite renderer
            Color newColor = Color.HSVToRGB(originalHue, originalSaturation, value);
            spriteRenderer.color = newColor;
        }
    }
}