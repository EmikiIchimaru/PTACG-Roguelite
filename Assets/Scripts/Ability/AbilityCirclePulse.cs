using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityCirclePulse : Ability
{
    [SerializeField] private GameObject bulletPrefab; 
    [SerializeField] private float baseDamage;
    [SerializeField] private int bulletCount;
    protected override void CastAbility()
    {
        AbilityCreator.CirclePulse(AbilityOwner, transform.position, baseDamage * stats.abilityPowerFinal, bulletCount, bulletPrefab);
    }
}
