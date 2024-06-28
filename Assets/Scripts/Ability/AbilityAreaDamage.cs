using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityAreaDamage : Ability
{
    [SerializeField] private float baseDamage;
    [SerializeField] private float radius;
    protected override void CastAbility()
    {
        AbilityCreator.AreaDamage(AbilityOwner, transform.position, baseDamage * stats.abilityPowerFinal, radius);
    }
}
