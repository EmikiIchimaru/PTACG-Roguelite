using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityMineField : Ability
{
    [SerializeField] private GameObject minePrefab; 
    [SerializeField] private float baseDamage;
    [SerializeField] private int fieldSize;
    protected override void CastAbility()
    {
        AbilityCreator.MineField(AbilityOwner, transform.position, baseDamage * (1f + 0.05f * stats.abilityPowerFinal), fieldSize, minePrefab);
    }
}
