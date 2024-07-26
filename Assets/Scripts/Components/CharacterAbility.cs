using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAbility : CharacterComponents
{
    
    public Ability currentAbility { get; set; }
    private Camera mainCamera;
    [SerializeField] private Ability abilityToUse;

    protected override void Start()
    {
        base.Start(); 	  
        mainCamera = Camera.main; 
        if (abilityToUse != null ) { EquipAbility(abilityToUse); }      
    } 

    protected override void HandleAbility()
    {
        base.HandleAbility();

        if (bAbilityInput && currentAbility != null) 
        { 
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            currentAbility.RequestAbilityCast(mousePosition); 
        }
        //UpdateAnimations();	       
    } 

    public void EquipAbility(Ability ability)
    {

        if (currentAbility != null)
        {
            Destroy(currentAbility.gameObject);
        }
        currentAbility = Instantiate(ability, transform.position, transform.rotation);
        currentAbility.transform.parent = transform;
        currentAbility.SetOwner(character);     

    } 

}
