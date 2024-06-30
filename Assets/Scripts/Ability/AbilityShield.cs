using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityShield : Ability
{
    [SerializeField] private float duration;
    [SerializeField] private GameObject shieldPrefab;

    protected override void CastAbility()
    {
        GameObject shieldGO = Instantiate(shieldPrefab, AbilityOwner.transform.position, Quaternion.identity, AbilityOwner.transform);
        shieldGO.GetComponent<TimedLife>().lifetime = duration;
    }
}
