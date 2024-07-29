using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityDamageCircle : Ability
{
    [SerializeField] private float baseDamage;
    [SerializeField] private float duration;
    [SerializeField] private DamageCircle circlePrefab;
    protected override void CastAbility()
    {
        DamageCircle circle = Instantiate(circlePrefab, AbilityOwner.transform.position, Quaternion.identity);
        circle.GetComponent<TimedLife>().lifetime = duration;
        circle.character = AbilityOwner;
        circle.damage = baseDamage * (1f + 0.03f * stats.abilityPowerFinal);
        circle.GetComponent<FollowParent>().parent = AbilityOwner.transform;
    }
}
