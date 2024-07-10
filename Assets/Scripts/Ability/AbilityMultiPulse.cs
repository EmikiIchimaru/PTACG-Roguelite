using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityMultiPulse : Ability
{
    [SerializeField] private GameObject bulletPrefab; 
    [SerializeField] private float baseDamage;
    [SerializeField] private int bulletCount;
    protected override void CastAbility()
    {
        Pulse();
        Invoke("Pulse", 0.5f);
        Invoke("Pulse", 1f);
    }
    
    private void Pulse()
    {
        AbilityCreator.CirclePulse(AbilityOwner, transform.position, baseDamage * (1f + 0.05f * stats.abilityPowerFinal), bulletCount, bulletPrefab);
    }
}
