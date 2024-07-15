using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityRandomSpray : Ability
{
    [SerializeField] private GameObject bulletPrefab; 
    [SerializeField] private float baseDamage;
    [SerializeField] private float sprayDuration;

    private int bulletCount;

    protected override void CastAbility()
    {
        bulletCount = (int) stats.attackSpeedFinal;
        StopCoroutine("RandomSpray");
        StartCoroutine(RandomSpray());
        
    }

    private IEnumerator RandomSpray()
    {
        //GameManager.isPlayerControlEnabled = false;
        //GameManager.isPlayerMovementEnabled = false;

        float interval = sprayDuration/bulletCount;

        for (int i = 0; i < bulletCount; i++)
        {
            float angle = Random.Range(0,360f);
            //Debug.Log(i);
            AbilityCreator.ShootSP(AbilityOwner, transform.position, baseDamage * (1f + 0.05f * stats.abilityPowerFinal), angle, bulletPrefab);
            yield return new WaitForSeconds(interval);
        }
        //GameManager.isPlayerControlEnabled = true;
        //GameManager.isPlayerMovementEnabled = true;
    }
}
