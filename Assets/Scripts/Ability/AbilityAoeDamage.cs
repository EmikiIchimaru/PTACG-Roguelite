using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityAoeDamage : Ability
{
    [SerializeField] private float baseDamage;
    [SerializeField] private float radius;
    protected override void CastAbility()
    {
        AbilityCreator.AreaDamage(AbilityOwner, transform.position, baseDamage * (1f + 0.05f * stats.abilityPowerFinal), radius);
        for (int i = 0; i < 10; i++)
        {
            float angleP = i * 36f;
            Vector2 offset2D = Utility.RotateVector(new Vector2(6,0), angleP);
            Vector3 transformOffset = transform.position + new Vector3(offset2D.x, offset2D.y, 0);
            VFXManager.Instance.SpellHit(transformOffset, 1f, Color.HSVToRGB(300f/360f, 1f, 1f));
        }
        
    }
}
