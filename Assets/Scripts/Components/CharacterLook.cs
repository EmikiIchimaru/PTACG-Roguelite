using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLook : CharacterComponents
{   

    public Vector3 currentAimAngle;

    private Camera mainCamera;
    private Vector3 direction;
    private Vector3 mousePosition;
    
    protected override void Start()
    {
        base.Start(); 	
        Cursor.visible = false;  
        mainCamera = Camera.main;       
    } 

    protected override void HandleAbility()
    {
        base.HandleAbility();
        GetMousePosition();
        controller.SetAngle(currentAimAngle);
        //UpdateAnimations();	       
    } 

    private void GetMousePosition()
    {
         // Get the mouse position in world coordinates
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Ensure the z coordinate is zero (since we're in 2D)
        mousePosition.z = 0;

        // Calculate the direction from the character to the mouse
        Vector3 direction = mousePosition - transform.position;

        // Calculate the angle in degrees
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Apply the rotation to the character
        currentAimAngle = new Vector3(0, 0, angle-90f);

    }
}