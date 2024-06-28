using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityBlink : Ability
{
    public LayerMask wallLayerMask;
    protected override void CastAbility()
    {
        Vector2 currentPosition = AbilityOwner.transform.position;
        Vector2 targetPosition = mousePosition;

        // Perform the linecast to check for walls
        RaycastHit2D hit = Physics2D.Linecast(currentPosition, targetPosition, wallLayerMask);

        if (hit.collider == null)
        {
            AbilityOwner.transform.position = targetPosition;
            // Wall detected, set the target position to the point just before the wall
            //targetPosition = hit.point - (hit.point - currentPosition).normalized * 0.1f; // Adjust the offset as needed
        }

        
    }
}
