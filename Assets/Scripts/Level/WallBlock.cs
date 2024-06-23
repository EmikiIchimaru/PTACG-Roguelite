using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBlock : MonoBehaviour
{
    private SpriteRenderer sr;
    public Color edgeColour;
    public bool isEdge;

    void Awake()
    {
        // Get the SpriteRenderer component attached to the GameObject
        sr = GetComponent<SpriteRenderer>();
    }

    public void SetupBlock(bool isBlockEdge)
    {
        isEdge = isBlockEdge;
        if (isBlockEdge)
        {
            sr.color = edgeColour;
        }
    }
}
