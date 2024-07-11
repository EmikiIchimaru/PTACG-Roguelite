using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityRingThrow : Ability
{
    [SerializeField] private GameObject ringPrefab; 
    [SerializeField] private float baseDamage;
    protected override void CastAbility()
    {
        for (int i = 0; i < 18; i++)
            {
                float angleP = i * 20f;
                Vector2 offset2D = Utility.RotateVector(new Vector2(2.5f,0), angleP);
                Vector3 transformOffset = transform.position + new Vector3(offset2D.x, offset2D.y, 0);
                float angle = Utility.GetAngleBetweenPoints(transformOffset, mousePosition);
                AbilityCreator.ShootSP(AbilityOwner, transformOffset, baseDamage * (1f + 0.05f * stats.abilityPowerFinal), angle, ringPrefab);
            }
        
    }
}
