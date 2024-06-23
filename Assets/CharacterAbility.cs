using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAbility : CharacterComponents
{
    [SerializeField] protected float abilityCooltime;
    private Vector3 currentAimAngle;
    private Vector3 direction;
    private Camera mainCamera;
    private Vector3 mousePosition;
    private float internalCooldown = 0f;

    public float percentageCooltime { get { return GetPercentageCooltime(); } }

    protected override void Start()
    {
        base.Start(); 	  
        mainCamera = Camera.main;       
    } 

    protected override void HandleAbility()
    {
        base.HandleAbility();
        if (internalCooldown > 0f) { internalCooldown -= (Time.deltaTime * stats.abilityHasteFinal); }
        UIManager.Instance.UpdateAbilityCooltime(percentageCooltime);
        if (bAbilityInput) { RequestAbilityCast(); }
        //UpdateAnimations();	       
    } 

    private void GetMousePosition()
    {
        // Get the mouse position in world coordinates
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Ensure the z coordinate is zero (since we're in 2D)
        mousePosition.z = 0;

        // Calculate the direction from the character to the mouse
        direction = mousePosition - transform.position;

        // Calculate the angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply the rotation to the character
        currentAimAngle = new Vector3(0, 0, angle-90f);
    }

    private void RequestAbilityCast()
    {
        if (internalCooldown > 0f) { return; }
        GetMousePosition();
        Debug.Log("cast ability");
        internalCooldown = abilityCooltime;
    }

    private float GetPercentageCooltime()
    {
        return (1f - (internalCooldown/abilityCooltime));
    }

}
