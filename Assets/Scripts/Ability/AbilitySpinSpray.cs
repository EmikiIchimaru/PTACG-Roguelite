using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitySpinSpray : Ability
{
    [SerializeField] private GameObject bulletPrefab; 
    [SerializeField] private float baseDamage;
    [SerializeField] private int bulletCount;

    protected override void CastAbility()
    {
        StopCoroutine("SpinSpray");
        StartCoroutine(SpinSpray(stats.attackSpeedFinal));
        
    }

    private IEnumerator SpinSpray(float aspd)
    {
        //GameManager.isPlayerControlEnabled = false;
        //GameManager.isPlayerMovementEnabled = false;

        float interval = 0.02f;

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = i * 18f;
            //Debug.Log(i);
            AbilityCreator.ShootSP(AbilityOwner, transform.position, 0.01f * aspd * baseDamage * (1f + 0.05f * stats.abilityPowerFinal), angle, bulletPrefab);
            AbilityCreator.ShootSP(AbilityOwner, transform.position, 0.01f * aspd * baseDamage * (1f + 0.05f * stats.abilityPowerFinal), angle+180f, bulletPrefab);
            yield return new WaitForSeconds(interval);
        }
        //GameManager.isPlayerControlEnabled = true;
        //GameManager.isPlayerMovementEnabled = true;
    }
}
