using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public static class Colour
{
    public static Color elementRedColour;
    public static Color elementYellowColour;
    public static Color elementGreenColour;
    public static Color elementBlueColour;
    public static Color elementPinkColour;

    static Colour()
    {
        // Initialize colors with hue values 0, 60, 110, 180, 300 respectively 
        elementRedColour = Color.HSVToRGB(0f / 360f, 0.75f, 1f);
        elementYellowColour = Color.HSVToRGB(60f / 360f, 0.75f, 1f);
        elementGreenColour = Color.HSVToRGB(120f / 360f, 0.75f, 1f);
        elementBlueColour = Color.HSVToRGB(220f / 360f, 0.75f, 1f);
        elementPinkColour = Color.HSVToRGB(300f / 360f, 0.75f, 1f);
    }

    public static Color ElementToColour(ElementType elementType)
    {
        switch (elementType)
        {
            case ElementType.Red:
                return elementRedColour;
            case ElementType.Yellow:
                return elementYellowColour;
            case ElementType.Green:
                return elementGreenColour;
            case ElementType.Blue:
                return elementBlueColour;
            case ElementType.Pink:
                return elementPinkColour;
            default:
                return Color.white;
        }
    }
}
